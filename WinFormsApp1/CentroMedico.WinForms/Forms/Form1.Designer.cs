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

        // Paleta:
        //  Encabezado   (23, 43, 77)     Fondo        (243, 246, 250)
        //  Texto        (30, 41, 59)     Texto suave  (100, 116, 139)
        //  Consulta     (37, 99, 235)    Turno        (22, 163, 74)    Retiro (124, 58, 237)

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnVerEstadisticas = new Button();
            lblSubtitle = new Label();
            lblTitle = new Label();
            tlpMain = new TableLayoutPanel();
            grpConsulta = new Panel();
            tlpConsulta = new TableLayoutPanel();
            lblTituloConsulta = new Label();
            lblDniConsulta = new Label();
            txtDniConsulta = new TextBox();
            btnConsultarTurno = new Button();
            lblResultadoConsulta = new Label();
            pnlAcentoConsulta = new Panel();
            grpTurno = new Panel();
            tlpTurno = new TableLayoutPanel();
            lblTituloTurno = new Label();
            lblDniTurno = new Label();
            lblNombreTurno = new Label();
            lblApellidoTurno = new Label();
            txtDniTurno = new TextBox();
            txtNombreTurno = new TextBox();
            txtApellidoTurno = new TextBox();
            lblEstudio = new Label();
            lblFechaTurno = new Label();
            lblHorario = new Label();
            cmbEstudios = new ComboBox();
            pnlFecha = new Panel();
            txtFechaTurno = new TextBox();
            btnSeleccionarFecha = new Button();
            cmbHorarios = new ComboBox();
            btnSacarTurno = new Button();
            pnlAcentoTurno = new Panel();
            grpResultado = new Panel();
            tlpResultado = new TableLayoutPanel();
            lblTituloResultado = new Label();
            lblDniResultado = new Label();
            txtDniResultado = new TextBox();
            btnRetirarResultado = new Button();
            pnlAcentoResultado = new Panel();
            pnlHeader.SuspendLayout();
            tlpMain.SuspendLayout();
            grpConsulta.SuspendLayout();
            tlpConsulta.SuspendLayout();
            grpTurno.SuspendLayout();
            tlpTurno.SuspendLayout();
            pnlFecha.SuspendLayout();
            grpResultado.SuspendLayout();
            tlpResultado.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(23, 43, 77);
            pnlHeader.Controls.Add(btnVerEstadisticas);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(780, 76);
            pnlHeader.TabIndex = 1;
            // 
            // btnVerEstadisticas
            // 
            btnVerEstadisticas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerEstadisticas.BackColor = Color.FromArgb(45, 70, 110);
            btnVerEstadisticas.Cursor = Cursors.Hand;
            btnVerEstadisticas.FlatAppearance.BorderColor = Color.FromArgb(96, 120, 160);
            btnVerEstadisticas.FlatAppearance.MouseDownBackColor = Color.FromArgb(35, 58, 95);
            btnVerEstadisticas.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 90, 135);
            btnVerEstadisticas.FlatStyle = FlatStyle.Flat;
            btnVerEstadisticas.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnVerEstadisticas.ForeColor = Color.White;
            btnVerEstadisticas.Location = new Point(626, 20);
            btnVerEstadisticas.Name = "btnVerEstadisticas";
            btnVerEstadisticas.Size = new Size(130, 36);
            btnVerEstadisticas.TabIndex = 3;
            btnVerEstadisticas.Text = "📊  Estadísticas";
            btnVerEstadisticas.UseVisualStyleBackColor = false;
            btnVerEstadisticas.Click += btnVerEstadisticas_Click;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Fill;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(160, 174, 192);
            lblSubtitle.Location = new Point(0, 46);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(780, 30);
            lblSubtitle.TabIndex = 4;
            lblSubtitle.Text = "Fernando Bortoluzzi - Thiago Godoy";
            lblSubtitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(780, 46);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Centro Médico · Gestión Local";
            lblTitle.TextAlign = ContentAlignment.BottomCenter;
            // 
            // tlpMain
            // 
            tlpMain.AutoScroll = true;
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(grpConsulta, 0, 0);
            tlpMain.Controls.Add(grpTurno, 0, 1);
            tlpMain.Controls.Add(grpResultado, 0, 2);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 76);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(24, 20, 24, 6);
            tlpMain.RowCount = 3;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 254F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 134F));
            tlpMain.Size = new Size(780, 624);
            tlpMain.TabIndex = 0;
            // 
            // grpConsulta
            // 
            grpConsulta.BackColor = Color.White;
            grpConsulta.Controls.Add(tlpConsulta);
            grpConsulta.Controls.Add(pnlAcentoConsulta);
            grpConsulta.Dock = DockStyle.Fill;
            grpConsulta.Location = new Point(24, 20);
            grpConsulta.Margin = new Padding(0, 0, 0, 14);
            grpConsulta.Name = "grpConsulta";
            grpConsulta.Size = new Size(732, 176);
            grpConsulta.TabIndex = 0;
            // 
            // tlpConsulta
            // 
            tlpConsulta.ColumnCount = 2;
            tlpConsulta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpConsulta.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpConsulta.Controls.Add(lblTituloConsulta, 0, 0);
            tlpConsulta.Controls.Add(lblDniConsulta, 0, 1);
            tlpConsulta.Controls.Add(txtDniConsulta, 0, 2);
            tlpConsulta.Controls.Add(btnConsultarTurno, 1, 2);
            tlpConsulta.Controls.Add(lblResultadoConsulta, 0, 3);
            tlpConsulta.Dock = DockStyle.Fill;
            tlpConsulta.Location = new Point(5, 0);
            tlpConsulta.Name = "tlpConsulta";
            tlpConsulta.Padding = new Padding(22, 14, 18, 12);
            tlpConsulta.RowCount = 4;
            tlpConsulta.RowStyles.Add(new RowStyle());
            tlpConsulta.RowStyles.Add(new RowStyle());
            tlpConsulta.RowStyles.Add(new RowStyle());
            tlpConsulta.RowStyles.Add(new RowStyle());
            tlpConsulta.Size = new Size(727, 176);
            tlpConsulta.TabIndex = 1;
            // 
            // lblTituloConsulta
            // 
            lblTituloConsulta.Anchor = AnchorStyles.Left;
            lblTituloConsulta.AutoSize = true;
            tlpConsulta.SetColumnSpan(lblTituloConsulta, 2);
            lblTituloConsulta.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTituloConsulta.ForeColor = Color.FromArgb(37, 99, 235);
            lblTituloConsulta.Location = new Point(22, 14);
            lblTituloConsulta.Margin = new Padding(0, 0, 0, 8);
            lblTituloConsulta.Name = "lblTituloConsulta";
            lblTituloConsulta.Size = new Size(156, 20);
            lblTituloConsulta.TabIndex = 0;
            lblTituloConsulta.Text = "1 · Consultar un turno";
            // 
            // lblDniConsulta
            // 
            lblDniConsulta.AutoSize = true;
            tlpConsulta.SetColumnSpan(lblDniConsulta, 2);
            lblDniConsulta.Font = new Font("Segoe UI", 9F);
            lblDniConsulta.ForeColor = Color.FromArgb(100, 116, 139);
            lblDniConsulta.Location = new Point(22, 42);
            lblDniConsulta.Margin = new Padding(0, 0, 0, 3);
            lblDniConsulta.Name = "lblDniConsulta";
            lblDniConsulta.Size = new Size(94, 15);
            lblDniConsulta.TabIndex = 1;
            lblDniConsulta.Text = "DNI del paciente";
            // 
            // txtDniConsulta
            // 
            txtDniConsulta.Dock = DockStyle.Fill;
            txtDniConsulta.Font = new Font("Segoe UI", 10.5F);
            txtDniConsulta.Location = new Point(22, 60);
            txtDniConsulta.Margin = new Padding(0, 0, 10, 0);
            txtDniConsulta.MaxLength = 10;
            txtDniConsulta.Name = "txtDniConsulta";
            txtDniConsulta.PlaceholderText = "Ej: 12345678";
            txtDniConsulta.Size = new Size(527, 26);
            txtDniConsulta.TabIndex = 0;
            // 
            // btnConsultarTurno
            // 
            btnConsultarTurno.BackColor = Color.FromArgb(37, 99, 235);
            btnConsultarTurno.Cursor = Cursors.Hand;
            btnConsultarTurno.Dock = DockStyle.Fill;
            btnConsultarTurno.FlatAppearance.BorderSize = 0;
            btnConsultarTurno.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
            btnConsultarTurno.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnConsultarTurno.FlatStyle = FlatStyle.Flat;
            btnConsultarTurno.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnConsultarTurno.ForeColor = Color.White;
            btnConsultarTurno.Location = new Point(559, 60);
            btnConsultarTurno.Margin = new Padding(0);
            btnConsultarTurno.Name = "btnConsultarTurno";
            btnConsultarTurno.Size = new Size(150, 26);
            btnConsultarTurno.TabIndex = 1;
            btnConsultarTurno.Text = "Buscar turno";
            btnConsultarTurno.UseVisualStyleBackColor = false;
            btnConsultarTurno.Click += btnConsultarTurno_Click;
            // 
            // lblResultadoConsulta
            // 
            lblResultadoConsulta.BackColor = Color.FromArgb(239, 246, 255);
            tlpConsulta.SetColumnSpan(lblResultadoConsulta, 2);
            lblResultadoConsulta.Dock = DockStyle.Fill;
            lblResultadoConsulta.Font = new Font("Segoe UI", 9.5F);
            lblResultadoConsulta.ForeColor = Color.FromArgb(71, 85, 105);
            lblResultadoConsulta.Location = new Point(22, 96);
            lblResultadoConsulta.Margin = new Padding(0, 10, 0, 0);
            lblResultadoConsulta.MinimumSize = new Size(0, 48);
            lblResultadoConsulta.Name = "lblResultadoConsulta";
            lblResultadoConsulta.Padding = new Padding(12, 0, 12, 0);
            lblResultadoConsulta.Size = new Size(687, 68);
            lblResultadoConsulta.TabIndex = 3;
            lblResultadoConsulta.Text = "Ingresá un DNI para buscar su turno…";
            lblResultadoConsulta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlAcentoConsulta
            // 
            pnlAcentoConsulta.BackColor = Color.FromArgb(37, 99, 235);
            pnlAcentoConsulta.Dock = DockStyle.Left;
            pnlAcentoConsulta.Location = new Point(0, 0);
            pnlAcentoConsulta.Name = "pnlAcentoConsulta";
            pnlAcentoConsulta.Size = new Size(5, 176);
            pnlAcentoConsulta.TabIndex = 2;
            // 
            // grpTurno
            // 
            grpTurno.BackColor = Color.White;
            grpTurno.Controls.Add(tlpTurno);
            grpTurno.Controls.Add(pnlAcentoTurno);
            grpTurno.Dock = DockStyle.Fill;
            grpTurno.Location = new Point(24, 210);
            grpTurno.Margin = new Padding(0, 0, 0, 14);
            grpTurno.Name = "grpTurno";
            grpTurno.Size = new Size(732, 240);
            grpTurno.TabIndex = 1;
            // 
            // tlpTurno
            // 
            tlpTurno.ColumnCount = 3;
            tlpTurno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpTurno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpTurno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tlpTurno.Controls.Add(lblTituloTurno, 0, 0);
            tlpTurno.Controls.Add(lblDniTurno, 0, 1);
            tlpTurno.Controls.Add(lblNombreTurno, 1, 1);
            tlpTurno.Controls.Add(lblApellidoTurno, 2, 1);
            tlpTurno.Controls.Add(txtDniTurno, 0, 2);
            tlpTurno.Controls.Add(txtNombreTurno, 1, 2);
            tlpTurno.Controls.Add(txtApellidoTurno, 2, 2);
            tlpTurno.Controls.Add(lblEstudio, 0, 3);
            tlpTurno.Controls.Add(lblFechaTurno, 1, 3);
            tlpTurno.Controls.Add(lblHorario, 2, 3);
            tlpTurno.Controls.Add(cmbEstudios, 0, 4);
            tlpTurno.Controls.Add(pnlFecha, 1, 4);
            tlpTurno.Controls.Add(cmbHorarios, 2, 4);
            tlpTurno.Controls.Add(btnSacarTurno, 0, 5);
            tlpTurno.Dock = DockStyle.Fill;
            tlpTurno.Location = new Point(5, 0);
            tlpTurno.Name = "tlpTurno";
            tlpTurno.Padding = new Padding(22, 14, 18, 12);
            tlpTurno.RowCount = 6;
            tlpTurno.RowStyles.Add(new RowStyle());
            tlpTurno.RowStyles.Add(new RowStyle());
            tlpTurno.RowStyles.Add(new RowStyle());
            tlpTurno.RowStyles.Add(new RowStyle());
            tlpTurno.RowStyles.Add(new RowStyle());
            tlpTurno.RowStyles.Add(new RowStyle());
            tlpTurno.Size = new Size(727, 240);
            tlpTurno.TabIndex = 1;
            // 
            // lblTituloTurno
            // 
            lblTituloTurno.Anchor = AnchorStyles.Left;
            lblTituloTurno.AutoSize = true;
            tlpTurno.SetColumnSpan(lblTituloTurno, 3);
            lblTituloTurno.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTituloTurno.ForeColor = Color.FromArgb(22, 163, 74);
            lblTituloTurno.Location = new Point(22, 14);
            lblTituloTurno.Margin = new Padding(0, 0, 0, 8);
            lblTituloTurno.Name = "lblTituloTurno";
            lblTituloTurno.Size = new Size(192, 20);
            lblTituloTurno.TabIndex = 0;
            lblTituloTurno.Text = "2 · Asignar un turno nuevo";
            // 
            // lblDniTurno
            // 
            lblDniTurno.AutoSize = true;
            lblDniTurno.Font = new Font("Segoe UI", 9F);
            lblDniTurno.ForeColor = Color.FromArgb(100, 116, 139);
            lblDniTurno.Location = new Point(22, 42);
            lblDniTurno.Margin = new Padding(0, 0, 0, 3);
            lblDniTurno.Name = "lblDniTurno";
            lblDniTurno.Size = new Size(94, 15);
            lblDniTurno.TabIndex = 1;
            lblDniTurno.Text = "DNI del paciente";
            // 
            // lblNombreTurno
            // 
            lblNombreTurno.AutoSize = true;
            lblNombreTurno.Font = new Font("Segoe UI", 9F);
            lblNombreTurno.ForeColor = Color.FromArgb(100, 116, 139);
            lblNombreTurno.Location = new Point(250, 42);
            lblNombreTurno.Margin = new Padding(0, 0, 0, 3);
            lblNombreTurno.Name = "lblNombreTurno";
            lblNombreTurno.Size = new Size(51, 15);
            lblNombreTurno.TabIndex = 2;
            lblNombreTurno.Text = "Nombre";
            // 
            // lblApellidoTurno
            // 
            lblApellidoTurno.AutoSize = true;
            lblApellidoTurno.Font = new Font("Segoe UI", 9F);
            lblApellidoTurno.ForeColor = Color.FromArgb(100, 116, 139);
            lblApellidoTurno.Location = new Point(478, 42);
            lblApellidoTurno.Margin = new Padding(0, 0, 0, 3);
            lblApellidoTurno.Name = "lblApellidoTurno";
            lblApellidoTurno.Size = new Size(51, 15);
            lblApellidoTurno.TabIndex = 3;
            lblApellidoTurno.Text = "Apellido";
            // 
            // txtDniTurno
            // 
            txtDniTurno.Dock = DockStyle.Fill;
            txtDniTurno.Font = new Font("Segoe UI", 10.5F);
            txtDniTurno.Location = new Point(22, 60);
            txtDniTurno.Margin = new Padding(0, 0, 12, 10);
            txtDniTurno.MaxLength = 10;
            txtDniTurno.Name = "txtDniTurno";
            txtDniTurno.PlaceholderText = "Ej: 12345678";
            txtDniTurno.Size = new Size(216, 26);
            txtDniTurno.TabIndex = 0;
            // 
            // txtNombreTurno
            // 
            txtNombreTurno.Dock = DockStyle.Fill;
            txtNombreTurno.Font = new Font("Segoe UI", 10.5F);
            txtNombreTurno.Location = new Point(250, 60);
            txtNombreTurno.Margin = new Padding(0, 0, 12, 10);
            txtNombreTurno.MaxLength = 50;
            txtNombreTurno.Name = "txtNombreTurno";
            txtNombreTurno.PlaceholderText = "Ej: Juan";
            txtNombreTurno.Size = new Size(216, 26);
            txtNombreTurno.TabIndex = 1;
            // 
            // txtApellidoTurno
            // 
            txtApellidoTurno.Dock = DockStyle.Fill;
            txtApellidoTurno.Font = new Font("Segoe UI", 10.5F);
            txtApellidoTurno.Location = new Point(478, 60);
            txtApellidoTurno.Margin = new Padding(0, 0, 0, 10);
            txtApellidoTurno.MaxLength = 50;
            txtApellidoTurno.Name = "txtApellidoTurno";
            txtApellidoTurno.PlaceholderText = "Ej: Pérez";
            txtApellidoTurno.Size = new Size(231, 26);
            txtApellidoTurno.TabIndex = 2;
            // 
            // lblEstudio
            // 
            lblEstudio.AutoSize = true;
            lblEstudio.Font = new Font("Segoe UI", 9F);
            lblEstudio.ForeColor = Color.FromArgb(100, 116, 139);
            lblEstudio.Location = new Point(22, 96);
            lblEstudio.Margin = new Padding(0, 0, 0, 3);
            lblEstudio.Name = "lblEstudio";
            lblEstudio.Size = new Size(89, 15);
            lblEstudio.TabIndex = 4;
            lblEstudio.Text = "Estudio médico";
            // 
            // lblFechaTurno
            // 
            lblFechaTurno.AutoSize = true;
            lblFechaTurno.Font = new Font("Segoe UI", 9F);
            lblFechaTurno.ForeColor = Color.FromArgb(100, 116, 139);
            lblFechaTurno.Location = new Point(250, 96);
            lblFechaTurno.Margin = new Padding(0, 0, 0, 3);
            lblFechaTurno.Name = "lblFechaTurno";
            lblFechaTurno.Size = new Size(89, 15);
            lblFechaTurno.TabIndex = 5;
            lblFechaTurno.Text = "Fecha del turno";
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Font = new Font("Segoe UI", 9F);
            lblHorario.ForeColor = Color.FromArgb(100, 116, 139);
            lblHorario.Location = new Point(478, 96);
            lblHorario.Margin = new Padding(0, 0, 0, 3);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(105, 15);
            lblHorario.TabIndex = 6;
            lblHorario.Text = "Horario disponible";
            // 
            // cmbEstudios
            // 
            cmbEstudios.Dock = DockStyle.Fill;
            cmbEstudios.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstudios.FlatStyle = FlatStyle.Flat;
            cmbEstudios.Font = new Font("Segoe UI", 10.5F);
            cmbEstudios.FormattingEnabled = true;
            cmbEstudios.Location = new Point(22, 114);
            cmbEstudios.Margin = new Padding(0, 0, 12, 10);
            cmbEstudios.Name = "cmbEstudios";
            cmbEstudios.Size = new Size(216, 27);
            cmbEstudios.TabIndex = 3;
            // 
            // pnlFecha
            // 
            pnlFecha.Controls.Add(txtFechaTurno);
            pnlFecha.Controls.Add(btnSeleccionarFecha);
            pnlFecha.Dock = DockStyle.Fill;
            pnlFecha.Location = new Point(250, 114);
            pnlFecha.Margin = new Padding(0, 0, 12, 10);
            pnlFecha.Name = "pnlFecha";
            pnlFecha.Size = new Size(216, 27);
            pnlFecha.TabIndex = 4;
            // 
            // txtFechaTurno
            // 
            txtFechaTurno.BackColor = Color.White;
            txtFechaTurno.Dock = DockStyle.Fill;
            txtFechaTurno.Font = new Font("Segoe UI", 10.5F);
            txtFechaTurno.Location = new Point(0, 0);
            txtFechaTurno.Name = "txtFechaTurno";
            txtFechaTurno.PlaceholderText = "dd/mm/aaaa";
            txtFechaTurno.ReadOnly = true;
            txtFechaTurno.Size = new Size(152, 26);
            txtFechaTurno.TabIndex = 0;
            // 
            // btnSeleccionarFecha
            // 
            btnSeleccionarFecha.BackColor = Color.FromArgb(226, 232, 240);
            btnSeleccionarFecha.Cursor = Cursors.Hand;
            btnSeleccionarFecha.Dock = DockStyle.Right;
            btnSeleccionarFecha.FlatAppearance.BorderSize = 0;
            btnSeleccionarFecha.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 190, 205);
            btnSeleccionarFecha.FlatAppearance.MouseOverBackColor = Color.FromArgb(203, 213, 225);
            btnSeleccionarFecha.FlatStyle = FlatStyle.Flat;
            btnSeleccionarFecha.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnSeleccionarFecha.ForeColor = Color.FromArgb(30, 41, 59);
            btnSeleccionarFecha.Location = new Point(152, 0);
            btnSeleccionarFecha.Name = "btnSeleccionarFecha";
            btnSeleccionarFecha.Size = new Size(64, 27);
            btnSeleccionarFecha.TabIndex = 1;
            btnSeleccionarFecha.Text = "📅 Elegir";
            btnSeleccionarFecha.UseVisualStyleBackColor = false;
            btnSeleccionarFecha.Click += btnSeleccionarFecha_Click;
            // 
            // cmbHorarios
            // 
            cmbHorarios.Dock = DockStyle.Fill;
            cmbHorarios.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHorarios.FlatStyle = FlatStyle.Flat;
            cmbHorarios.Font = new Font("Segoe UI", 10.5F);
            cmbHorarios.FormattingEnabled = true;
            cmbHorarios.Location = new Point(478, 114);
            cmbHorarios.Margin = new Padding(0, 0, 0, 10);
            cmbHorarios.Name = "cmbHorarios";
            cmbHorarios.Size = new Size(231, 27);
            cmbHorarios.TabIndex = 5;
            // 
            // btnSacarTurno
            // 
            btnSacarTurno.Anchor = AnchorStyles.Right;
            btnSacarTurno.BackColor = Color.FromArgb(22, 163, 74);
            tlpTurno.SetColumnSpan(btnSacarTurno, 3);
            btnSacarTurno.Cursor = Cursors.Hand;
            btnSacarTurno.FlatAppearance.BorderSize = 0;
            btnSacarTurno.FlatAppearance.MouseDownBackColor = Color.FromArgb(21, 101, 52);
            btnSacarTurno.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btnSacarTurno.FlatStyle = FlatStyle.Flat;
            btnSacarTurno.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSacarTurno.ForeColor = Color.White;
            btnSacarTurno.Location = new Point(519, 173);
            btnSacarTurno.Margin = new Padding(0, 6, 0, 0);
            btnSacarTurno.Name = "btnSacarTurno";
            btnSacarTurno.Size = new Size(190, 38);
            btnSacarTurno.TabIndex = 6;
            btnSacarTurno.Text = "✔  Confirmar turno";
            btnSacarTurno.UseVisualStyleBackColor = false;
            btnSacarTurno.Click += btnSacarTurno_Click;
            // 
            // pnlAcentoTurno
            // 
            pnlAcentoTurno.BackColor = Color.FromArgb(22, 163, 74);
            pnlAcentoTurno.Dock = DockStyle.Left;
            pnlAcentoTurno.Location = new Point(0, 0);
            pnlAcentoTurno.Name = "pnlAcentoTurno";
            pnlAcentoTurno.Size = new Size(5, 240);
            pnlAcentoTurno.TabIndex = 2;
            // 
            // grpResultado
            // 
            grpResultado.BackColor = Color.White;
            grpResultado.Controls.Add(tlpResultado);
            grpResultado.Controls.Add(pnlAcentoResultado);
            grpResultado.Dock = DockStyle.Fill;
            grpResultado.Location = new Point(24, 464);
            grpResultado.Margin = new Padding(0, 0, 0, 14);
            grpResultado.Name = "grpResultado";
            grpResultado.Size = new Size(732, 140);
            grpResultado.TabIndex = 2;
            // 
            // tlpResultado
            // 
            tlpResultado.ColumnCount = 2;
            tlpResultado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpResultado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpResultado.Controls.Add(lblTituloResultado, 0, 0);
            tlpResultado.Controls.Add(lblDniResultado, 0, 1);
            tlpResultado.Controls.Add(txtDniResultado, 0, 2);
            tlpResultado.Controls.Add(btnRetirarResultado, 1, 2);
            tlpResultado.Dock = DockStyle.Fill;
            tlpResultado.Location = new Point(5, 0);
            tlpResultado.Name = "tlpResultado";
            tlpResultado.Padding = new Padding(22, 14, 18, 12);
            tlpResultado.RowCount = 3;
            tlpResultado.RowStyles.Add(new RowStyle());
            tlpResultado.RowStyles.Add(new RowStyle());
            tlpResultado.RowStyles.Add(new RowStyle());
            tlpResultado.Size = new Size(727, 140);
            tlpResultado.TabIndex = 1;
            // 
            // lblTituloResultado
            // 
            lblTituloResultado.Anchor = AnchorStyles.Left;
            lblTituloResultado.AutoSize = true;
            tlpResultado.SetColumnSpan(lblTituloResultado, 2);
            lblTituloResultado.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTituloResultado.ForeColor = Color.FromArgb(124, 58, 237);
            lblTituloResultado.Location = new Point(22, 14);
            lblTituloResultado.Margin = new Padding(0, 0, 0, 8);
            lblTituloResultado.Name = "lblTituloResultado";
            lblTituloResultado.Size = new Size(177, 20);
            lblTituloResultado.TabIndex = 0;
            lblTituloResultado.Text = "3 · Entrega de resultados";
            // 
            // lblDniResultado
            // 
            lblDniResultado.AutoSize = true;
            tlpResultado.SetColumnSpan(lblDniResultado, 2);
            lblDniResultado.Font = new Font("Segoe UI", 9F);
            lblDniResultado.ForeColor = Color.FromArgb(100, 116, 139);
            lblDniResultado.Location = new Point(22, 42);
            lblDniResultado.Margin = new Padding(0, 0, 0, 3);
            lblDniResultado.Name = "lblDniResultado";
            lblDniResultado.Size = new Size(94, 15);
            lblDniResultado.TabIndex = 1;
            lblDniResultado.Text = "DNI del paciente";
            // 
            // txtDniResultado
            // 
            txtDniResultado.Dock = DockStyle.Fill;
            txtDniResultado.Font = new Font("Segoe UI", 10.5F);
            txtDniResultado.Location = new Point(22, 60);
            txtDniResultado.Margin = new Padding(0, 0, 10, 0);
            txtDniResultado.MaxLength = 10;
            txtDniResultado.Name = "txtDniResultado";
            txtDniResultado.PlaceholderText = "Ej: 12345678";
            txtDniResultado.Size = new Size(527, 26);
            txtDniResultado.TabIndex = 0;
            // 
            // btnRetirarResultado
            // 
            btnRetirarResultado.BackColor = Color.FromArgb(124, 58, 237);
            btnRetirarResultado.Cursor = Cursors.Hand;
            btnRetirarResultado.Dock = DockStyle.Fill;
            btnRetirarResultado.FlatAppearance.BorderSize = 0;
            btnRetirarResultado.FlatAppearance.MouseDownBackColor = Color.FromArgb(91, 33, 182);
            btnRetirarResultado.FlatAppearance.MouseOverBackColor = Color.FromArgb(109, 40, 217);
            btnRetirarResultado.FlatStyle = FlatStyle.Flat;
            btnRetirarResultado.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnRetirarResultado.ForeColor = Color.White;
            btnRetirarResultado.Location = new Point(559, 60);
            btnRetirarResultado.Margin = new Padding(0);
            btnRetirarResultado.Name = "btnRetirarResultado";
            btnRetirarResultado.Size = new Size(150, 68);
            btnRetirarResultado.TabIndex = 1;
            btnRetirarResultado.Text = "Registrar retiro";
            btnRetirarResultado.UseVisualStyleBackColor = false;
            btnRetirarResultado.Click += btnRetirarResultado_Click;
            // 
            // pnlAcentoResultado
            // 
            pnlAcentoResultado.BackColor = Color.FromArgb(124, 58, 237);
            pnlAcentoResultado.Dock = DockStyle.Left;
            pnlAcentoResultado.Location = new Point(0, 0);
            pnlAcentoResultado.Name = "pnlAcentoResultado";
            pnlAcentoResultado.Size = new Size(5, 140);
            pnlAcentoResultado.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 250);
            ClientSize = new Size(780, 700);
            Controls.Add(tlpMain);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(660, 560);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Pacientes - Sistema Médico";
            pnlHeader.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            grpConsulta.ResumeLayout(false);
            tlpConsulta.ResumeLayout(false);
            tlpConsulta.PerformLayout();
            grpTurno.ResumeLayout(false);
            tlpTurno.ResumeLayout(false);
            tlpTurno.PerformLayout();
            pnlFecha.ResumeLayout(false);
            pnlFecha.PerformLayout();
            grpResultado.ResumeLayout(false);
            tlpResultado.ResumeLayout(false);
            tlpResultado.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnVerEstadisticas;
        private TableLayoutPanel tlpMain;

        // Tarjeta 1
        private Panel grpConsulta;
        private Panel pnlAcentoConsulta;
        private TableLayoutPanel tlpConsulta;
        private Label lblTituloConsulta;
        private Label lblDniConsulta;
        private TextBox txtDniConsulta;
        private Button btnConsultarTurno;
        private Label lblResultadoConsulta;

        // Tarjeta 2
        private Panel grpTurno;
        private Panel pnlAcentoTurno;
        private TableLayoutPanel tlpTurno;
        private Label lblTituloTurno;
        private Label lblDniTurno;
        private TextBox txtDniTurno;
        private Label lblNombreTurno;
        private TextBox txtNombreTurno;
        private Label lblApellidoTurno;
        private TextBox txtApellidoTurno;
        private Label lblEstudio;
        private ComboBox cmbEstudios;
        private Label lblFechaTurno;
        private Panel pnlFecha;
        private TextBox txtFechaTurno;
        private Button btnSeleccionarFecha;
        private Label lblHorario;
        private ComboBox cmbHorarios;
        private Button btnSacarTurno;

        // Tarjeta 3
        private Panel grpResultado;
        private Panel pnlAcentoResultado;
        private TableLayoutPanel tlpResultado;
        private Label lblTituloResultado;
        private Label lblDniResultado;
        private TextBox txtDniResultado;
        private Button btnRetirarResultado;
    }
}