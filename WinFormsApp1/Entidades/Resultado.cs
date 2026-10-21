using System;

namespace WinFormsApp1
{
    public class Resultado
    {
        public int Id { get; private set; }
        public int TurnoId { get; private set; }
        public Turno TurnoAsociado { get; private set; }
        public bool Disponible { get; private set; }
        public bool Retirado { get; private set; }

        private Resultado() { }

        public Resultado(Turno turnoAsociado, bool disponible)
        {
            TurnoAsociado = turnoAsociado ?? throw new ArgumentNullException(nameof(turnoAsociado));
            TurnoId = turnoAsociado.Id;
            Disponible = disponible;
            Retirado = false;
        }

        public void MarcarComoRetirado()
        {
            if (!Disponible) throw new InvalidOperationException("El resultado aún no se encuentra disponible para su retiro.");
            if (Retirado) throw new InvalidOperationException("Este resultado ya fue retirado previamente.");

            Retirado = true;
        }
    }
}