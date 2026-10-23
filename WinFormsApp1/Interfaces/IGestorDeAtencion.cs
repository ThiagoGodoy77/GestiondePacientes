using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public interface IGestorDeAtencion
    {
        void RegistrarPaciente(Paciente paciente);
        void RegistrarResultado(Resultado resultado);
        Turno ConsultarTurno(string dni);
        List<DateTime> ObtenerHorariosDisponibles(DateTime fecha);
        Turno SacarTurno(Paciente paciente, Estudio estudio, DateTime fechaHora);
        bool RegistrarRetiroResultado(int idResultado);
        Paciente ConsultarPaciente(string dni);
    }
}