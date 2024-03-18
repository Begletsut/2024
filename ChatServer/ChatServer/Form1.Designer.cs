namespace ChatServer
{
    partial class Form1
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
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.gridClients = new System.Windows.Forms.DataGridView();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelCounter = new System.Windows.Forms.Label();
            this.rtbIncomingMsg = new System.Windows.Forms.RichTextBox();
            this.rtbSent = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(12, 12);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 23);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click_1);
            // 
            // btnStopServer
            // 
            this.btnStopServer.Location = new System.Drawing.Point(124, 12);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(75, 23);
            this.btnStopServer.TabIndex = 1;
            this.btnStopServer.Text = "Stop Server";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click_1);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(67, 70);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(79, 13);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Server is offline";
            // 
            // gridClients
            // 
            this.gridClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridClients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User});
            this.gridClients.Location = new System.Drawing.Point(25, 117);
            this.gridClients.Name = "gridClients";
            this.gridClients.Size = new System.Drawing.Size(381, 150);
            this.gridClients.TabIndex = 3;
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.Width = 200;
            // 
            // labelCounter
            // 
            this.labelCounter.AutoSize = true;
            this.labelCounter.Location = new System.Drawing.Point(210, 57);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(13, 13);
            this.labelCounter.TabIndex = 4;
            this.labelCounter.Text = "0";
            // 
            // rtbIncomingMsg
            // 
            this.rtbIncomingMsg.Location = new System.Drawing.Point(434, 117);
            this.rtbIncomingMsg.Name = "rtbIncomingMsg";
            this.rtbIncomingMsg.Size = new System.Drawing.Size(229, 150);
            this.rtbIncomingMsg.TabIndex = 5;
            this.rtbIncomingMsg.Text = "";
            // 
            // rtbSent
            // 
            this.rtbSent.Location = new System.Drawing.Point(669, 117);
            this.rtbSent.Name = "rtbSent";
            this.rtbSent.Size = new System.Drawing.Size(196, 150);
            this.rtbSent.TabIndex = 6;
            this.rtbSent.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Incoming";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 298);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbSent);
            this.Controls.Add(this.rtbIncomingMsg);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.gridClients);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.btnStartServer);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.DataGridView gridClients;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.RichTextBox rtbIncomingMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.RichTextBox rtbSent;
        private System.Windows.Forms.Label label1;
    }
}

