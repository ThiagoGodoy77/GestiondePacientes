using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public class GestorDeAtencion : IGestorDeAtencion
    {
        private static readonly TimeSpan InicioAtencion = TimeSpan.FromHours(8);
        private static readonly TimeSpan FinAtencion = TimeSpan.FromHours(18);
        private static readonly TimeSpan DuracionTurno = TimeSpan.FromMinutes(30);

        public GestorDeAtencion()
        {
            using var context = new CentroMedicoContext();
            context.Database.EnsureCreated();
        }

        // ───────────────────────── Utilidades de DNI ─────────────────────────

        public static string NormalizarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return string.Empty;

            return new string(dni.Where(char.IsDigit).ToArray());
        }

        public static bool EsDniValido(string dniNormalizado)
        {
            return dniNormalizado.Length >= 7 && dniNormalizado.Length <= 8;
        }

        // ───────────────────────── Pacientes ─────────────────────────

        public void RegistrarPaciente(Paciente paciente)
        {
            if (paciente == null)
                throw new ArgumentNullException(nameof(paciente));

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
            return context.Pacientes
                .AsNoTracking()
                .FirstOrDefault(p => p.Dni == dni);
        }

        // ───────────────────────── Turnos ─────────────────────────

        public List<Turno> ConsultarTurnos(string dni)
        {
            dni = NormalizarDni(dni);
            if (dni.Length == 0)
                return new List<Turno>();

            using var context = new CentroMedicoContext();
            return context.Turnos
                .AsNoTracking()
                .Include(t => t.Paciente)
                .Include(t => t.Estudio)
                .Where(t => t.Paciente.Dni == dni && t.FechaHora >= DateTime.Today)
                .OrderBy(t => t.FechaHora)
                .ToList();
        }

        public List<DateTime> ObtenerHorariosDisponibles(DateTime fecha)
        {
            var inicioDia = fecha.Date;
            var finDia = inicioDia.AddDays(1);

            using var context = new CentroMedicoContext();

            var ocupados = context.Turnos
                .AsNoTracking()
                .Where(t => t.FechaHora >= inicioDia && t.FechaHora < finDia)
                .Select(t => t.FechaHora)
                .ToHashSet();

            var ahora = DateTime.Now;
            var disponibles = new List<DateTime>();

            for (var horario = inicioDia + InicioAtencion; horario < inicioDia + FinAtencion; horario += DuracionTurno)
            {
                if (horario > ahora && !ocupados.Contains(horario))
                    disponibles.Add(horario);
            }

            return disponibles;
        }

        public Turno SacarTurno(Paciente paciente, Estudio estudio, DateTime fechaHora)
        {
            if (paciente == null)
                throw new ArgumentNullException(nameof(paciente));
            if (estudio == null)
                throw new ArgumentNullException(nameof(estudio));
            if (!EsHorarioDeAtencion(fechaHora))
                throw new InvalidOperationException("El horario seleccionado está fuera del horario de atención.");
            if (fechaHora <= DateTime.Now)
                throw new InvalidOperationException("No se puede sacar un turno para una fecha u hora que ya pasó.");

            using var context = new CentroMedicoContext();
            using var transaccion = context.Database.BeginTransaction();

            if (context.Turnos.Any(t => t.FechaHora == fechaHora))
                throw new InvalidOperationException("El horario seleccionado ya se encuentra ocupado.");

            // Buscar Estudio por ID o por Nombre; si no existe, registrarlo automáticamente
            var dbEstudio = estudio.Id > 0
                ? context.Estudios.FirstOrDefault(e => e.Id == estudio.Id)
                : context.Estudios.FirstOrDefault(e => e.Nombre == estudio.Nombre);

            if (dbEstudio == null)
            {
                dbEstudio = estudio;
                context.Estudios.Add(dbEstudio);
            }

            // Buscar o Registrar Paciente
            var dni = NormalizarDni(paciente.Dni);
            var dbPaciente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);
            if (dbPaciente == null)
            {
                dbPaciente = paciente;
                context.Pacientes.Add(dbPaciente);
            }
            else
            {
                dbPaciente.ActualizarDatosPersonales(paciente.Nombre, paciente.Apellido);
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
                throw new InvalidOperationException(
                    "No se pudo registrar el turno. Es posible que el horario haya sido ocupado en este momento.", ex);
            }

            return nuevoTurno;
        }

        private static bool EsHorarioDeAtencion(DateTime fechaHora)
        {
            var horaDelDia = fechaHora.TimeOfDay;

            return horaDelDia >= InicioAtencion
                && horaDelDia < FinAtencion
                && (horaDelDia - InicioAtencion).Ticks % DuracionTurno.Ticks == 0;
        }

        // ───────────────────────── Resultados ─────────────────────────

        public void RegistrarResultado(Resultado resultado)
        {
            if (resultado == null)
                throw new ArgumentNullException(nameof(resultado));
            if (resultado.TurnoAsociado == null)
                throw new ArgumentException("El resultado debe estar asociado a un turno.", nameof(resultado));

            var turnoId = resultado.TurnoAsociado.Id;

            using var context = new CentroMedicoContext();

            if (!context.Turnos.Any(t => t.Id == turnoId))
                throw new KeyNotFoundException($"No existe el turno #{turnoId}.");
            if (context.Resultados.Any(r => r.TurnoAsociado.Id == turnoId))
                throw new InvalidOperationException($"El turno #{turnoId} ya tiene un resultado registrado.");

            context.Attach(resultado.TurnoAsociado);
            context.Resultados.Add(resultado);
            context.SaveChanges();
        }

        public Resultado ConsultarResultadoPendientePorDni(string dni)
        {
            dni = NormalizarDni(dni);
            if (dni.Length == 0)
                return null;

            using var context = new CentroMedicoContext();
            return ResultadosPendientes(context, dni)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public bool RegistrarRetiroResultado(int idResultado)
        {
            using var context = new CentroMedicoContext();

            var resultado = context.Resultados.Find(idResultado);
            if (resultado == null)
                throw new KeyNotFoundException($"No se encontró ningún resultado registrado con el ID #{idResultado}.");

            resultado.MarcarComoRetirado();
            context.SaveChanges();
            return true;
        }

        public bool RegistrarRetiroResultado(string dni)
        {
            dni = NormalizarDni(dni);
            if (dni.Length == 0)
                throw new ArgumentException("El DNI no puede estar vacío.", nameof(dni));

            using var context = new CentroMedicoContext();

            var resultado = ResultadosPendientes(context, dni).FirstOrDefault();
            if (resultado == null)
                throw new KeyNotFoundException(
                    $"No se encontró ningún resultado disponible pendiente de retiro para el paciente con DNI #{dni}.");

            resultado.MarcarComoRetirado();
            context.SaveChanges();
            return true;
        }

        private static IQueryable<Resultado> ResultadosPendientes(CentroMedicoContext context, string dniNormalizado)
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