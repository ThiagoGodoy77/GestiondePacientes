using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.CentroMedico.WinForms.Forms
{
    // Ventana de reportes: muestra cuántos turnos tiene registrados cada
    // estudio médico. Solo se llega acá después de ingresar el PIN de
    // administración correcto (ver PinForm y Form1.btnVerEstadisticas_Click).
    public class EstadisticasForm : Form
    {
        public EstadisticasForm(IReadOnlyDictionary<string, int> estadisticas)
        {
            Text = "Estadísticas de Turnos por Estudio";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 320);
            BackColor = Color.White;

            var lblTitulo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 20),
                Text = "Turnos registrados por estudio"
            };

            var lstEstadisticas = new ListBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 55),
                Size = new Size(380, 190)
            };

            if (estadisticas == null || estadisticas.Count == 0)
            {
                lstEstadisticas.Items.Add("Todavía no hay turnos registrados.");
            }
            else
            {
                foreach (var item in estadisticas)
                    lstEstadisticas.Items.Add($"{item.Key}: {item.Value} turno(s)");
            }

            var btnCerrar = new Button
            {
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(270, 260),
                Size = new Size(130, 35),
                Text = "Cerrar"
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (_, _) => Close();

            Controls.Add(lblTitulo);
            Controls.Add(lstEstadisticas);
            Controls.Add(btnCerrar);
        }
    }
}