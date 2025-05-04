namespace ControlEscolar.View
{
    partial class frmEstudiantes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitulo = new Label();
            scEstudiantes = new SplitContainer();
            groupBox1 = new GroupBox();
            pbxDudaNumControl = new PictureBox();
            label1 = new Label();
            txtCurp = new TextBox();
            btnGuardar = new Button();
            cbxEstatus = new ComboBox();
            lblCurp = new Label();
            lblEstatus = new Label();
            dtpFechaBaja = new DateTimePicker();
            lblFechaBaja = new Label();
            dtpFechaAlta = new DateTimePicker();
            lblFechaAlta = new Label();
            upSemestre = new DomainUpDown();
            lblSemestre = new Label();
            txtNoControl = new TextBox();
            lblMatricula = new Label();
            dtpFechaNac = new DateTimePicker();
            lblFechaNac = new Label();
            txtTelefono = new TextBox();
            lblTelefono = new Label();
            txtCorreo = new TextBox();
            lblCorreo = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            gbxHerramientas = new GroupBox();
            btnSplit = new Button();
            btnCargamasiva = new Button();
            lbArchivo = new Label();
            gbxFiltros = new GroupBox();
            chkSoloActivos = new CheckBox();
            lblTotalRegistros = new Label();
            lblBuscar = new Label();
            lblTipoFecha = new Label();
            btnActualizar = new Button();
            cbxTipoFecha = new ComboBox();
            txtBusqueda = new TextBox();
            lblFechaIni = new Label();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            lblFechaFin = new Label();
            dgvEstudiantes = new DataGridView();
            cmsEstudiantes = new ContextMenuStrip(components);
            editarEstudianteToolStripMenuItem = new ToolStripMenuItem();
            exportarAExcelToolStripMenuItem = new ToolStripMenuItem();
            ofdArchivo = new OpenFileDialog();
            ttInfo = new ToolTip(components);
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)scEstudiantes).BeginInit();
            scEstudiantes.Panel1.SuspendLayout();
            scEstudiantes.Panel2.SuspendLayout();
            scEstudiantes.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxDudaNumControl).BeginInit();
            gbxHerramientas.SuspendLayout();
            gbxFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).BeginInit();
            cmsEstudiantes.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitulo.BackColor = Color.SlateGray;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, -4);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1014, 35);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Control de estudiantes";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scEstudiantes
            // 
            scEstudiantes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            scEstudiantes.FixedPanel = FixedPanel.Panel1;
            scEstudiantes.Location = new Point(0, 34);
            scEstudiantes.Name = "scEstudiantes";
            // 
            // scEstudiantes.Panel1
            // 
            scEstudiantes.Panel1.Controls.Add(groupBox1);
            // 
            // scEstudiantes.Panel2
            // 
            scEstudiantes.Panel2.Controls.Add(gbxHerramientas);
            scEstudiantes.Panel2.Controls.Add(gbxFiltros);
            scEstudiantes.Panel2.Controls.Add(dgvEstudiantes);
            scEstudiantes.Size = new Size(1010, 691);
            scEstudiantes.SplitterDistance = 336;
            scEstudiantes.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(pbxDudaNumControl);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtCurp);
            groupBox1.Controls.Add(btnGuardar);
            groupBox1.Controls.Add(cbxEstatus);
            groupBox1.Controls.Add(lblCurp);
            groupBox1.Controls.Add(lblEstatus);
            groupBox1.Controls.Add(dtpFechaBaja);
            groupBox1.Controls.Add(lblFechaBaja);
            groupBox1.Controls.Add(dtpFechaAlta);
            groupBox1.Controls.Add(lblFechaAlta);
            groupBox1.Controls.Add(upSemestre);
            groupBox1.Controls.Add(lblSemestre);
            groupBox1.Controls.Add(txtNoControl);
            groupBox1.Controls.Add(lblMatricula);
            groupBox1.Controls.Add(dtpFechaNac);
            groupBox1.Controls.Add(lblFechaNac);
            groupBox1.Controls.Add(txtTelefono);
            groupBox1.Controls.Add(lblTelefono);
            groupBox1.Controls.Add(txtCorreo);
            groupBox1.Controls.Add(lblCorreo);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(lblNombre);
            groupBox1.Location = new Point(12, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(309, 600);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Alta o edición";
            // 
            // pbxDudaNumControl
            // 
            pbxDudaNumControl.Image = Properties.Resources.help24;
            pbxDudaNumControl.Location = new Point(255, 333);
            pbxDudaNumControl.Name = "pbxDudaNumControl";
            pbxDudaNumControl.Size = new Size(29, 34);
            pbxDudaNumControl.TabIndex = 21;
            pbxDudaNumControl.TabStop = false;
            ttInfo.SetToolTip(pbxDudaNumControl, "T/M-Año de ingreso-Número de alumno");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 572);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 20;
            label1.Text = "* Datos obligatorios";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // txtCurp
            // 
            txtCurp.Location = new Point(22, 211);
            txtCurp.Margin = new Padding(2);
            txtCurp.MaxLength = 18;
            txtCurp.Name = "txtCurp";
            txtCurp.Size = new Size(256, 23);
            txtCurp.TabIndex = 4;
            // 
            // btnGuardar
            // 
            btnGuardar.Image = Properties.Resources.save;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(200, 566);
            btnGuardar.Margin = new Padding(2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(80, 26);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextAlign = ContentAlignment.MiddleRight;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // cbxEstatus
            // 
            cbxEstatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEstatus.FormattingEnabled = true;
            cbxEstatus.Location = new Point(22, 457);
            cbxEstatus.Margin = new Padding(2);
            cbxEstatus.Name = "cbxEstatus";
            cbxEstatus.Size = new Size(258, 23);
            cbxEstatus.TabIndex = 9;
            cbxEstatus.SelectedIndexChanged += cbxEstatus_SelectedIndexChanged;
            // 
            // lblCurp
            // 
            lblCurp.AutoSize = true;
            lblCurp.Location = new Point(22, 185);
            lblCurp.Margin = new Padding(2, 0, 2, 0);
            lblCurp.Name = "lblCurp";
            lblCurp.Size = new Size(45, 15);
            lblCurp.TabIndex = 18;
            lblCurp.Text = "CURP *";
            lblCurp.TextAlign = ContentAlignment.TopRight;
            // 
            // lblEstatus
            // 
            lblEstatus.AutoSize = true;
            lblEstatus.Location = new Point(18, 434);
            lblEstatus.Margin = new Padding(2, 0, 2, 0);
            lblEstatus.Name = "lblEstatus";
            lblEstatus.Size = new Size(52, 15);
            lblEstatus.TabIndex = 15;
            lblEstatus.Text = "Estatus *";
            lblEstatus.TextAlign = ContentAlignment.TopRight;
            // 
            // dtpFechaBaja
            // 
            dtpFechaBaja.Format = DateTimePickerFormat.Short;
            dtpFechaBaja.Location = new Point(23, 515);
            dtpFechaBaja.Margin = new Padding(2);
            dtpFechaBaja.Name = "dtpFechaBaja";
            dtpFechaBaja.Size = new Size(257, 23);
            dtpFechaBaja.TabIndex = 10;
            // 
            // lblFechaBaja
            // 
            lblFechaBaja.AutoSize = true;
            lblFechaBaja.Location = new Point(19, 498);
            lblFechaBaja.Margin = new Padding(2, 0, 2, 0);
            lblFechaBaja.Name = "lblFechaBaja";
            lblFechaBaja.Size = new Size(63, 15);
            lblFechaBaja.TabIndex = 14;
            lblFechaBaja.Text = "Fecha baja";
            lblFechaBaja.TextAlign = ContentAlignment.TopRight;
            // 
            // dtpFechaAlta
            // 
            dtpFechaAlta.Format = DateTimePickerFormat.Short;
            dtpFechaAlta.Location = new Point(18, 388);
            dtpFechaAlta.Margin = new Padding(2);
            dtpFechaAlta.Name = "dtpFechaAlta";
            dtpFechaAlta.Size = new Size(262, 23);
            dtpFechaAlta.TabIndex = 8;
            // 
            // lblFechaAlta
            // 
            lblFechaAlta.AutoSize = true;
            lblFechaAlta.Location = new Point(14, 371);
            lblFechaAlta.Margin = new Padding(2, 0, 2, 0);
            lblFechaAlta.Name = "lblFechaAlta";
            lblFechaAlta.Size = new Size(68, 15);
            lblFechaAlta.TabIndex = 12;
            lblFechaAlta.Text = "Fecha alta *";
            lblFechaAlta.TextAlign = ContentAlignment.TopRight;
            // 
            // upSemestre
            // 
            upSemestre.BorderStyle = BorderStyle.FixedSingle;
            upSemestre.Items.Add("13");
            upSemestre.Items.Add("12");
            upSemestre.Items.Add("11");
            upSemestre.Items.Add("10");
            upSemestre.Items.Add("9");
            upSemestre.Items.Add("8");
            upSemestre.Items.Add("7");
            upSemestre.Items.Add("6");
            upSemestre.Items.Add("5");
            upSemestre.Items.Add("4");
            upSemestre.Items.Add("3");
            upSemestre.Items.Add("2");
            upSemestre.Items.Add("1");
            upSemestre.Location = new Point(146, 273);
            upSemestre.Margin = new Padding(2);
            upSemestre.Name = "upSemestre";
            upSemestre.ReadOnly = true;
            upSemestre.Size = new Size(132, 23);
            upSemestre.TabIndex = 6;
            upSemestre.Text = "1";
            upSemestre.SelectedItemChanged += upSemestre_SelectedItemChanged;
            // 
            // lblSemestre
            // 
            lblSemestre.AutoSize = true;
            lblSemestre.Location = new Point(146, 256);
            lblSemestre.Margin = new Padding(2, 0, 2, 0);
            lblSemestre.Name = "lblSemestre";
            lblSemestre.Size = new Size(63, 15);
            lblSemestre.TabIndex = 9;
            lblSemestre.Text = "Semestre *";
            lblSemestre.TextAlign = ContentAlignment.TopRight;
            // 
            // txtNoControl
            // 
            txtNoControl.Location = new Point(14, 333);
            txtNoControl.Margin = new Padding(2);
            txtNoControl.MaxLength = 20;
            txtNoControl.Name = "txtNoControl";
            txtNoControl.Size = new Size(236, 23);
            txtNoControl.TabIndex = 7;
            // 
            // lblMatricula
            // 
            lblMatricula.AutoSize = true;
            lblMatricula.Location = new Point(18, 307);
            lblMatricula.Margin = new Padding(2, 0, 2, 0);
            lblMatricula.Name = "lblMatricula";
            lblMatricula.Size = new Size(91, 15);
            lblMatricula.TabIndex = 7;
            lblMatricula.Text = "No. de control *";
            lblMatricula.TextAlign = ContentAlignment.TopRight;
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Format = DateTimePickerFormat.Short;
            dtpFechaNac.Location = new Point(18, 273);
            dtpFechaNac.Margin = new Padding(2);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(104, 23);
            dtpFechaNac.TabIndex = 5;
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(14, 256);
            lblFechaNac.Margin = new Padding(2, 0, 2, 0);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(109, 15);
            lblFechaNac.TabIndex = 6;
            lblFechaNac.Text = "Fecha nacimiento *";
            lblFechaNac.TextAlign = ContentAlignment.TopRight;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(18, 145);
            txtTelefono.Margin = new Padding(2);
            txtTelefono.MaxLength = 15;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(260, 23);
            txtTelefono.TabIndex = 3;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(18, 128);
            lblTelefono.Margin = new Padding(2, 0, 2, 0);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(61, 15);
            lblTelefono.TabIndex = 4;
            lblTelefono.Text = "Teléfono *";
            lblTelefono.TextAlign = ContentAlignment.TopRight;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(18, 95);
            txtCorreo.Margin = new Padding(2);
            txtCorreo.MaxLength = 100;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(260, 23);
            txtCorreo.TabIndex = 2;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(18, 79);
            lblCorreo.Margin = new Padding(2, 0, 2, 0);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(51, 15);
            lblCorreo.TabIndex = 2;
            lblCorreo.Text = "Correo *";
            lblCorreo.TextAlign = ContentAlignment.TopRight;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(18, 49);
            txtNombre.Margin = new Padding(2);
            txtNombre.MaxLength = 255;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(260, 23);
            txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(14, 32);
            lblNombre.Margin = new Padding(2, 0, 2, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(113, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre completo *";
            // 
            // gbxHerramientas
            // 
            gbxHerramientas.Controls.Add(btnExportar);
            gbxHerramientas.Controls.Add(btnSplit);
            gbxHerramientas.Controls.Add(btnCargamasiva);
            gbxHerramientas.Controls.Add(lbArchivo);
            gbxHerramientas.Location = new Point(4, 3);
            gbxHerramientas.Margin = new Padding(2);
            gbxHerramientas.Name = "gbxHerramientas";
            gbxHerramientas.Padding = new Padding(2);
            gbxHerramientas.Size = new Size(650, 47);
            gbxHerramientas.TabIndex = 19;
            gbxHerramientas.TabStop = false;
            gbxHerramientas.Text = "Herramientas";
            gbxHerramientas.Enter += gbxHerramientas_Enter;
            // 
            // btnSplit
            // 
            btnSplit.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSplit.Location = new Point(4, 18);
            btnSplit.Margin = new Padding(2);
            btnSplit.Name = "btnSplit";
            btnSplit.Size = new Size(132, 22);
            btnSplit.TabIndex = 0;
            btnSplit.Text = "Mostrar captura rápida";
            btnSplit.UseVisualStyleBackColor = true;
            btnSplit.Click += btnSplit_Click;
            // 
            // btnCargamasiva
            // 
            btnCargamasiva.Font = new Font("Segoe UI", 8F);
            btnCargamasiva.Location = new Point(140, 18);
            btnCargamasiva.Margin = new Padding(2);
            btnCargamasiva.Name = "btnCargamasiva";
            btnCargamasiva.Size = new Size(108, 22);
            btnCargamasiva.TabIndex = 2;
            btnCargamasiva.Text = "Carga masiva";
            btnCargamasiva.UseVisualStyleBackColor = true;
            btnCargamasiva.Click += btnCargamasiva_Click;
            // 
            // lbArchivo
            // 
            lbArchivo.AutoSize = true;
            lbArchivo.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbArchivo.Location = new Point(253, 23);
            lbArchivo.Margin = new Padding(2, 0, 2, 0);
            lbArchivo.Name = "lbArchivo";
            lbArchivo.Size = new Size(122, 12);
            lbArchivo.TabIndex = 17;
            lbArchivo.Text = "Ruta de archivo a importar";
            lbArchivo.Click += lbArchivo_Click;
            // 
            // gbxFiltros
            // 
            gbxFiltros.Controls.Add(chkSoloActivos);
            gbxFiltros.Controls.Add(lblTotalRegistros);
            gbxFiltros.Controls.Add(lblBuscar);
            gbxFiltros.Controls.Add(lblTipoFecha);
            gbxFiltros.Controls.Add(btnActualizar);
            gbxFiltros.Controls.Add(cbxTipoFecha);
            gbxFiltros.Controls.Add(txtBusqueda);
            gbxFiltros.Controls.Add(lblFechaIni);
            gbxFiltros.Controls.Add(dtpFechaInicio);
            gbxFiltros.Controls.Add(dtpFechaFin);
            gbxFiltros.Controls.Add(lblFechaFin);
            gbxFiltros.Location = new Point(4, 50);
            gbxFiltros.Margin = new Padding(2);
            gbxFiltros.Name = "gbxFiltros";
            gbxFiltros.Padding = new Padding(2);
            gbxFiltros.Size = new Size(650, 71);
            gbxFiltros.TabIndex = 18;
            gbxFiltros.TabStop = false;
            gbxFiltros.Text = "Filtros";
            // 
            // chkSoloActivos
            // 
            chkSoloActivos.AutoSize = true;
            chkSoloActivos.Location = new Point(540, 17);
            chkSoloActivos.Margin = new Padding(2);
            chkSoloActivos.Name = "chkSoloActivos";
            chkSoloActivos.Size = new Size(89, 19);
            chkSoloActivos.TabIndex = 21;
            chkSoloActivos.Text = "Solo activos";
            chkSoloActivos.UseVisualStyleBackColor = true;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Location = new Point(4, 50);
            lblTotalRegistros.Margin = new Padding(2, 0, 2, 0);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(103, 15);
            lblTotalRegistros.TabIndex = 19;
            lblTotalRegistros.Text = "TOTAL REGISTROS";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(153, 51);
            lblBuscar.Margin = new Padding(2, 0, 2, 0);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(42, 15);
            lblBuscar.TabIndex = 17;
            lblBuscar.Text = "Buscar";
            lblBuscar.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTipoFecha
            // 
            lblTipoFecha.AutoSize = true;
            lblTipoFecha.Location = new Point(4, 20);
            lblTipoFecha.Margin = new Padding(2, 0, 2, 0);
            lblTipoFecha.Name = "lblTipoFecha";
            lblTipoFecha.Size = new Size(63, 15);
            lblTipoFecha.TabIndex = 6;
            lblTipoFecha.Text = "Tipo fecha";
            // 
            // btnActualizar
            // 
            btnActualizar.Image = Properties.Resources.refresh;
            btnActualizar.ImageAlign = ContentAlignment.MiddleLeft;
            btnActualizar.Location = new Point(540, 43);
            btnActualizar.Margin = new Padding(2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(95, 27);
            btnActualizar.TabIndex = 15;
            btnActualizar.Text = "Actualizar";
            btnActualizar.TextAlign = ContentAlignment.MiddleRight;
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // cbxTipoFecha
            // 
            cbxTipoFecha.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTipoFecha.FormattingEnabled = true;
            cbxTipoFecha.Location = new Point(74, 17);
            cbxTipoFecha.Margin = new Padding(2);
            cbxTipoFecha.Name = "cbxTipoFecha";
            cbxTipoFecha.Size = new Size(175, 23);
            cbxTipoFecha.TabIndex = 5;
            cbxTipoFecha.SelectedIndexChanged += cbxTipoFecha_SelectedIndexChanged;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(253, 49);
            txtBusqueda.Margin = new Padding(2);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(253, 23);
            txtBusqueda.TabIndex = 16;
            // 
            // lblFechaIni
            // 
            lblFechaIni.AutoSize = true;
            lblFechaIni.Location = new Point(253, 19);
            lblFechaIni.Margin = new Padding(2, 0, 2, 0);
            lblFechaIni.Name = "lblFechaIni";
            lblFechaIni.Size = new Size(36, 15);
            lblFechaIni.TabIndex = 13;
            lblFechaIni.Text = "Inicio";
            lblFechaIni.TextAlign = ContentAlignment.TopRight;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(295, 17);
            dtpFechaInicio.Margin = new Padding(2);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(87, 23);
            dtpFechaInicio.TabIndex = 4;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(418, 17);
            dtpFechaFin.Margin = new Padding(2);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(88, 23);
            dtpFechaFin.TabIndex = 4;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(391, 20);
            lblFechaFin.Margin = new Padding(2, 0, 2, 0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(23, 15);
            lblFechaFin.TabIndex = 14;
            lblFechaFin.Text = "Fin";
            lblFechaFin.TextAlign = ContentAlignment.TopRight;
            // 
            // dgvEstudiantes
            // 
            dgvEstudiantes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEstudiantes.BackgroundColor = Color.White;
            dgvEstudiantes.BorderStyle = BorderStyle.Fixed3D;
            dgvEstudiantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstudiantes.ContextMenuStrip = cmsEstudiantes;
            dgvEstudiantes.Location = new Point(4, 125);
            dgvEstudiantes.Margin = new Padding(2);
            dgvEstudiantes.Name = "dgvEstudiantes";
            dgvEstudiantes.RowHeadersWidth = 62;
            dgvEstudiantes.Size = new Size(651, 479);
            dgvEstudiantes.TabIndex = 1;
            dgvEstudiantes.CellDoubleClick += dgvEstudiantes_CellDoubleClick;
            // 
            // cmsEstudiantes
            // 
            cmsEstudiantes.ImageScalingSize = new Size(24, 24);
            cmsEstudiantes.Items.AddRange(new ToolStripItem[] { editarEstudianteToolStripMenuItem, exportarAExcelToolStripMenuItem });
            cmsEstudiantes.Name = "contextMenuStrip1";
            cmsEstudiantes.Size = new Size(163, 48);
            cmsEstudiantes.Opening += cmsEstudiantes_Opening;
            // 
            // editarEstudianteToolStripMenuItem
            // 
            editarEstudianteToolStripMenuItem.Image = Properties.Resources.edit;
            editarEstudianteToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            editarEstudianteToolStripMenuItem.Name = "editarEstudianteToolStripMenuItem";
            editarEstudianteToolStripMenuItem.Size = new Size(162, 22);
            editarEstudianteToolStripMenuItem.Text = "Editar estudiante";
            editarEstudianteToolStripMenuItem.Click += editarEstudianteToolStripMenuItem_Click;
            // 
            // exportarAExcelToolStripMenuItem
            // 
            exportarAExcelToolStripMenuItem.Image = Properties.Resources.excel;
            exportarAExcelToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            exportarAExcelToolStripMenuItem.Name = "exportarAExcelToolStripMenuItem";
            exportarAExcelToolStripMenuItem.Size = new Size(162, 22);
            exportarAExcelToolStripMenuItem.Text = "Exportar a Excel";
            exportarAExcelToolStripMenuItem.Click += exportarAExcelToolStripMenuItem_Click;
            // 
            // ofdArchivo
            // 
            ofdArchivo.FileName = "Carga masiva de estudiantes";
            // 
            // btnExportar
            // 
            btnExportar.Image = Properties.Resources.excel;
            btnExportar.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportar.Location = new Point(515, 13);
            btnExportar.Margin = new Padding(2);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(120, 27);
            btnExportar.TabIndex = 22;
            btnExportar.Text = "Exportar a Excel ";
            btnExportar.TextAlign = ContentAlignment.MiddleRight;
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // frmEstudiantes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1012, 637);
            Controls.Add(scEstudiantes);
            Controls.Add(lblTitulo);
            Margin = new Padding(2);
            Name = "frmEstudiantes";
            Text = "Estudiantes";
            Load += Estudiantes_Load;
            scEstudiantes.Panel1.ResumeLayout(false);
            scEstudiantes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scEstudiantes).EndInit();
            scEstudiantes.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxDudaNumControl).EndInit();
            gbxHerramientas.ResumeLayout(false);
            gbxHerramientas.PerformLayout();
            gbxFiltros.ResumeLayout(false);
            gbxFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).EndInit();
            cmsEstudiantes.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private SplitContainer scEstudiantes;
        private GroupBox groupBox1;
        private Button btnCargamasiva;
        private DataGridView dgvEstudiantes;
        private Button btnSplit;
        private DateTimePicker dtpFechaFin;
        private DateTimePicker dtpFechaInicio;
        private TextBox txtCorreo;
        private Label lblCorreo;
        private TextBox txtNombre;
        private Label lblNombre;
        private Label lblFechaNac;
        private TextBox txtTelefono;
        private Label lblTelefono;
        private Label lblMatricula;
        private DateTimePicker dtpFechaNac;
        private ComboBox cbxEstatus;
        private Label lblEstatus;
        private DateTimePicker dtpFechaBaja;
        private Label lblFechaBaja;
        private DateTimePicker dtpFechaAlta;
        private Label lblFechaAlta;
        private DomainUpDown upSemestre;
        private Label lblSemestre;
        private TextBox txtNoControl;
        private Button btnGuardar;
        private ComboBox cbxTipoFecha;
        private Label lblFechaFin;
        private Label lblFechaIni;
        private Label lblTipoFecha;
        private TextBox txtBusqueda;
        private Button btnActualizar;
        private GroupBox gbxFiltros;
        private Label lbArchivo;
        private GroupBox gbxHerramientas;
        private Label lblBuscar;
        private TextBox txtCurp;
        private Label lblCurp;
        private OpenFileDialog ofdArchivo;
        private Label label1;
        private PictureBox pbxDudaNumControl;
        private ToolTip ttInfo;
        private Label lblTotalRegistros;
        private Label lblETotRegistros;
        private CheckBox chkSoloActivos;
        private ContextMenuStrip cmsEstudiantes;
        private ToolStripMenuItem editarEstudianteToolStripMenuItem;
        private ToolStripMenuItem exportarAExcelToolStripMenuItem;
        private Button btnExportar;
    }
}