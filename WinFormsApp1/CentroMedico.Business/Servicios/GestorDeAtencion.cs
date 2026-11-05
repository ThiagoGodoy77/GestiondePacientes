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
            using var context = new CentroMedicoContext();
            context.Database.EnsureCreated();
        }

        public static string NormalizarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return string.Empty;
            return new string(dni.Where(char.IsDigit).ToArray());
        }

        public static bool EsDniValido(string dni)
        {
            return !string.IsNullOrEmpty(dni)
                && dni.Length is >= 7 and <= 8
                && dni.All(char.IsDigit);
        }

        public void RegistrarPaciente(Paciente paciente)
        {
            if (paciente == null) throw new ArgumentNullException(nameof(paciente));

            using var context = new CentroMedicoContext();
            var dni = NormalizarDni(paciente.Dni);
            var existente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if (existente == null)
                context.Pacientes.Add(paciente);
            else
                existente.ActualizarDatosPersonales(paciente.Nombre, paciente.Apellido);

            context.SaveChanges();
        }

        public Paciente ConsultarPaciente(string dni)
        {
            dni = NormalizarDni(dni);
            using var context = new CentroMedicoContext();
            return context.Pacientes.AsNoTracking().FirstOrDefault(p => p.Dni == dni);
        }

        public void RegistrarResultado(Resultado resultado)
        {
            if (resultado == null) throw new ArgumentNullException(nameof(resultado));

            using var context = new CentroMedicoContext();

            if (resultado.TurnoAsociado != null)
            {
                // Un mismo turno no puede tener dos resultados cargados.
                bool yaTieneResultado = context.Resultados.Any(r => r.TurnoId == resultado.TurnoAsociado.Id);
                if (yaTieneResultado)
                    throw new InvalidOperationException("Ese turno ya tiene un resultado registrado.");

                context.Turnos.Attach(resultado.TurnoAsociado);
            }

            context.Resultados.Add(resultado);
            context.SaveChanges();
        }

        // Turnos futuros de un paciente, del más próximo al más lejano.
        // Los turnos que ya pasaron no interesan para esta consulta.
        public List<Turno> ConsultarTurnos(string dni)
        {
            dni = NormalizarDni(dni);

            using var context = new CentroMedicoContext();
            return context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Estudio)
                .Where(t => t.Paciente.Dni == dni && t.FechaHora >= DateTime.Today)
                .OrderBy(t => t.FechaHora)
                .ToList();
        }

        // Todos los turnos de un día puntual (de cualquier paciente), en orden
        // cronológico. Pensado para una agenda diaria, no para un paciente en particular.
        // Si no se pasa fecha, se usa el día de hoy por defecto.
        public List<Turno> ObtenerTurnosPendientesDelDia(DateTime? fecha)
        {
            var fechaConsulta = (fecha ?? DateTime.Today).Date;
            using var context = new CentroMedicoContext();
            return context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Estudio)
                .Where(t => t.FechaHora.Date == fechaConsulta)
                .OrderBy(t => t.FechaHora)
                .ToList();
        }

        public List<DateTime> ObtenerHorariosDisponibles(DateTime fecha)
        {
            var horariosPosibles = new List<DateTime>();
            DateTime inicioAtencion = fecha.Date.AddHours(8);
            DateTime finAtencion = fecha.Date.AddHours(18);

            using var context = new CentroMedicoContext();
            var turnosOcupados = context.Turnos
                .Where(t => t.FechaHora.Date == fecha.Date)
                .Select(t => t.FechaHora)
                .ToList();

            while (inicioAtencion < finAtencion)
            {
                if (!turnosOcupados.Contains(inicioAtencion))
                    horariosPosibles.Add(inicioAtencion);

                inicioAtencion = inicioAtencion.AddMinutes(30);
            }

            return horariosPosibles;
        }

        public Turno SacarTurno(Paciente paciente, Estudio estudio, DateTime fechaHora)
        {
            if (paciente == null) throw new ArgumentNullException(nameof(paciente));
            if (estudio == null) throw new ArgumentNullException(nameof(estudio));

            if (!EsHorarioDeAtencion(fechaHora))
                throw new InvalidOperationException("El horario seleccionado está fuera del horario de atención.");

            // La fecha/hora tiene que ser estrictamente futura (no alcanza con que sea hoy).
            if (fechaHora <= DateTime.Now)
                throw new InvalidOperationException("No se puede sacar un turno para una fecha u hora que ya pasó.");

            using var context = new CentroMedicoContext();
            using var transaccion = context.Database.BeginTransaction();

            if (context.Turnos.Any(t => t.FechaHora == fechaHora))
                throw new InvalidOperationException("El horario seleccionado ya se encuentra ocupado.");

            // Buscar el estudio por Id (si ya fue guardado) o por nombre (para no duplicar).
            var dbEstudio = estudio.Id > 0
                ? context.Estudios.FirstOrDefault(e => e.Id == estudio.Id)
                : context.Estudios.FirstOrDefault(e => e.Nombre == estudio.Nombre);

            if (dbEstudio == null)
            {
                dbEstudio = estudio;
                context.Estudios.Add(dbEstudio);
            }

            // Buscar el paciente por DNI.
            var dni = NormalizarDni(paciente.Dni);
            var dbPaciente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if (dbPaciente == null)
            {
                // Paciente nuevo: se registra en la BD.
                dbPaciente = paciente;
                context.Pacientes.Add(dbPaciente);
            }
            else
            {
                // Paciente existente: el nombre y apellido deben coincidir con lo que
                // ya está registrado. Si no coinciden, es probable que haya un error
                // de tipeo o que el DNI ingresado no le pertenezca a esa persona.
                bool nombreCoincide = string.Equals(dbPaciente.Nombre.Trim(), paciente.Nombre.Trim(), StringComparison.OrdinalIgnoreCase);
                bool apellidoCoincide = string.Equals(dbPaciente.Apellido.Trim(), paciente.Apellido.Trim(), StringComparison.OrdinalIgnoreCase);

                if (!nombreCoincide || !apellidoCoincide)
                    //en esta excepcion podriamos agregar el nombre y apellido que se intento registrar para que el usuario vea cual es la diferencia '{dbPaciente.NombreCompleto}' 
                    throw new InvalidOperationException(
                        $"El DNI #{dni} ya pertenece a otro paciente. No se puede registrar con un nombre diferente.");
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
                // Puede pasar si dos personas intentan sacar el mismo horario al mismo tiempo.
                throw new InvalidOperationException(
                    "No se pudo registrar el turno. Es posible que el horario haya sido ocupado en este momento.", ex);
            }

            return nuevoTurno;
        }

        // El centro atiende de 8:00 a 18:00, en bloques de 30 minutos.
        private static bool EsHorarioDeAtencion(DateTime fechaHora)
        {
            var hora = fechaHora.TimeOfDay;
            bool dentroDelRango = hora >= TimeSpan.FromHours(8) && hora < TimeSpan.FromHours(18);
            bool enBloqueDeMediaHora = fechaHora.Minute % 30 == 0 && fechaHora.Second == 0;
            return dentroDelRango && enBloqueDeMediaHora;
        }

        public Resultado ConsultarResultadoPendientePorDni(string dni)
        {
            dni = NormalizarDni(dni);
            if (string.IsNullOrWhiteSpace(dni)) return null;

            using var context = new CentroMedicoContext();
            return ResultadosPendientesQuery(context, dni).FirstOrDefault();
        }

        public bool RegistrarRetiroResultado(string dni)
        {
            dni = NormalizarDni(dni);
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("Debe indicar un DNI válido para registrar el retiro.", nameof(dni));

            using var context = new CentroMedicoContext();
            var resultado = ResultadosPendientesQuery(context, dni).FirstOrDefault();

            if (resultado == null)
                throw new KeyNotFoundException($"No se encontró ningún resultado pendiente de retiro para el DNI #{dni}.");

            resultado.MarcarComoRetirado();
            context.SaveChanges();
            return true;
        }

        // Variante por Id: la usa la pantalla de resultados, que ya tiene el
        // resultado concreto a mano (no necesita volver a buscarlo por DNI).
        public bool RegistrarRetiroResultado(int resultadoId)
        {
            using var context = new CentroMedicoContext();
            var resultado = context.Resultados
                .Include(r => r.TurnoAsociado).ThenInclude(t => t.Paciente)
                .FirstOrDefault(r => r.Id == resultadoId);

            if (resultado == null)
                throw new KeyNotFoundException($"No se encontró ningún resultado con el ID #{resultadoId}.");

            resultado.MarcarComoRetirado();
            context.SaveChanges();
            return true;
        }

        public Dictionary<string, int> ObtenerEstadisticasTurnosPorEstudio()
        {
            using var context = new CentroMedicoContext();
            return context.Turnos
                .Include(t => t.Estudio)
                .GroupBy(t => t.Estudio.Nombre)
                .ToDictionary(g => g.Key, g => g.Count());
        }

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