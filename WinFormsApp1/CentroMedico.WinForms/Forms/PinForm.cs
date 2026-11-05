using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.CentroMedico.WinForms.Forms
{
    // Ventana simple que pide el PIN de administración antes de dejar ver
    // los reportes. Solo se encarga de pedir el dato y devolverlo: quien
    // la usa (Form1) es quien decide si el PIN ingresado es correcto.
    public class PinForm : Form
    {
        private readonly TextBox txtPin;

        public string PinIngresado { get; private set; } = string.Empty;

        public PinForm()
        {
            Text = "Control de Acceso";
            ClientSize = new Size(300, 170);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            var lblInfo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, 20),
                Text = "Ingrese el PIN de administración:"
            };

            txtPin = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 50),
                PasswordChar = '•',
                Size = new Size(260, 25)
            };

            var btnAceptar = new Button
            {
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Location = new Point(100, 100),
                Size = new Size(90, 35),
                Text = "Ingresar"
            };
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnAceptar.Click += (_, _) =>
            {
                PinIngresado = txtPin.Text;
                DialogResult = DialogResult.OK;
                Close();
            };

            var btnCancelar = new Button
            {
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Location = new Point(190, 100),
                Size = new Size(90, 35),
                Text = "Cancelar"
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(lblInfo);
            Controls.Add(txtPin);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);

            AcceptButton = btnAceptar; // Permite confirmar con "Enter"
            CancelButton = btnCancelar; // Permite cerrar con "Esc"
        }
    }
}