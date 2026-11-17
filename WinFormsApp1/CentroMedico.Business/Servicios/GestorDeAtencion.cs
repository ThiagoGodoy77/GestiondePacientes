using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.CentroMedico.Business.Interfaces;
using WinFormsApp1.CentroMedico.Data.Datos;
using WinFormsApp1.CentroMedico.Data.Entidades;

namespace WinFormsApp1.CentroMedico.Business.Servicios
{
    public class GestorDeAtencion : IGestorDeAtencion
    {
        public GestorDeAtencion()
        {
            // Si es la primera vez que se abre la app, crea la base con sus tablas.
            using var context = new CentroMedicoContext();
            context.Database.EnsureCreated();
        }

        // Quita puntos, guiones y espacios. Las letras no se tocan a propósito:
        // si las descartáramos, "1234567a" pasaría como un DNI válido.
        public static string NormalizarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return string.Empty;

            return new string(dni.Where(c => c != '.' && c != '-' && !char.IsWhiteSpace(c)).ToArray());
        }

        // Un DNI es válido si tiene 7 u 8 dígitos y nada más.
        public static bool EsDniValido(string dni)
        {
            return !string.IsNullOrEmpty(dni)
                && dni.Length is >= 7 and <= 8
                && dni.All(char.IsDigit);
        }

        // ───────────────────────── Pacientes ─────────────────────────

        public void RegistrarPaciente(Paciente paciente)
        {
            if (paciente == null) throw new ArgumentNullException(nameof(paciente));

            var dni = NormalizarDni(paciente.Dni);
            if (!EsDniValido(dni))
                throw new ArgumentException("El DNI debe contener entre 7 y 8 dígitos.", nameof(paciente));

            if (string.IsNullOrWhiteSpace(paciente.Nombre) || string.IsNullOrWhiteSpace(paciente.Apellido))
                throw new ArgumentException("El nombre y el apellido son obligatorios.", nameof(paciente));

            using var context = new CentroMedicoContext();
            var existente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if (existente == null)
            {
                // Lo guardamos con el DNI ya normalizado, no como llegó.
                context.Pacientes.Add(new Paciente(dni, paciente.Nombre, paciente.Apellido));
                context.SaveChanges();
                return;
            }

            // Si el DNI ya existe no pisamos los datos: mismo criterio que al sacar un turno.
            bool mismoNombre = string.Equals(existente.Nombre?.Trim(), paciente.Nombre.Trim(), StringComparison.OrdinalIgnoreCase);
            bool mismoApellido = string.Equals(existente.Apellido?.Trim(), paciente.Apellido.Trim(), StringComparison.OrdinalIgnoreCase);

            if (!mismoNombre || !mismoApellido)
                throw new InvalidOperationException(
                    $"El DNI #{dni} ya pertenece a '{existente.NombreCompleto}'. No se puede registrar con un nombre diferente.");
        }

        public Paciente ConsultarPaciente(string dni)
        {
            dni = NormalizarDni(dni);
            if (!EsDniValido(dni))
                return null;

            using var context = new CentroMedicoContext();
            return context.Pacientes.AsNoTracking().FirstOrDefault(p => p.Dni == dni);
        }

        // ───────────────────────── Turnos y agenda ─────────────────────────

        // Turnos próximos de un paciente, del más cercano al más lejano.
        // Los que ya pasaron (aunque sea hoy más temprano) no aparecen.
        public List<Turno> ConsultarTurnos(string dni)
        {
            dni = NormalizarDni(dni);
            if (!EsDniValido(dni))
                return new List<Turno>();

            var ahora = DateTime.Now;

            using var context = new CentroMedicoContext();
            return context.Turnos
                .AsNoTracking()
                .Include(t => t.Paciente)
                .Include(t => t.Estudio)
                .Where(t => t.Paciente.Dni == dni && t.FechaHora >= ahora)
                .OrderBy(t => t.FechaHora)
                .ToList();
        }

        // Agenda de un día: los turnos de todos los pacientes, en orden cronológico.
        // Si no se indica fecha, se toma el día de hoy.
        public List<Turno> ObtenerTurnosPendientesDelDia(DateTime? fecha)
        {
            var dia = (fecha ?? DateTime.Today).Date;

            using var context = new CentroMedicoContext();
            return context.Turnos
                .AsNoTracking()
                .Include(t => t.Paciente)
                .Include(t => t.Estudio)
                .Where(t => t.FechaHora.Date == dia)
                .OrderBy(t => t.FechaHora)
                .ToList();
        }

        // Horarios libres de un día, de 08:00 a 17:30 cada 30 minutos.
        // Si el día es hoy, se descartan los que ya pasaron.
        public List<DateTime> ObtenerHorariosDisponibles(DateTime fecha)
        {
            var horarios = new List<DateTime>();
            var horario = fecha.Date.AddHours(8);
            var cierre = fecha.Date.AddHours(18);
            var ahora = DateTime.Now;

            using var context = new CentroMedicoContext();
            var ocupados = context.Turnos
                .AsNoTracking()
                .Where(t => t.FechaHora.Date == fecha.Date)
                .Select(t => t.FechaHora)
                .ToList();

            while (horario < cierre)
            {
                if (horario > ahora && !ocupados.Contains(horario))
                    horarios.Add(horario);

                horario = horario.AddMinutes(30);
            }

            return horarios;
        }

        public Turno SacarTurno(Paciente paciente, Estudio estudio, DateTime fechaHora)
        {
            if (paciente == null) throw new ArgumentNullException(nameof(paciente));
            if (estudio == null) throw new ArgumentNullException(nameof(estudio));

            if (!EsHorarioDeAtencion(fechaHora))
                throw new InvalidOperationException("El horario seleccionado está fuera del horario de atención.");

            // Tiene que ser en el futuro: no alcanza con que sea hoy.
            if (fechaHora <= DateTime.Now)
                throw new InvalidOperationException("No se puede sacar un turno para una fecha u hora que ya pasó.");

            var dni = NormalizarDni(paciente.Dni);
            if (!EsDniValido(dni))
                throw new ArgumentException("El DNI debe contener entre 7 y 8 dígitos.", nameof(paciente));

            if (string.IsNullOrWhiteSpace(paciente.Nombre) || string.IsNullOrWhiteSpace(paciente.Apellido))
                throw new ArgumentException("El nombre y el apellido son obligatorios.", nameof(paciente));

            // Paciente, estudio y turno se guardan juntos o no se guarda nada.
            // Si algo falla antes del Commit, al salir la transacción se deshace sola.
            using var context = new CentroMedicoContext();
            using var transaccion = context.Database.BeginTransaction();

            if (context.Turnos.Any(t => t.FechaHora == fechaHora))
                throw new InvalidOperationException("El horario seleccionado ya se encuentra ocupado.");

            // Al estudio lo buscamos por Id si ya estaba guardado, o por nombre si es nuevo,
            // así no se duplica en el catálogo.
            var dbEstudio = estudio.Id > 0
                ? context.Estudios.FirstOrDefault(e => e.Id == estudio.Id)
                : context.Estudios.FirstOrDefault(e => e.Nombre == estudio.Nombre);

            if (dbEstudio == null)
            {
                // Un Id que no existe en el catálogo es un error, no un estudio nuevo.
                if (estudio.Id > 0)
                    throw new InvalidOperationException("El estudio indicado no existe en el catálogo.");

                dbEstudio = estudio;
                context.Estudios.Add(dbEstudio);
            }

            var dbPaciente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if (dbPaciente == null)
            {
                // Paciente nuevo: se registra con el DNI normalizado.
                dbPaciente = new Paciente(dni, paciente.Nombre, paciente.Apellido);
                context.Pacientes.Add(dbPaciente);
            }
            else
            {
                // Si el DNI ya existe, el nombre y el apellido tienen que coincidir.
                // Si no coinciden, lo más probable es un error de tipeo o un DNI ajeno.
                bool mismoNombre = string.Equals(dbPaciente.Nombre?.Trim(), paciente.Nombre.Trim(), StringComparison.OrdinalIgnoreCase);
                bool mismoApellido = string.Equals(dbPaciente.Apellido?.Trim(), paciente.Apellido.Trim(), StringComparison.OrdinalIgnoreCase);

                if (!mismoNombre || !mismoApellido)
                    throw new InvalidOperationException(
                        $"El DNI #{dni} ya pertenece a '{dbPaciente.NombreCompleto}'. No se puede registrar con un nombre diferente.");
            }

            var nuevoTurno = new Turno(fechaHora, dbPaciente, dbEstudio);
            context.Turnos.Add(nuevoTurno);

            try
            {
                context.SaveChanges();
                transaccion.Commit();
            }
            catch (DbUpdateException ex)
            {
                // Pasa si otro puesto reservó el mismo horario justo antes que nosotros.
                throw new InvalidOperationException(
                    "No se pudo registrar el turno. Es posible que el horario haya sido ocupado en este momento.", ex);
            }

            return nuevoTurno;
        }

        // Se atiende de 08:00 a 18:00 en bloques de 30 minutos.
        // El último turno empieza a las 17:30; a las 18:00 ya es cierre.
        private static bool EsHorarioDeAtencion(DateTime fechaHora)
        {
            var hora = fechaHora.TimeOfDay;
            bool dentroDelHorario = hora >= TimeSpan.FromHours(8) && hora < TimeSpan.FromHours(18);

            // Sin segundos ni milisegundos sueltos, para que la comparación con los
            // horarios ya ocupados sea exacta.
            bool esBloqueExacto = fechaHora.Minute % 30 == 0 && fechaHora.Second == 0 && fechaHora.Millisecond == 0;

            return dentroDelHorario && esBloqueExacto;
        }

        // ───────────────────────── Resultados ─────────────────────────

        public void RegistrarResultado(Resultado resultado)
        {
            if (resultado == null) throw new ArgumentNullException(nameof(resultado));

            // El turno tiene que existir y estar ya guardado.
            if (resultado.TurnoAsociado == null || resultado.TurnoAsociado.Id <= 0)
                throw new InvalidOperationException("El turno debe estar guardado antes de registrar su resultado.");

            using var context = new CentroMedicoContext();
            int turnoId = resultado.TurnoAsociado.Id;

            if (!context.Turnos.Any(t => t.Id == turnoId))
                throw new InvalidOperationException("El turno asociado no existe.");

            // Un turno no puede tener dos resultados.
            if (context.Resultados.Any(r => r.TurnoId == turnoId))
                throw new InvalidOperationException("Ese turno ya tiene un resultado registrado.");

            context.Turnos.Attach(resultado.TurnoAsociado);
            context.Resultados.Add(resultado);
            context.SaveChanges();
        }

        // Devuelve el resultado listo para retirar de ese DNI: el turno ya ocurrió,
        // el resultado está disponible y todavía no se entregó.
        // Si hay varios, se toma el del turno más reciente.
        public Resultado ConsultarResultadoPendientePorDni(string dni)
        {
            dni = NormalizarDni(dni);
            if (!EsDniValido(dni)) return null;

            using var context = new CentroMedicoContext();
            return ResultadosPendientesQuery(context, dni).AsNoTracking().FirstOrDefault();
        }

        public bool RegistrarRetiroResultado(string dni)
        {
            dni = NormalizarDni(dni);
            if (!EsDniValido(dni))
                throw new ArgumentException("Debe indicar un DNI válido para registrar el retiro.", nameof(dni));

            using var context = new CentroMedicoContext();
            var resultado = ResultadosPendientesQuery(context, dni).FirstOrDefault();

            if (resultado == null)
                throw new KeyNotFoundException($"No se encontró ningún resultado pendiente de retiro para el DNI #{dni}.");

            resultado.MarcarComoRetirado();
            context.SaveChanges();
            return true;
        }

        // Misma operación, pero partiendo del Id del resultado. Se mantiene para cuando
        // ya se tiene el resultado a mano y no hace falta buscarlo por DNI.
        public bool RegistrarRetiroResultado(int idResultado)
        {
            using var context = new CentroMedicoContext();
            var resultado = context.Resultados
                .Include(r => r.TurnoAsociado)
                .FirstOrDefault(r => r.Id == idResultado);

            if (resultado == null)
                throw new KeyNotFoundException($"No se encontró ningún resultado con el ID #{idResultado}.");

            // Mismas reglas que por DNI: el turno tiene que haber ocurrido.
            if (resultado.TurnoAsociado.FechaHora > DateTime.Now)
                throw new InvalidOperationException("El turno todavía no ocurrió: el resultado no puede retirarse.");

            resultado.MarcarComoRetirado();
            context.SaveChanges();
            return true;
        }

        // ───────────────────────── Estadísticas ─────────────────────────

        // Cantidad de turnos reservados por estudio. Sin turnos, devuelve un diccionario vacío.
        public Dictionary<string, int> ObtenerEstadisticasTurnosPorEstudio()
        {
            using var context = new CentroMedicoContext();
            return context.Turnos
                .AsNoTracking()
                .GroupBy(t => t.Estudio.Nombre)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // ───────────────────────── Auxiliares ─────────────────────────

        // Resultados de un DNI que ya se pueden entregar, del turno más reciente al más antiguo.
        private static IQueryable<Resultado> ResultadosPendientesQuery(CentroMedicoContext context, string dniNormalizado)
        {
            var ahora = DateTime.Now;

            return context.Resultados
                .Include(r => r.TurnoAsociado).ThenInclude(t => t.Paciente)
                .Include(r => r.TurnoAsociado).ThenInclude(t => t.Estudio)
                .Where(r => r.TurnoAsociado.Paciente.Dni == dniNormalizado
                         && r.TurnoAsociado.FechaHora <= ahora
                         && r.Disponible
                         && !r.Retirado)
                .OrderByDescending(r => r.TurnoAsociado.FechaHora);
        }
    }
}