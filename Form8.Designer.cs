namespace Casino
{
    partial class Form8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form8));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.edit_elemenet = new System.Windows.Forms.GroupBox();
            this.volver_win = new System.Windows.Forms.Button();
            this.edit_item_win = new System.Windows.Forms.TextBox();
            this.itemtoedit = new System.Windows.Forms.Label();
            this.save_edit = new System.Windows.Forms.Button();
            this.panel_edit_item = new System.Windows.Forms.Panel();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.edit_elemenet.SuspendLayout();
            this.panel_edit_item.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(12, 11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(545, 196);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(247, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "     Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // edit_elemenet
            // 
            this.edit_elemenet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edit_elemenet.AutoSize = true;
            this.edit_elemenet.Controls.Add(this.volver_win);
            this.edit_elemenet.Controls.Add(this.edit_item_win);
            this.edit_elemenet.Controls.Add(this.itemtoedit);
            this.edit_elemenet.Controls.Add(this.save_edit);
            this.edit_elemenet.Location = new System.Drawing.Point(20, 19);
            this.edit_elemenet.Margin = new System.Windows.Forms.Padding(6);
            this.edit_elemenet.Name = "edit_elemenet";
            this.edit_elemenet.Padding = new System.Windows.Forms.Padding(6);
            this.edit_elemenet.Size = new System.Drawing.Size(237, 127);
            this.edit_elemenet.TabIndex = 2;
            this.edit_elemenet.TabStop = false;
            this.edit_elemenet.Text = "Editar Elemento";
            this.edit_elemenet.Visible = false;
            // 
            // volver_win
            // 
            this.volver_win.Location = new System.Drawing.Point(28, 82);
            this.volver_win.Name = "volver_win";
            this.volver_win.Size = new System.Drawing.Size(75, 23);
            this.volver_win.TabIndex = 3;
            this.volver_win.Text = "Cancelar";
            this.volver_win.UseVisualStyleBackColor = true;
            this.volver_win.Click += new System.EventHandler(this.volver_win_Click);
            // 
            // edit_item_win
            // 
            this.edit_item_win.Location = new System.Drawing.Point(116, 29);
            this.edit_item_win.Name = "edit_item_win";
            this.edit_item_win.Size = new System.Drawing.Size(109, 20);
            this.edit_item_win.TabIndex = 2;
            // 
            // itemtoedit
            // 
            this.itemtoedit.AutoSize = true;
            this.itemtoedit.Location = new System.Drawing.Point(9, 32);
            this.itemtoedit.Name = "itemtoedit";
            this.itemtoedit.Size = new System.Drawing.Size(35, 13);
            this.itemtoedit.TabIndex = 1;
            this.itemtoedit.Text = "label1";
            // 
            // save_edit
            // 
            this.save_edit.Location = new System.Drawing.Point(116, 82);
            this.save_edit.Name = "save_edit";
            this.save_edit.Size = new System.Drawing.Size(75, 23);
            this.save_edit.TabIndex = 0;
            this.save_edit.Text = "Guardar Cambios";
            this.save_edit.UseVisualStyleBackColor = true;
            this.save_edit.Click += new System.EventHandler(this.save_edit_Click);
            // 
            // panel_edit_item
            // 
            this.panel_edit_item.Controls.Add(this.edit_elemenet);
            this.panel_edit_item.Location = new System.Drawing.Point(146, 37);
            this.panel_edit_item.Name = "panel_edit_item";
            this.panel_edit_item.Size = new System.Drawing.Size(276, 155);
            this.panel_edit_item.TabIndex = 3;
            this.panel_edit_item.Visible = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Servicio";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Costo Servicio";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Fecha Inicio Validez";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Fecha Fin Validez";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 240);
            this.Controls.Add(this.panel_edit_item);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial Servicios Configurados";
            this.Load += new System.EventHandler(this.Form8_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.edit_elemenet.ResumeLayout(false);
            this.edit_elemenet.PerformLayout();
            this.panel_edit_item.ResumeLayout(false);
            this.panel_edit_item.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox edit_elemenet;
        private System.Windows.Forms.Button volver_win;
        private System.Windows.Forms.TextBox edit_item_win;
        private System.Windows.Forms.Label itemtoedit;
        private System.Windows.Forms.Button save_edit;
        private System.Windows.Forms.Panel panel_edit_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}