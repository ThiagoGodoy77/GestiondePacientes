using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class ConsultaTurnosForm : Form
    {
        public ConsultaTurnosForm(string nombrePaciente, IReadOnlyCollection<string> turnos)
        {
            Text = "Turnos del paciente";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(520, 300);

            var lblPaciente = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Location = new Point(25, 20),
                Text = $"Paciente: {nombrePaciente}"
            };

            var lstTurnos = new ListBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 60),
                Size = new Size(470, 170)
            };

            foreach (var turno in turnos)
                lstTurnos.Items.Add(turno);

            var btnCerrar = new Button
            {
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Location = new Point(365, 245),
                Size = new Size(130, 35),
                Text = "Cerrar"
            };
            btnCerrar.Click += (_, _) => Close();

            Controls.Add(lblPaciente);
            Controls.Add(lstTurnos);
            Controls.Add(btnCerrar);
        }
    }
}
