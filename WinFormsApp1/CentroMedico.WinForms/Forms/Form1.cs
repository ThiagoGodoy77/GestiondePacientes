using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.CentroMedico.Business.Interfaces;
using WinFormsApp1.CentroMedico.Business.Servicios;
using WinFormsApp1.CentroMedico.Data.Datos;
using WinFormsApp1.CentroMedico.Data.Entidades;
using WinFormsApp1.CentroMedico.WinForms.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private static readonly Color ColorExito = Color.FromArgb(39, 174, 96);
        private static readonly Color ColorError = Color.FromArgb(192, 57, 43);

        private IGestorDeAtencion gestor;
        private List<Estudio> catalogoEstudios = new List<Estudio>();
        private DateTime fechaTurno = DateTime.Today.AddDays(1);

        public Form1()
        {
            InitializeComponent();
        }

        // La inicialización va en OnLoad (y no en el constructor) para que no corra
        // dentro del diseñador de Visual Studio ni congele la ventana al construirla.
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            try
            {
                gestor = new GestorDeAtencion();   // crea la base si no existe
                CargarDatosSemilla();

                CargarEstudios();
                ActualizarFechaTurno();
                ActualizarHorarios();
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo inicializar la base de datos:\n\n" + ex.Message);
            }
        }

        // ───────────────────────── Datos de prueba ─────────────────────────

        private void CargarDatosSemilla()
        {
            using var context = new CentroMedicoContext();

            // 1. Catalogo de estudios
            if (!context.Estudios.Any())
            {
                context.Estudios.AddRange(
                    new Estudio("Radiografía de Tórax", "Radiología General", "Dr. López", "Marta"),
                    new Estudio("Ecografía Abdominal", "Ultrasonografía", "Dra. Gómez", "Luis"),
                    new Estudio("Análisis de Sangre Completo", "Laboratorio Central", "Dra. Martínez", "Carlos"));
                context.SaveChanges();
            }

            // 2. Pacientes de prueba
            var pacienteRetirado = ObtenerOCrearPaciente(context, "12345678", "Juan", "Pérez");
            var pacientePendiente = ObtenerOCrearPaciente(context, "87654321", "María", "Gómez");
            context.SaveChanges();

            // 3. Resultados de prueba
            if (!context.Resultados.Any())
            {
                // Se toma el estudio desde este mismo contexto para que EF no intente insertarlo de nuevo.
                var estudio = context.Estudios.OrderBy(x => x.Id).First();

                // Turnos del día anterior: el estudio ya "transcurrió" a cualquier hora
                // y no choca con los horarios disponibles de mañana.
                var ayer = DateTime.Today.AddDays(-1);

                // Paciente 1: resultado ya retirado.
                var turnoRetirado = new Turno(ayer.AddHours(9), pacienteRetirado, estudio);
                var resultadoRetirado = new Resultado(turnoRetirado, disponible: true);
                resultadoRetirado.MarcarComoRetirado();

                // Paciente 2: resultado disponible, pendiente de retiro.
                var turnoPendiente = new Turno(ayer.AddHours(11), pacientePendiente, estudio);
                var resultadoPendiente = new Resultado(turnoPendiente, disponible: true);

                context.Turnos.AddRange(turnoRetirado, turnoPendiente);
                context.Resultados.AddRange(resultadoRetirado, resultadoPendiente);
                context.SaveChanges();
            }

            catalogoEstudios = context.Estudios.AsNoTracking().OrderBy(x => x.Id).ToList();
        }

        private static Paciente ObtenerOCrearPaciente(CentroMedicoContext context, string dni, string nombre, string apellido)
        {
            var paciente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);
            if (paciente == null)
            {
                paciente = new Paciente(dni, nombre, apellido);
                context.Pacientes.Add(paciente);
            }
            return paciente;
        }

        // ───────────────────────── Interfaz ─────────────────────────

        private void CargarEstudios()
        {
            cmbEstudios.DisplayMember = "Nombre";
            cmbEstudios.DataSource = catalogoEstudios;
        }

        private void ActualizarHorarios()
        {
            cmbHorarios.FormattingEnabled = true;
            cmbHorarios.FormatString = "HH:mm";
            cmbHorarios.DataSource = gestor.ObtenerHorariosDisponibles(fechaTurno);
        }

        private void ActualizarFechaTurno()
        {
            txtFechaTurno.Text = fechaTurno.ToString("dd/MM/yyyy");
        }

        private void btnSeleccionarFecha_Click(object? sender, EventArgs e)
        {
            using var ventanaFecha = new SeleccionarFechaForm(fechaTurno);

            if (ventanaFecha.ShowDialog(this) != DialogResult.OK)
                return;

            fechaTurno = ventanaFecha.FechaSeleccionada;
            ActualizarFechaTurno();
            ActualizarHorarios();
        }

        
        private void MostrarTurno(string dni)
        {
            var turnos = gestor.ConsultarTurnos(dni);

            if (turnos != null && turnos.Any())
            {
                var turnosTexto = turnos.Select(t =>
                {
                    string descripcion =
                        $"{t.FechaHora:dd/MM HH:mm} hs - {t.Estudio.Nombre}";

                    if (!string.IsNullOrWhiteSpace(t.Estudio.Profesional))
                        descripcion += $" - {t.Estudio.Profesional}";

                    if (!string.IsNullOrWhiteSpace(t.Estudio.Asistente))
                        descripcion += $" - Asistente: {t.Estudio.Asistente}";

                    return descripcion;
                }).ToList();

                using var ventana = new ConsultaTurnosForm(
                    turnos.First().Paciente.NombreCompleto,
                    turnosTexto);

                ventana.ShowDialog(this);
            }
            else
            {
                MostrarAdvertencia("No se encontraron turnos próximos para el DNI ingresado.");
            }
        }


        // ───────────────────────── Consultar turno ─────────────────────────

        private void btnConsultarTurno_Click(object sender, EventArgs e)
        {
            string dniBusqueda = txtDniConsulta.Text.Trim();
            if (string.IsNullOrWhiteSpace(dniBusqueda))
            {
                MessageBox.Show("Por favor, ingrese un número de DNI para consultar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MostrarTurno(dniBusqueda);
        }

        // ───────────────────────── Sacar turno ─────────────────────────

        private void btnSacarTurno_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreTurno.Text.Trim();
            string apellido = txtApellidoTurno.Text.Trim();

            if (!TryObtenerDni(txtDniTurno.Text, out var dni))
                return;

            if (nombre.Length == 0 || apellido.Length == 0)
            {
                MostrarAdvertencia("Por favor, complete el nombre y el apellido del paciente.");
                return;
            }

            if (cmbEstudios.SelectedItem is not Estudio estudio || cmbHorarios.SelectedItem is not DateTime horario)
            {
                MostrarAdvertencia("Seleccione un estudio médico y un horario disponible.");
                return;
            }

            try
            {
                var pacienteIngresado = new Paciente(dni, nombre, apellido);

                // Guardamos el objeto Turno devuelto por la lógica de negocio
                var turnoRegistrado = gestor.SacarTurno(pacienteIngresado, estudio, horario);

                // Usamos turnoRegistrado.Paciente.NombreCompleto para mostrar el nombre real en la BD
                MostrarInformacion($"Turno registrado exitosamente para {turnoRegistrado.Paciente.NombreCompleto} el {horario:dd/MM} a las {horario:HH:mm} hs.");

                LimpiarFormularioDeTurno();
                txtDniConsulta.Text = dni;
                MostrarTurno(dni);
                ActualizarHorarios();
            }
            catch (InvalidOperationException ex)
            {
                MostrarAdvertencia(ex.Message);
                ActualizarHorarios();
            }
            catch (Exception ex)
            {
                MostrarError("Ocurrió un error inesperado al registrar el turno:\n\n" + ex.Message);
            }
        }

        private void LimpiarFormularioDeTurno()
        {
            txtDniTurno.Clear();
            txtNombreTurno.Clear();
            txtApellidoTurno.Clear();
            txtDniTurno.Focus();
        }

        // ───────────────────────── Retirar resultado ─────────────────────────

        private void btnRetirarResultado_Click(object sender, EventArgs e)
        {
            if (!TryObtenerDni(txtDniResultado.Text, out var dni))
                return;

            try
            {
                // 1. Buscar un estudio realizado con resultado pendiente de retiro.
                var resultado = gestor.ConsultarResultadoPendientePorDni(dni);

                if (resultado == null)
                {
                    MessageBox.Show(
                        $"No se encontró ningún estudio realizado pendiente de retiro para el DNI #{dni}.\n\n" +
                        "Verifique que el paciente tenga un turno registrado y que la fecha de atención ya haya transcurrido.",
                        "Resultado no disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // 2. Confirmar con los datos reales antes de entregar.
                var turno = resultado.TurnoAsociado;
                var confirmacion = MessageBox.Show(
                    "Se encontró un resultado listo para entregar:\n\n" +
                    $"• Paciente: {turno.Paciente.NombreCompleto} (DNI: {turno.Paciente.Dni})\n" +
                    $"• Estudio: {turno.Estudio.Nombre}\n" +
                    $"• Fecha de atención: {turno.FechaHora:dd/MM/yyyy HH:mm} hs\n\n" +
                    "¿Desea confirmar la entrega y registrar el retiro?",
                    "Confirmar retiro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                    return;

                // 3. Registrar el retiro.
                gestor.RegistrarRetiroResultado(resultado.Id);
                MostrarInformacion($"El retiro del resultado de {turno.Paciente.NombreCompleto} fue registrado exitosamente.");
                txtDniResultado.Clear();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        // ───────────────────────── Sección de Reportes ─────────────────────────

        private void btnVerEstadisticas_Click(object sender, EventArgs e)
        {
            using var pinForm = new PinForm();

            // Si el usuario presiona "Ingresar" en la ventana del PIN
            if (pinForm.ShowDialog(this) == DialogResult.OK)
            {
                // Validar el PIN estático
                if (pinForm.PinIngresado == "1234")
                {
                    var estadisticas = gestor.ObtenerEstadisticasTurnosPorEstudio();
                    using var reporte = new EstadisticasForm(estadisticas);
                    reporte.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("PIN incorrecto. Acceso denegado a los reportes.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // ───────────────────────── Helpers ─────────────────────────

        /// <summary>
        /// Normaliza y valida el DNI. Si no es válido, avisa al usuario y devuelve false. -sacado de ia 
        /// </summary>
        private bool TryObtenerDni(string textoIngresado, out string dni)
        {
            dni = GestorDeAtencion.NormalizarDni(textoIngresado);

            if (dni.Length == 0)
            {
                MostrarAdvertencia("Por favor, ingrese un número de DNI.");
                return false;
            }

            if (!GestorDeAtencion.EsDniValido(dni))
            {
                MostrarAdvertencia("El DNI debe tener 7 u 8 dígitos.");
                return false;
            }

            return true;
        }

        private static void MostrarAdvertencia(string mensaje) =>
            MessageBox.Show(mensaje, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private static void MostrarInformacion(string mensaje) =>
            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

    }
}