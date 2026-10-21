namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            grpConsulta = new GroupBox();
            lblResultadoConsulta = new Label();
            btnConsultarTurno = new Button();
            txtDniConsulta = new TextBox();
            lblDniConsulta = new Label();
            grpTurno = new GroupBox();
            txtApellidoTurno = new TextBox();
            lblApellidoTurno = new Label();
            txtNombreTurno = new TextBox();
            lblNombreTurno = new Label();
            txtDniTurno = new TextBox();
            lblDniTurno = new Label();
            btnSacarTurno = new Button();
            cmbHorarios = new ComboBox();
            lblHorario = new Label();
            cmbEstudios = new ComboBox();
            lblEstudio = new Label();
            grpResultado = new GroupBox();
            btnRetirarResultado = new Button();
            txtIdResultado = new TextBox();
            lblIdResultado = new Label();
            pnlHeader.SuspendLayout();
            grpConsulta.SuspendLayout();
            grpTurno.SuspendLayout();
            grpResultado.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(44, 62, 80);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4, 5, 4, 5);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1029, 117);
            pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(189, 195, 199);
            lblSubtitle.Location = new Point(358, 73);
            lblSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(300, 25);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "Fernando Bortoluzzi - Thiago Godoy";
            lblSubtitle.Click += lblSubtitle_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(300, 15);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(415, 40);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Centro Médico - Gestión Local";
            lblTitle.Click += lblTitle_Click;
            // 
            // grpConsulta
            // 
            grpConsulta.Controls.Add(lblResultadoConsulta);
            grpConsulta.Controls.Add(btnConsultarTurno);
            grpConsulta.Controls.Add(txtDniConsulta);
            grpConsulta.Controls.Add(lblDniConsulta);
            grpConsulta.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            grpConsulta.ForeColor = Color.FromArgb(44, 62, 80);
            grpConsulta.Location = new Point(29, 142);
            grpConsulta.Margin = new Padding(4, 5, 4, 5);
            grpConsulta.Name = "grpConsulta";
            grpConsulta.Padding = new Padding(4, 5, 4, 5);
            grpConsulta.Size = new Size(971, 167);
            grpConsulta.TabIndex = 1;
            grpConsulta.TabStop = false;
            grpConsulta.Text = "1. Consulta de Turno por DNI";
            grpConsulta.Enter += grpConsulta_Enter;
            // 
            // lblResultadoConsulta
            // 
            lblResultadoConsulta.AutoSize = true;
            lblResultadoConsulta.Font = new Font("Segoe UI", 9.25F, FontStyle.Italic);
            lblResultadoConsulta.ForeColor = Color.FromArgb(127, 140, 141);
            lblResultadoConsulta.Location = new Point(319, 35);
            lblResultadoConsulta.Margin = new Padding(4, 0, 4, 0);
            lblResultadoConsulta.Name = "lblResultadoConsulta";
            lblResultadoConsulta.Size = new Size(323, 25);
            lblResultadoConsulta.TabIndex = 0;
            lblResultadoConsulta.Text = "Ingrese un DNI para buscar su turno...";
            // 
            // btnConsultarTurno
            // 
            btnConsultarTurno.BackColor = Color.FromArgb(52, 152, 219);
            btnConsultarTurno.FlatAppearance.BorderSize = 0;
            btnConsultarTurno.FlatStyle = FlatStyle.Flat;
            btnConsultarTurno.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnConsultarTurno.ForeColor = Color.White;
            btnConsultarTurno.Location = new Point(791, 115);
            btnConsultarTurno.Margin = new Padding(4, 5, 4, 5);
            btnConsultarTurno.Name = "btnConsultarTurno";
            btnConsultarTurno.Size = new Size(171, 42);
            btnConsultarTurno.TabIndex = 1;
            btnConsultarTurno.Text = "Buscar Turno";
            btnConsultarTurno.UseVisualStyleBackColor = false;
            btnConsultarTurno.Click += btnConsultarTurno_Click;
            // 
            // txtDniConsulta
            // 
            txtDniConsulta.Font = new Font("Segoe UI", 9F);
            txtDniConsulta.Location = new Point(340, 100);
            txtDniConsulta.Margin = new Padding(4, 5, 4, 5);
            txtDniConsulta.Name = "txtDniConsulta";
            txtDniConsulta.PlaceholderText = "Ej: 12345678";
            txtDniConsulta.Size = new Size(227, 31);
            txtDniConsulta.TabIndex = 0;
            // 
            // lblDniConsulta
            // 
            lblDniConsulta.AutoSize = true;
            lblDniConsulta.Font = new Font("Segoe UI", 9F);
            lblDniConsulta.ForeColor = Color.FromArgb(51, 51, 51);
            lblDniConsulta.Location = new Point(340, 70);
            lblDniConsulta.Margin = new Padding(4, 0, 4, 0);
            lblDniConsulta.Name = "lblDniConsulta";
            lblDniConsulta.Size = new Size(145, 25);
            lblDniConsulta.TabIndex = 2;
            lblDniConsulta.Text = "DNI del Paciente:";
            // 
            // grpTurno
            // 
            grpTurno.Controls.Add(txtApellidoTurno);
            grpTurno.Controls.Add(lblApellidoTurno);
            grpTurno.Controls.Add(txtNombreTurno);
            grpTurno.Controls.Add(lblNombreTurno);
            grpTurno.Controls.Add(txtDniTurno);
            grpTurno.Controls.Add(lblDniTurno);
            grpTurno.Controls.Add(btnSacarTurno);
            grpTurno.Controls.Add(cmbHorarios);
            grpTurno.Controls.Add(lblHorario);
            grpTurno.Controls.Add(cmbEstudios);
            grpTurno.Controls.Add(lblEstudio);
            grpTurno.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            grpTurno.ForeColor = Color.FromArgb(44, 62, 80);
            grpTurno.Location = new Point(29, 325);
            grpTurno.Margin = new Padding(4, 5, 4, 5);
            grpTurno.Name = "grpTurno";
            grpTurno.Padding = new Padding(4, 5, 4, 5);
            grpTurno.Size = new Size(971, 267);
            grpTurno.TabIndex = 2;
            grpTurno.TabStop = false;
            grpTurno.Text = "2. Solicitar y Asignar Nuevo Turno";
            // 
            // txtApellidoTurno
            // 
            txtApellidoTurno.Font = new Font("Segoe UI", 9F);
            txtApellidoTurno.Location = new Point(616, 72);
            txtApellidoTurno.Margin = new Padding(4, 5, 4, 5);
            txtApellidoTurno.Name = "txtApellidoTurno";
            txtApellidoTurno.PlaceholderText = "Ej: Pérez";
            txtApellidoTurno.Size = new Size(213, 31);
            txtApellidoTurno.TabIndex = 2;
            // 
            // lblApellidoTurno
            // 
            lblApellidoTurno.AutoSize = true;
            lblApellidoTurno.Font = new Font("Segoe UI", 9F);
            lblApellidoTurno.ForeColor = Color.FromArgb(51, 51, 51);
            lblApellidoTurno.Location = new Point(616, 42);
            lblApellidoTurno.Margin = new Padding(4, 0, 4, 0);
            lblApellidoTurno.Name = "lblApellidoTurno";
            lblApellidoTurno.Size = new Size(82, 25);
            lblApellidoTurno.TabIndex = 3;
            lblApellidoTurno.Text = "Apellido:";
            // 
            // txtNombreTurno
            // 
            txtNombreTurno.Font = new Font("Segoe UI", 9F);
            txtNombreTurno.Location = new Point(387, 72);
            txtNombreTurno.Margin = new Padding(4, 5, 4, 5);
            txtNombreTurno.Name = "txtNombreTurno";
            txtNombreTurno.PlaceholderText = "Ej: Juan";
            txtNombreTurno.Size = new Size(213, 31);
            txtNombreTurno.TabIndex = 1;
            // 
            // lblNombreTurno
            // 
            lblNombreTurno.AutoSize = true;
            lblNombreTurno.Font = new Font("Segoe UI", 9F);
            lblNombreTurno.ForeColor = Color.FromArgb(51, 51, 51);
            lblNombreTurno.Location = new Point(387, 42);
            lblNombreTurno.Margin = new Padding(4, 0, 4, 0);
            lblNombreTurno.Name = "lblNombreTurno";
            lblNombreTurno.Size = new Size(82, 25);
            lblNombreTurno.TabIndex = 4;
            lblNombreTurno.Text = "Nombre:";
            // 
            // txtDniTurno
            // 
            txtDniTurno.Font = new Font("Segoe UI", 9F);
            txtDniTurno.Location = new Point(159, 72);
            txtDniTurno.Margin = new Padding(4, 5, 4, 5);
            txtDniTurno.Name = "txtDniTurno";
            txtDniTurno.PlaceholderText = "Ej: 12345678";
            txtDniTurno.Size = new Size(198, 31);
            txtDniTurno.TabIndex = 0;
            // 
            // lblDniTurno
            // 
            lblDniTurno.AutoSize = true;
            lblDniTurno.Font = new Font("Segoe UI", 9F);
            lblDniTurno.ForeColor = Color.FromArgb(51, 51, 51);
            lblDniTurno.Location = new Point(159, 42);
            lblDniTurno.Margin = new Padding(4, 0, 4, 0);
            lblDniTurno.Name = "lblDniTurno";
            lblDniTurno.Size = new Size(145, 25);
            lblDniTurno.TabIndex = 5;
            lblDniTurno.Text = "DNI del Paciente:";
            // 
            // btnSacarTurno
            // 
            btnSacarTurno.BackColor = Color.FromArgb(39, 174, 96);
            btnSacarTurno.FlatAppearance.BorderSize = 0;
            btnSacarTurno.FlatStyle = FlatStyle.Flat;
            btnSacarTurno.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnSacarTurno.ForeColor = Color.White;
            btnSacarTurno.Location = new Point(763, 213);
            btnSacarTurno.Margin = new Padding(4, 5, 4, 5);
            btnSacarTurno.Name = "btnSacarTurno";
            btnSacarTurno.Size = new Size(200, 43);
            btnSacarTurno.TabIndex = 5;
            btnSacarTurno.Text = "Confirmar Turno";
            btnSacarTurno.UseVisualStyleBackColor = false;
            btnSacarTurno.Click += btnSacarTurno_Click;
            // 
            // cmbHorarios
            // 
            cmbHorarios.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHorarios.Font = new Font("Segoe UI", 9F);
            cmbHorarios.FormattingEnabled = true;
            cmbHorarios.Location = new Point(501, 177);
            cmbHorarios.Margin = new Padding(4, 5, 4, 5);
            cmbHorarios.Name = "cmbHorarios";
            cmbHorarios.Size = new Size(198, 33);
            cmbHorarios.TabIndex = 4;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Font = new Font("Segoe UI", 9F);
            lblHorario.ForeColor = Color.FromArgb(51, 51, 51);
            lblHorario.Location = new Point(501, 147);
            lblHorario.Margin = new Padding(4, 0, 4, 0);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(166, 25);
            lblHorario.TabIndex = 6;
            lblHorario.Text = "Horario Disponible:";
            // 
            // cmbEstudios
            // 
            cmbEstudios.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstudios.Font = new Font("Segoe UI", 9F);
            cmbEstudios.FormattingEnabled = true;
            cmbEstudios.Location = new Point(159, 177);
            cmbEstudios.Margin = new Padding(4, 5, 4, 5);
            cmbEstudios.Name = "cmbEstudios";
            cmbEstudios.Size = new Size(313, 33);
            cmbEstudios.TabIndex = 3;
            // 
            // lblEstudio
            // 
            lblEstudio.AutoSize = true;
            lblEstudio.Font = new Font("Segoe UI", 9F);
            lblEstudio.ForeColor = Color.FromArgb(51, 51, 51);
            lblEstudio.Location = new Point(159, 147);
            lblEstudio.Margin = new Padding(4, 0, 4, 0);
            lblEstudio.Name = "lblEstudio";
            lblEstudio.Size = new Size(139, 25);
            lblEstudio.TabIndex = 7;
            lblEstudio.Text = "Estudio Médico:";
            // 
            // grpResultado
            // 
            grpResultado.Controls.Add(btnRetirarResultado);
            grpResultado.Controls.Add(txtIdResultado);
            grpResultado.Controls.Add(lblIdResultado);
            grpResultado.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            grpResultado.ForeColor = Color.FromArgb(44, 62, 80);
            grpResultado.Location = new Point(29, 608);
            grpResultado.Margin = new Padding(4, 5, 4, 5);
            grpResultado.Name = "grpResultado";
            grpResultado.Padding = new Padding(4, 5, 4, 5);
            grpResultado.Size = new Size(971, 150);
            grpResultado.TabIndex = 3;
            grpResultado.TabStop = false;
            grpResultado.Text = "3. Entrega y Registro de Retiro de Resultados";
            // 
            // btnRetirarResultado
            // 
            btnRetirarResultado.BackColor = Color.FromArgb(142, 68, 173);
            btnRetirarResultado.FlatAppearance.BorderSize = 0;
            btnRetirarResultado.FlatStyle = FlatStyle.Flat;
            btnRetirarResultado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnRetirarResultado.ForeColor = Color.White;
            btnRetirarResultado.Location = new Point(271, 70);
            btnRetirarResultado.Margin = new Padding(4, 5, 4, 5);
            btnRetirarResultado.Name = "btnRetirarResultado";
            btnRetirarResultado.Size = new Size(214, 42);
            btnRetirarResultado.TabIndex = 1;
            btnRetirarResultado.Text = "Registrar Retiro";
            btnRetirarResultado.UseVisualStyleBackColor = false;
            btnRetirarResultado.Click += btnRetirarResultado_Click;
            // 
            // txtIdResultado
            // 
            txtIdResultado.Font = new Font("Segoe UI", 9F);
            txtIdResultado.Location = new Point(29, 72);
            txtIdResultado.Margin = new Padding(4, 5, 4, 5);
            txtIdResultado.Name = "txtIdResultado";
            txtIdResultado.PlaceholderText = "Ej: 100, 101 o 102";
            txtIdResultado.Size = new Size(227, 31);
            txtIdResultado.TabIndex = 0;
            // 
            // lblIdResultado
            // 
            lblIdResultado.AutoSize = true;
            lblIdResultado.Font = new Font("Segoe UI", 9F);
            lblIdResultado.ForeColor = Color.FromArgb(51, 51, 51);
            lblIdResultado.Location = new Point(29, 42);
            lblIdResultado.Margin = new Padding(4, 0, 4, 0);
            lblIdResultado.Name = "lblIdResultado";
            lblIdResultado.Size = new Size(142, 25);
            lblIdResultado.TabIndex = 2;
            lblIdResultado.Text = "ID de Resultado:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1029, 792);
            Controls.Add(grpResultado);
            Controls.Add(grpTurno);
            Controls.Add(grpConsulta);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Pacientes - Sistema Médico";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpConsulta.ResumeLayout(false);
            grpConsulta.PerformLayout();
            grpTurno.ResumeLayout(false);
            grpTurno.PerformLayout();
            grpResultado.ResumeLayout(false);
            grpResultado.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private GroupBox grpConsulta;
        private Label lblDniConsulta;
        private TextBox txtDniConsulta;
        private Button btnConsultarTurno;
        private Label lblResultadoConsulta;
        private GroupBox grpTurno;
        private Label lblDniTurno;
        private TextBox txtDniTurno;
        private Label lblNombreTurno;
        private TextBox txtNombreTurno;
        private Label lblApellidoTurno;
        private TextBox txtApellidoTurno;
        private Label lblEstudio;
        private ComboBox cmbEstudios;
        private Label lblHorario;
        private ComboBox cmbHorarios;
        private Button btnSacarTurno;
        private GroupBox grpResultado;
        private Label lblIdResultado;
        private TextBox txtIdResultado;
        private Button btnRetirarResultado;
    }
}