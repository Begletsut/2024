namespace EmcTester_Interface_DemoUI.UI.Forms_PriliminaryTest
{
    partial class FormPreliminaryTest_ADC_Log
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
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Clear = new System.Windows.Forms.Button();
            this.checkBox_StartSop = new System.Windows.Forms.CheckBox();
            this.checkBox_GoToEnd = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Log.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox_Log.ForeColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox_Log.Location = new System.Drawing.Point(0, 32);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.ReadOnly = true;
            this.richTextBox_Log.Size = new System.Drawing.Size(800, 418);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            this.richTextBox_Log.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox_GoToEnd);
            this.panel1.Controls.Add(this.checkBox_StartSop);
            this.panel1.Controls.Add(this.button_Clear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 32);
            this.panel1.TabIndex = 1;
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(160, 6);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Clear.TabIndex = 2;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // checkBox_StartSop
            // 
            this.checkBox_StartSop.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_StartSop.AutoSize = true;
            this.checkBox_StartSop.Checked = true;
            this.checkBox_StartSop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_StartSop.Location = new System.Drawing.Point(12, 6);
            this.checkBox_StartSop.Name = "checkBox_StartSop";
            this.checkBox_StartSop.Size = new System.Drawing.Size(72, 23);
            this.checkBox_StartSop.TabIndex = 3;
            this.checkBox_StartSop.Text = "Start / Stop";
            this.checkBox_StartSop.UseVisualStyleBackColor = true;
            // 
            // checkBox_GoToEnd
            // 
            this.checkBox_GoToEnd.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_GoToEnd.AutoSize = true;
            this.checkBox_GoToEnd.Checked = true;
            this.checkBox_GoToEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_GoToEnd.Location = new System.Drawing.Point(90, 6);
            this.checkBox_GoToEnd.Name = "checkBox_GoToEnd";
            this.checkBox_GoToEnd.Size = new System.Drawing.Size(64, 23);
            this.checkBox_GoToEnd.TabIndex = 4;
            this.checkBox_GoToEnd.Text = "Go to end";
            this.checkBox_GoToEnd.UseVisualStyleBackColor = true;
            this.checkBox_GoToEnd.CheckedChanged += new System.EventHandler(this.checkBox_GoToEnd_CheckedChanged);
            // 
            // FormPriliminaryTest_ADC_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox_Log);
            this.Controls.Add(this.panel1);
            this.Name = "FormPriliminaryTest_ADC_Log";
            this.Text = "FormPriliminaryTest_ADC_Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPriliminaryTest_ADC_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.CheckBox checkBox_GoToEnd;
        private System.Windows.Forms.CheckBox checkBox_StartSop;
    }
}