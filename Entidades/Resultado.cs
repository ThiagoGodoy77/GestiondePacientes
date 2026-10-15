using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace GestionPacientesUI
{
    public class Resultado
    {
        public int Id { get; private set; }
        public Turno TurnoAsociado { get; private set; }
        public bool Disponible { get; private set; }
        public bool Retirado { get; private set; }

        public Resultado(int id, Turno turnoAsociado, bool disponible)
        {
            Id = id;
            TurnoAsociado = turnoAsociado ?? throw new ArgumentNullException(nameof(turnoAsociado));
            Disponible = disponible;
            Retirado = false;
        }

        public void MarcarComoRetirado()
        {
            if (!Disponible)
                throw new InvalidOperationException("El resultado aun no esta disponible.");
            if (Retirado)
                throw new InvalidOperationException("El resultado ya fue retirado.");

            Retirado = true;
        }
    }
}