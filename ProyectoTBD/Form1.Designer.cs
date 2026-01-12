namespace ProyectoTBD
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbBasesDeDatos = new System.Windows.Forms.ComboBox();
            this.cbTablas = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CrearNuevaTabla = new System.Windows.Forms.Button();
            this.AgregarNuevoCampo = new System.Windows.Forms.Button();
            this.EliminarCampo = new System.Windows.Forms.Button();
            this.VaciarDGV = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Campo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TDato = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Const = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbBasesDeDatos
            // 
            this.cbBasesDeDatos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbBasesDeDatos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbBasesDeDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.cbBasesDeDatos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBasesDeDatos.ForeColor = System.Drawing.Color.White;
            this.cbBasesDeDatos.FormattingEnabled = true;
            this.cbBasesDeDatos.Location = new System.Drawing.Point(12, 12);
            this.cbBasesDeDatos.Name = "cbBasesDeDatos";
            this.cbBasesDeDatos.Size = new System.Drawing.Size(545, 21);
            this.cbBasesDeDatos.TabIndex = 1;
            this.cbBasesDeDatos.Text = "Base de datos";
            this.cbBasesDeDatos.SelectedIndexChanged += new System.EventHandler(this.cbBasesDeDatos_SelectedIndexChanged);
            // 
            // cbTablas
            // 
            this.cbTablas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.cbTablas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbTablas.ForeColor = System.Drawing.Color.White;
            this.cbTablas.FormattingEnabled = true;
            this.cbTablas.Location = new System.Drawing.Point(12, 39);
            this.cbTablas.Name = "cbTablas";
            this.cbTablas.Size = new System.Drawing.Size(545, 21);
            this.cbTablas.TabIndex = 2;
            this.cbTablas.Text = "Tabla";
            this.cbTablas.SelectedIndexChanged += new System.EventHandler(this.cbTablas_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Campo,
            this.TDato,
            this.LMax,
            this.NoNull,
            this.Const});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridView1.Location = new System.Drawing.Point(12, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(545, 372);
            this.dataGridView1.TabIndex = 3;
            // 
            // CrearNuevaTabla
            // 
            this.CrearNuevaTabla.Location = new System.Drawing.Point(563, 120);
            this.CrearNuevaTabla.Name = "CrearNuevaTabla";
            this.CrearNuevaTabla.Size = new System.Drawing.Size(94, 48);
            this.CrearNuevaTabla.TabIndex = 5;
            this.CrearNuevaTabla.Text = "Crear Tabla";
            this.CrearNuevaTabla.UseVisualStyleBackColor = true;
            this.CrearNuevaTabla.Click += new System.EventHandler(this.CrearNuevaTabla_Click);
            // 
            // AgregarNuevoCampo
            // 
            this.AgregarNuevoCampo.Location = new System.Drawing.Point(563, 174);
            this.AgregarNuevoCampo.Name = "AgregarNuevoCampo";
            this.AgregarNuevoCampo.Size = new System.Drawing.Size(94, 48);
            this.AgregarNuevoCampo.TabIndex = 6;
            this.AgregarNuevoCampo.Text = "Agregar Campo";
            this.AgregarNuevoCampo.UseVisualStyleBackColor = true;
            this.AgregarNuevoCampo.Click += new System.EventHandler(this.AgregarNuevoCampo_Click);
            // 
            // EliminarCampo
            // 
            this.EliminarCampo.Location = new System.Drawing.Point(563, 228);
            this.EliminarCampo.Name = "EliminarCampo";
            this.EliminarCampo.Size = new System.Drawing.Size(94, 48);
            this.EliminarCampo.TabIndex = 7;
            this.EliminarCampo.Text = "Eliminar Campo";
            this.EliminarCampo.UseVisualStyleBackColor = true;
            this.EliminarCampo.Click += new System.EventHandler(this.EliminarCampo_Click);
            // 
            // VaciarDGV
            // 
            this.VaciarDGV.Location = new System.Drawing.Point(563, 66);
            this.VaciarDGV.Name = "VaciarDGV";
            this.VaciarDGV.Size = new System.Drawing.Size(94, 48);
            this.VaciarDGV.TabIndex = 4;
            this.VaciarDGV.Text = "Vaciar DataGrid";
            this.VaciarDGV.UseVisualStyleBackColor = true;
            this.VaciarDGV.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(567, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 48);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cerrar Sesion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Campo
            // 
            this.Campo.HeaderText = "Campo";
            this.Campo.Name = "Campo";
            this.Campo.Width = 63;
            // 
            // TDato
            // 
            this.TDato.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TDato.HeaderText = "TDato";
            this.TDato.Items.AddRange(new object[] {
            "bigint",
            "binary",
            "bit",
            "char",
            "date",
            "datetime",
            "datetime2",
            "datetimeoffset",
            "decimal",
            "float",
            "geography",
            "geometry",
            "hierarchyid",
            "image",
            "int",
            "money",
            "nchar",
            "ntext",
            "numeric",
            "nvarchar",
            "nvarchar",
            "real",
            "smalldatetime",
            "smallint",
            "smallmoney",
            "sql_variant",
            "text",
            "time",
            "timestamp",
            "tinyint",
            "uniquidentifier",
            "varbinary",
            "varbinary",
            "varchar",
            "varchar",
            "xml"});
            this.TDato.Name = "TDato";
            this.TDato.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TDato.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TDato.Width = 60;
            // 
            // LMax
            // 
            this.LMax.HeaderText = "Longitud Maxima";
            this.LMax.Name = "LMax";
            this.LMax.Width = 101;
            // 
            // NoNull
            // 
            this.NoNull.HeaderText = "No Nulos";
            this.NoNull.Name = "NoNull";
            this.NoNull.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoNull.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.NoNull.Width = 68;
            // 
            // Const
            // 
            this.Const.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Const.HeaderText = "Constraint";
            this.Const.Items.AddRange(new object[] {
            "PRIMARY KEY",
            "FOREIGN KEY",
            "CHECK",
            "UNIQUE",
            "NINGUNO"});
            this.Const.Name = "Const";
            this.Const.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Const.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Const.Width = 77;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(673, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.EliminarCampo);
            this.Controls.Add(this.AgregarNuevoCampo);
            this.Controls.Add(this.CrearNuevaTabla);
            this.Controls.Add(this.VaciarDGV);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbTablas);
            this.Controls.Add(this.cbBasesDeDatos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.99D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASISTENTE DE BASE DE DATOS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbBasesDeDatos;
        private System.Windows.Forms.ComboBox cbTablas;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button CrearNuevaTabla;
        private System.Windows.Forms.Button AgregarNuevoCampo;
        private System.Windows.Forms.Button EliminarCampo;
        private System.Windows.Forms.Button VaciarDGV;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Campo;
        private System.Windows.Forms.DataGridViewComboBoxColumn TDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn LMax;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NoNull;
        private System.Windows.Forms.DataGridViewComboBoxColumn Const;
    }
}

