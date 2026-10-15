using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionPacientesUI;
using Estudio;

namespace GestionPacientesUI
{
    public partial class Form1 : Form
    {
        private IGestorDeAtencion gestor;
        private List<Estudio> catalogoEstudios;
        private Paciente pacienteFijo;

        public Form1()
        {
            InitializeComponent();

            gestor = new GestorDeAtencion();
            catalogoEstudios = new List<Estudio>();

            CargarDatosSemilla();
            ConfigurarInterfazInicial();
        }

        private void CargarDatosSemilla()
        {
            pacienteFijo = new Paciente("12345678", "Juan", "Perez");
            gestor.RegistrarPaciente(pacienteFijo);

            catalogoEstudios.Add(new Estudio(1, "Radiografia de Torax", "RX", "Dr. Lopez", "Asist. Marta"));
            catalogoEstudios.Add(new Estudio(2, "Ecografia Abdominal", "Eco", "Dra. Gomez", "Asist. Luis"));

            Turno turnoExistente = gestor.SacarTurno(pacienteFijo, catalogoEstudios[0], DateTime.Today.AddHours(10));
            gestor.RegistrarResultado(new Resultado(100, turnoExistente, true));
        }

        private void ConfigurarInterfazInicial()
        {
            cmbEstudios.DataSource = catalogoEstudios;
            cmbEstudios.DisplayMember = "Nombre";

            DateTime fechaBase = DateTime.Today.AddDays(1);
            cmbHorarios.DataSource = gestor.ObtenerHorariosDisponibles(fechaBase);
            cmbHorarios.FormatString = "HH:mm";
        }

        private void btnConsultarTurno_Click(object sender, EventArgs e)
        {
            string dni = txtDniConsulta.Text;
            Turno turno = gestor.ConsultarTurno(dni);

            if (turno != null)
            {
                lblResultadoConsulta.Text = $"Turno: {turno.FechaHora:HH:mm} - {turno.Estudio.Nombre}";
            }
            else
            {
                lblResultadoConsulta.Text = "No se encontraron turnos.";
            }
        }

        private void btnSacarTurno_Click(object sender, EventArgs e)
        {
            if (cmbEstudios.SelectedItem == null || cmbHorarios.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estudio y un horario.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Estudio estudioSeleccionado = (Estudio)cmbEstudios.SelectedItem;
            DateTime horarioSeleccionado = (DateTime)cmbHorarios.SelectedItem;

            try
            {
                gestor.SacarTurno(pacienteFijo, estudioSeleccionado, horarioSeleccionado);
                MessageBox.Show("Turno asignado exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ConfigurarInterfazInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetirarResultado_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIdResultado.Text, out int idResultado))
            {
                MessageBox.Show("Ingrese un ID valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool exito = gestor.RegistrarRetiroResultado(idResultado);
                if (exito)
                {
                    MessageBox.Show("Resultado retirado exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El resultado no esta disponible.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}