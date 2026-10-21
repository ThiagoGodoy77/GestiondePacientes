using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public class Turno
    {
        public int Id { get; private set; }
        public DateTime FechaHora { get; private set; }
        public string PacienteDni { get; private set; }
        public Paciente Paciente { get; private set; }
        public int EstudioId { get; private set; }
        public Estudio Estudio { get; private set; }

        private Turno() { }

        public Turno(DateTime fechaHora, Paciente paciente, Estudio estudio)
        {
            FechaHora = fechaHora;
            Paciente = paciente ?? throw new ArgumentNullException(nameof(paciente));
            Estudio = estudio ?? throw new ArgumentNullException(nameof(estudio));
            PacienteDni = paciente.Dni;
            EstudioId = estudio.Id;
        }
    }
}