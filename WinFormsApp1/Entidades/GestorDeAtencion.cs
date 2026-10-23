using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public class GestorDeAtencion : IGestorDeAtencion
    {
        public GestorDeAtencion()
        {
            using var context = new CentroMedicoContext();
            context.Database.EnsureCreated();
        }

        public void RegistrarPaciente(Paciente paciente)
        {
            using var context = new CentroMedicoContext();
            var pExistente = context.Pacientes.FirstOrDefault(p => p.Dni == paciente.Dni);
            if (pExistente == null)
            {
                context.Pacientes.Add(paciente);
            }
            else
            {
                pExistente.ActualizarDatosPersonales(paciente.Nombre, paciente.Apellido);
            }
            context.SaveChanges();
        }

        public Paciente ConsultarPaciente(string dni)
        {
            using var context = new CentroMedicoContext();
            return context.Pacientes.FirstOrDefault(p => p.Dni == dni);
        }

        public void RegistrarResultado(Resultado resultado)
        {
            using var context = new CentroMedicoContext();
            if (resultado.TurnoAsociado != null)
            {
                var tExistente = context.Turnos.Find(resultado.TurnoAsociado.Id);
                if (tExistente != null)
                {
                    context.Entry(tExistente).State = EntityState.Unchanged;
                }
            }
            context.Resultados.Add(resultado);
            context.SaveChanges();
        }

        public Turno ConsultarTurno(string dni)
        {
            using var context = new CentroMedicoContext();
            return context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Estudio)
                .OrderByDescending(t => t.FechaHora)
                .FirstOrDefault(t => t.Paciente.Dni == dni && t.FechaHora >= DateTime.Today);
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
            using var context = new CentroMedicoContext();

            if (context.Turnos.Any(t => t.FechaHora == fechaHora))
                throw new InvalidOperationException("El horario seleccionado ya se encuentra ocupado.");

            // Buscar o Registrar Paciente
            var dbPaciente = context.Pacientes.FirstOrDefault(p => p.Dni == paciente.Dni);
            if (dbPaciente == null)
            {
                dbPaciente = paciente;
                context.Pacientes.Add(dbPaciente);
            }
            else
            {
                dbPaciente.ActualizarDatosPersonales(paciente.Nombre, paciente.Apellido);
            }

            // Vincular Estudio existente
            var dbEstudio = context.Estudios.FirstOrDefault(e => e.Id == estudio.Id);
            if (dbEstudio == null)
            {
                dbEstudio = estudio;
                context.Estudios.Add(dbEstudio);
            }

            var nuevoTurno = new Turno(fechaHora, dbPaciente, dbEstudio);
            context.Turnos.Add(nuevoTurno);
            context.SaveChanges();

            return nuevoTurno;
        }

        public bool RegistrarRetiroResultado(int idResultado)
        {
            using var context = new CentroMedicoContext();
            var resultado = context.Resultados.FirstOrDefault(r => r.Id == idResultado);

            if (resultado == null)
                throw new KeyNotFoundException($"No se encontró ningún resultado registrado con el ID #{idResultado}.");

            resultado.MarcarComoRetirado();
            context.SaveChanges();
            return true;
        }
    }
}