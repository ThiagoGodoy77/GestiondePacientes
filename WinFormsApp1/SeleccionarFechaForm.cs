using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class SeleccionarFechaForm : Form
    {
        public DateTime FechaSeleccionada { get; private set; }

        public SeleccionarFechaForm(DateTime fechaInicial)
        {
            FechaSeleccionada = fechaInicial;
            Text = "Seleccionar fecha del turno";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(330, 330);
            BackColor = Color.White;

            var calendario = new MonthCalendar
            {
                Location = new Point(18, 18),
                MaxSelectionCount = 1,
                MinDate = DateTime.Today
            };
            calendario.SetDate(fechaInicial);

            var btnAceptar = new Button
            {
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(18, 275),
                Size = new Size(140, 38),
                Text = "Aceptar"
            };
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnAceptar.Click += (_, _) =>
            {
                FechaSeleccionada = calendario.SelectionStart.Date;
                DialogResult = DialogResult.OK;
                Close();
            };

            var btnCancelar = new Button
            {
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(172, 275),
                Size = new Size(140, 38),
                Text = "Cancelar"
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(calendario);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
        }
    }
}
