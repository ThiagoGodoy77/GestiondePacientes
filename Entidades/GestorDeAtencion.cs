using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionPacientesUI
{
    public class GestorDeAtencion : IGestorDeAtencion
    {
        private List<Paciente> pacientes;
        private List<Turno> turnos;
        private List<Resultado> resultados;

        public GestorDeAtencion()
        {
            pacientes = new List<Paciente>();
            turnos = new List<Turno>();
            resultados = new List<Resultado>();
        }

        public void RegistrarPaciente(Paciente paciente) => pacientes.Add(paciente);

        public void RegistrarResultado(Resultado resultado) => resultados.Add(resultado);

        public Turno ConsultarTurno(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI no puede estar vacio.");

            return turnos.FirstOrDefault(t => t.Paciente.Dni == dni);
        }

        public List<DateTime> ObtenerHorariosDisponibles(DateTime fecha)
        {
            List<DateTime> horariosPosibles = new List<DateTime>();
            DateTime inicioAtencion = fecha.Date.AddHours(8);
            DateTime finAtencion = fecha.Date.AddHours(18);

            while (inicioAtencion < finAtencion)
            {
                if (!turnos.Any(t => t.FechaHora == inicioAtencion))
                {
                    horariosPosibles.Add(inicioAtencion);
                }
                inicioAtencion = inicioAtencion.AddMinutes(30);
            }

            return horariosPosibles;
        }

        public Turno SacarTurno(Paciente paciente, Estudio estudio, DateTime fechaHora)
        {
            if (paciente == null || estudio == null)
                throw new ArgumentNullException("Paciente y Estudio son obligatorios.");

            if (turnos.Any(t => t.FechaHora == fechaHora))
                throw new InvalidOperationException("El horario seleccionado ya esta ocupado.");

            Turno nuevoTurno = new Turno(fechaHora, paciente, estudio);
            turnos.Add(nuevoTurno);
            return nuevoTurno;
        }

        public bool RegistrarRetiroResultado(int idResultado)
        {
            Resultado resultado = resultados.FirstOrDefault(r => r.Id == idResultado);

            if (resultado == null)
                throw new KeyNotFoundException("Resultado no encontrado.");

            if (resultado.Disponible && !resultado.Retirado)
            {
                resultado.MarcarComoRetirado();
                return true;
            }

            return false;
        }
    }
}