using System;

namespace GestionPacientesUI
{
    public class Turno
    {
        public DateTime FechaHora { get; private set; }
        public Paciente Paciente { get; private set; }
        public Estudio Estudio { get; private set; }

        public Turno(DateTime fechaHora, Paciente paciente, Estudio estudio)
        {
            FechaHora = fechaHora;
            Paciente = paciente ?? throw new ArgumentNullException(nameof(paciente));
            Estudio = estudio ?? throw new ArgumentNullException(nameof(estudio));
        }
    }
}