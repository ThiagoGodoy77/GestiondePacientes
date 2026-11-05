using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.CentroMedico.Data.Entidades;

namespace WinFormsApp1.CentroMedico.Business.Interfaces
{
    public interface IGestorDeAtencion
    {
        // ───────────────────────── Pacientes ─────────────────────────
        void RegistrarPaciente(Paciente paciente);
        Paciente ConsultarPaciente(string dni);

        // ───────────────────────── Turnos y Agenda ─────────────────────────
        List<Turno> ConsultarTurnos(string dni);
        List<DateTime> ObtenerHorariosDisponibles(DateTime fecha);
        Turno SacarTurno(Paciente paciente, Estudio estudio, DateTime fechaHora);
        List<Turno> ObtenerTurnosPendientesDelDia(DateTime? fecha = null);

        // ───────────────────────── Resultados y Estadísticas ─────────────────────────
        void RegistrarResultado(Resultado resultado);
        Resultado ConsultarResultadoPendientePorDni(string dni);
        bool RegistrarRetiroResultado(int idResultado);
        bool RegistrarRetiroResultado(string dni);
        Dictionary<string, int> ObtenerEstadisticasTurnosPorEstudio();

    }
}