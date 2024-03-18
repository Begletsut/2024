namespace PZO_V3
{
    partial class DBForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelDemands = new MetroFramework.Controls.MetroPanel();
            this.tbId = new MetroFramework.Controls.MetroTextBox();
            this.tbName = new MetroFramework.Controls.MetroTextBox();
            this.btnExportKontragents = new System.Windows.Forms.Button();
            this.textBoxSearchKontagents = new MetroFramework.Controls.MetroTextBox();
            this.btnInsertKontragents = new System.Windows.Forms.Button();
            this.btnAddKontragents = new System.Windows.Forms.Button();
            this.btnUpdateKontragents = new System.Windows.Forms.Button();
            this.btnDeleteKontragents = new System.Windows.Forms.Button();
            this.gridDB = new MetroFramework.Controls.MetroGrid();
            this.btnRefreshTable = new System.Windows.Forms.Button();
            this.rtbThird = new System.Windows.Forms.RichTextBox();
            this.rtbSecond = new System.Windows.Forms.RichTextBox();
            this.panelDemands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDB)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDemands
            // 
            this.panelDemands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDemands.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelDemands.Controls.Add(this.rtbSecond);
            this.panelDemands.Controls.Add(this.rtbThird);
            this.panelDemands.Controls.Add(this.textBoxSearchKontagents);
            this.panelDemands.Controls.Add(this.tbId);
            this.panelDemands.Controls.Add(this.tbName);
            this.panelDemands.Controls.Add(this.btnExportKontragents);
            this.panelDemands.Controls.Add(this.btnInsertKontragents);
            this.panelDemands.Controls.Add(this.btnAddKontragents);
            this.panelDemands.Controls.Add(this.btnUpdateKontragents);
            this.panelDemands.Controls.Add(this.btnDeleteKontragents);
            this.panelDemands.Controls.Add(this.gridDB);
            this.panelDemands.HorizontalScrollbarBarColor = true;
            this.panelDemands.HorizontalScrollbarHighlightOnWheel = false;
            this.panelDemands.HorizontalScrollbarSize = 10;
            this.panelDemands.Location = new System.Drawing.Point(5, 79);
            this.panelDemands.Name = "panelDemands";
            this.panelDemands.Size = new System.Drawing.Size(590, 279);
            this.panelDemands.TabIndex = 48;
            this.panelDemands.UseCustomBackColor = true;
            this.panelDemands.VerticalScrollbarBarColor = true;
            this.panelDemands.VerticalScrollbarHighlightOnWheel = false;
            this.panelDemands.VerticalScrollbarSize = 10;
            // 
            // tbId
            // 
            // 
            // 
            // 
            this.tbId.CustomButton.Image = null;
            this.tbId.CustomButton.Location = new System.Drawing.Point(20, 1);
            this.tbId.CustomButton.Name = "";
            this.tbId.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbId.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbId.CustomButton.TabIndex = 1;
            this.tbId.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbId.CustomButton.UseSelectable = true;
            this.tbId.CustomButton.Visible = false;
            this.tbId.Enabled = false;
            this.tbId.Lines = new string[0];
            this.tbId.Location = new System.Drawing.Point(3, 26);
            this.tbId.MaxLength = 32767;
            this.tbId.Name = "tbId";
            this.tbId.PasswordChar = '\0';
            this.tbId.PromptText = "Id";
            this.tbId.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbId.SelectedText = "";
            this.tbId.SelectionLength = 0;
            this.tbId.SelectionStart = 0;
            this.tbId.ShortcutsEnabled = true;
            this.tbId.Size = new System.Drawing.Size(34, 23);
            this.tbId.TabIndex = 81;
            this.tbId.UseSelectable = true;
            this.tbId.WaterMark = "Id";
            this.tbId.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbId.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbName
            // 
            // 
            // 
            // 
            this.tbName.CustomButton.Image = null;
            this.tbName.CustomButton.Location = new System.Drawing.Point(53, 1);
            this.tbName.CustomButton.Name = "";
            this.tbName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbName.CustomButton.TabIndex = 1;
            this.tbName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbName.CustomButton.UseSelectable = true;
            this.tbName.CustomButton.Visible = false;
            this.tbName.Lines = new string[0];
            this.tbName.Location = new System.Drawing.Point(43, 26);
            this.tbName.MaxLength = 32767;
            this.tbName.Name = "tbName";
            this.tbName.PasswordChar = '\0';
            this.tbName.PromptText = "Name";
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbName.SelectedText = "";
            this.tbName.SelectionLength = 0;
            this.tbName.SelectionStart = 0;
            this.tbName.ShortcutsEnabled = true;
            this.tbName.Size = new System.Drawing.Size(100, 23);
            this.tbName.TabIndex = 78;
            this.tbName.UseSelectable = true;
            this.tbName.WaterMark = "Name";
            this.tbName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnExportKontragents
            // 
            this.btnExportKontragents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportKontragents.BackgroundImage = global::PZO_V3.Properties.Resources.export;
            this.btnExportKontragents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportKontragents.Location = new System.Drawing.Point(547, 236);
            this.btnExportKontragents.Name = "btnExportKontragents";
            this.btnExportKontragents.Size = new System.Drawing.Size(40, 40);
            this.btnExportKontragents.TabIndex = 77;
            this.btnExportKontragents.UseVisualStyleBackColor = true;
            this.btnExportKontragents.Click += new System.EventHandler(this.btnExportKontragents_Click);
            // 
            // textBoxSearchKontagents
            // 
            this.textBoxSearchKontagents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.textBoxSearchKontagents.CustomButton.Image = null;
            this.textBoxSearchKontagents.CustomButton.Location = new System.Drawing.Point(95, 1);
            this.textBoxSearchKontagents.CustomButton.Name = "";
            this.textBoxSearchKontagents.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxSearchKontagents.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxSearchKontagents.CustomButton.TabIndex = 1;
            this.textBoxSearchKontagents.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxSearchKontagents.CustomButton.UseSelectable = true;
            this.textBoxSearchKontagents.CustomButton.Visible = false;
            this.textBoxSearchKontagents.Lines = new string[0];
            this.textBoxSearchKontagents.Location = new System.Drawing.Point(424, 26);
            this.textBoxSearchKontagents.MaxLength = 32767;
            this.textBoxSearchKontagents.Name = "textBoxSearchKontagents";
            this.textBoxSearchKontagents.PasswordChar = '\0';
            this.textBoxSearchKontagents.PromptText = "Search";
            this.textBoxSearchKontagents.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxSearchKontagents.SelectedText = "";
            this.textBoxSearchKontagents.SelectionLength = 0;
            this.textBoxSearchKontagents.SelectionStart = 0;
            this.textBoxSearchKontagents.ShortcutsEnabled = true;
            this.textBoxSearchKontagents.Size = new System.Drawing.Size(117, 23);
            this.textBoxSearchKontagents.TabIndex = 75;
            this.textBoxSearchKontagents.UseSelectable = true;
            this.textBoxSearchKontagents.WaterMark = "Search";
            this.textBoxSearchKontagents.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxSearchKontagents.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxSearchKontagents.TextChanged += new System.EventHandler(this.textBoxSearchKontagents_TextChanged);
            // 
            // btnInsertKontragents
            // 
            this.btnInsertKontragents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertKontragents.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsertKontragents.BackgroundImage")));
            this.btnInsertKontragents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsertKontragents.Location = new System.Drawing.Point(547, 99);
            this.btnInsertKontragents.Name = "btnInsertKontragents";
            this.btnInsertKontragents.Size = new System.Drawing.Size(40, 40);
            this.btnInsertKontragents.TabIndex = 68;
            this.btnInsertKontragents.UseVisualStyleBackColor = true;
            this.btnInsertKontragents.Click += new System.EventHandler(this.btnInsertKontragents_Click);
            // 
            // btnAddKontragents
            // 
            this.btnAddKontragents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddKontragents.BackgroundImage = global::PZO_V3.Properties.Resources.clear;
            this.btnAddKontragents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddKontragents.Location = new System.Drawing.Point(547, 9);
            this.btnAddKontragents.Name = "btnAddKontragents";
            this.btnAddKontragents.Size = new System.Drawing.Size(40, 40);
            this.btnAddKontragents.TabIndex = 66;
            this.btnAddKontragents.UseVisualStyleBackColor = true;
            this.btnAddKontragents.Click += new System.EventHandler(this.btnAddKontragents_Click);
            // 
            // btnUpdateKontragents
            // 
            this.btnUpdateKontragents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateKontragents.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdateKontragents.BackgroundImage")));
            this.btnUpdateKontragents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdateKontragents.Location = new System.Drawing.Point(547, 144);
            this.btnUpdateKontragents.Name = "btnUpdateKontragents";
            this.btnUpdateKontragents.Size = new System.Drawing.Size(40, 40);
            this.btnUpdateKontragents.TabIndex = 67;
            this.btnUpdateKontragents.UseVisualStyleBackColor = true;
            this.btnUpdateKontragents.Click += new System.EventHandler(this.btnUpdateKontragents_Click);
            // 
            // btnDeleteKontragents
            // 
            this.btnDeleteKontragents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteKontragents.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteKontragents.BackgroundImage")));
            this.btnDeleteKontragents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteKontragents.Location = new System.Drawing.Point(547, 190);
            this.btnDeleteKontragents.Name = "btnDeleteKontragents";
            this.btnDeleteKontragents.Size = new System.Drawing.Size(40, 40);
            this.btnDeleteKontragents.TabIndex = 65;
            this.btnDeleteKontragents.UseVisualStyleBackColor = true;
            this.btnDeleteKontragents.Click += new System.EventHandler(this.btnDeleteKontragents_Click);
            // 
            // gridDB
            // 
            this.gridDB.AllowUserToAddRows = false;
            this.gridDB.AllowUserToResizeRows = false;
            this.gridDB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridDB.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridDB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridDB.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridDB.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.gridDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDB.DefaultCellStyle = dataGridViewCellStyle14;
            this.gridDB.EnableHeadersVisualStyles = false;
            this.gridDB.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridDB.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridDB.Location = new System.Drawing.Point(0, 54);
            this.gridDB.Name = "gridDB";
            this.gridDB.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDB.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.gridDB.RowHeadersVisible = false;
            this.gridDB.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridDB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDB.Size = new System.Drawing.Size(545, 222);
            this.gridDB.TabIndex = 3;
            this.gridDB.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDB_CellClick);
            // 
            // btnRefreshTable
            // 
            this.btnRefreshTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshTable.BackgroundImage = global::PZO_V3.Properties.Resources.refresh;
            this.btnRefreshTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshTable.Location = new System.Drawing.Point(552, 33);
            this.btnRefreshTable.Name = "btnRefreshTable";
            this.btnRefreshTable.Size = new System.Drawing.Size(40, 40);
            this.btnRefreshTable.TabIndex = 62;
            this.btnRefreshTable.UseVisualStyleBackColor = true;
            this.btnRefreshTable.Click += new System.EventHandler(this.btnRefreshTable_Click);
            // 
            // rtbThird
            // 
            this.rtbThird.Location = new System.Drawing.Point(285, 5);
            this.rtbThird.Name = "rtbThird";
            this.rtbThird.Size = new System.Drawing.Size(133, 44);
            this.rtbThird.TabIndex = 82;
            this.rtbThird.Text = "";
            this.rtbThird.Visible = false;
            // 
            // rtbSecond
            // 
            this.rtbSecond.Location = new System.Drawing.Point(149, 5);
            this.rtbSecond.Name = "rtbSecond";
            this.rtbSecond.Size = new System.Drawing.Size(130, 44);
            this.rtbSecond.TabIndex = 83;
            this.rtbSecond.Text = "";
            // 
            // DBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 361);
            this.Controls.Add(this.panelDemands);
            this.Controls.Add(this.btnRefreshTable);
            this.Name = "DBForm";
            this.Text = "Kontragents";
            this.panelDemands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel panelDemands;
        private MetroFramework.Controls.MetroTextBox textBoxSearchKontagents;
        private System.Windows.Forms.Button btnInsertKontragents;
        private System.Windows.Forms.Button btnAddKontragents;
        private System.Windows.Forms.Button btnUpdateKontragents;
        private System.Windows.Forms.Button btnDeleteKontragents;
        private MetroFramework.Controls.MetroGrid gridDB;
        private System.Windows.Forms.Button btnExportKontragents;
        private System.Windows.Forms.Button btnRefreshTable;
        private MetroFramework.Controls.MetroTextBox tbId;
        public MetroFramework.Controls.MetroTextBox tbName;
        public System.Windows.Forms.RichTextBox rtbSecond;
        public System.Windows.Forms.RichTextBox rtbThird;
    }
}