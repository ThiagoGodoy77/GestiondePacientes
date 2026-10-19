using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace GestionPacientesUI
{
	public interface IGestorDeAtencion
	{
		void RegistrarPaciente(Paciente paciente);
		void RegistrarResultado(Resultado resultado);
		Turno ConsultarTurno(string dni);
		List<DateTime> ObtenerHorariosDisponibles(DateTime fecha);
		Turno SacarTurno(Paciente paciente, Estudio estudio, DateTime fechaHora);
		bool RegistrarRetiroResultado(int idResultado);
	}
}