using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly IGestorDeAtencion gestor;
        private List<Estudio> catalogoEstudios;

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
            using var context = new CentroMedicoContext();

            // 1. Catálogo de Estudios
            if (!context.Estudios.Any())
            {
                context.Estudios.Add(new Estudio("Radiografía de Tórax", "Radiología General", "Dr. López", "Marta"));
                context.Estudios.Add(new Estudio("Ecografía Abdominal", "Ultrasonografía", "Dra. Gómez", "Luis"));
                context.Estudios.Add(new Estudio("Análisis de Sangre Completo", "Laboratorio Central", "Dra. Martínez", "Carlos"));
                context.SaveChanges();
            }

            catalogoEstudios = context.Estudios.ToList();

            // 2. Paciente de prueba
            var pacienteTest = context.Pacientes.FirstOrDefault(p => p.Dni == "12345678");
            if (pacienteTest == null)
            {
                pacienteTest = new Paciente("12345678", "Juan", "Pérez");
                context.Pacientes.Add(pacienteTest);
                context.SaveChanges();
            }

            // 3. Resultados Semilla
            if (!context.Resultados.Any())
            {
                var estudio = catalogoEstudios.First();

                var turno1 = new Turno(DateTime.Today.AddHours(9), pacienteTest, estudio);
                context.Turnos.Add(turno1);
                context.SaveChanges();

                var res1 = new Resultado(turno1, disponible: true);
                context.Resultados.Add(res1);

                var turno2 = new Turno(DateTime.Today.AddHours(10), pacienteTest, estudio);
                context.Turnos.Add(turno2);
                context.SaveChanges();

                var res2 = new Resultado(turno2, disponible: true);
                res2.MarcarComoRetirado();
                context.Resultados.Add(res2);

                context.SaveChanges();
            }
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
            string dniBusqueda = txtDniConsulta.Text.Trim();
            if (string.IsNullOrWhiteSpace(dniBusqueda))
            {
                MessageBox.Show("Por favor, ingrese un número de DNI para consultar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var turno = gestor.ConsultarTurno(dniBusqueda);
            if (turno != null)
            {
                lblResultadoConsulta.Text = $"Turno: {turno.FechaHora:HH:mm} hs - {turno.Estudio.Nombre} | Paciente: {turno.Paciente.NombreCompleto}";
                lblResultadoConsulta.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            }
            else
            {
                lblResultadoConsulta.Text = "No se encontraron turnos próximos para el DNI ingresado.";
                lblResultadoConsulta.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            }
        }

        private void btnSacarTurno_Click(object sender, EventArgs e)
        {
            string dni = txtDniTurno.Text.Trim();
            string nombre = txtNombreTurno.Text.Trim();
            string apellido = txtApellidoTurno.Text.Trim();

            if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("Por favor, complete los datos del paciente (DNI, Nombre y Apellido).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbEstudios.SelectedItem == null || cmbHorarios.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estudio médico y un horario disponible.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var paciente = new Paciente(dni, nombre, apellido);
                var estudio = (Estudio)cmbEstudios.SelectedItem;
                var horario = (DateTime)cmbHorarios.SelectedItem;

                gestor.SacarTurno(paciente, estudio, horario);

                MessageBox.Show($"Turno registrado exitosamente para {paciente.NombreCompleto} a las {horario:HH:mm} hs.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtDniConsulta.Text = dni;
                ConfigurarInterfazInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetirarResultado_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIdResultado.Text.Trim(), out int idResultado))
            {
                MessageBox.Show("Ingrese un número válido para el ID del resultado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (gestor.RegistrarRetiroResultado(idResultado))
                {
                    MessageBox.Show($"Resultado #{idResultado} retirado y registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdResultado.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención / Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }

        private void grpConsulta_Enter(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}