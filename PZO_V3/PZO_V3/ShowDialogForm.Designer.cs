namespace PZO_V3
{
    partial class ShowDialogForm
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
            this.btnLeft = new MetroFramework.Controls.MetroButton();
            this.btnRight = new MetroFramework.Controls.MetroButton();
            this.textBoxInput = new MetroFramework.Controls.MetroTextBox();
            this.labelText = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeft.Location = new System.Drawing.Point(59, 134);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 23);
            this.btnLeft.TabIndex = 0;
            this.btnLeft.Text = "Yes";
            this.btnLeft.UseSelectable = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRight.Location = new System.Drawing.Point(202, 134);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 23);
            this.btnRight.TabIndex = 1;
            this.btnRight.Text = "No";
            this.btnRight.UseSelectable = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.textBoxInput.CustomButton.Image = null;
            this.textBoxInput.CustomButton.Location = new System.Drawing.Point(53, 1);
            this.textBoxInput.CustomButton.Name = "";
            this.textBoxInput.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxInput.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxInput.CustomButton.TabIndex = 1;
            this.textBoxInput.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxInput.CustomButton.UseSelectable = true;
            this.textBoxInput.CustomButton.Visible = false;
            this.textBoxInput.Lines = new string[0];
            this.textBoxInput.Location = new System.Drawing.Point(108, 105);
            this.textBoxInput.MaxLength = 32767;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.PasswordChar = '\0';
            this.textBoxInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxInput.SelectedText = "";
            this.textBoxInput.SelectionLength = 0;
            this.textBoxInput.SelectionStart = 0;
            this.textBoxInput.ShortcutsEnabled = true;
            this.textBoxInput.Size = new System.Drawing.Size(100, 23);
            this.textBoxInput.TabIndex = 2;
            this.textBoxInput.UseSelectable = true;
            this.textBoxInput.Visible = false;
            this.textBoxInput.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxInput.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(50, 65);
            this.labelText.MaximumSize = new System.Drawing.Size(270, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(0, 0);
            this.labelText.TabIndex = 3;
            // 
            // ShowDialogForm
            // 
            this.AcceptButton = this.btnLeft;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 189);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Name = "ShowDialogForm";
            this.Text = "ShowDialogForm";
            this.Shown += new System.EventHandler(this.ShowDialogForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnLeft;
        private MetroFramework.Controls.MetroButton btnRight;
        private MetroFramework.Controls.MetroLabel labelText;
        public MetroFramework.Controls.MetroTextBox textBoxInput;
    }
}