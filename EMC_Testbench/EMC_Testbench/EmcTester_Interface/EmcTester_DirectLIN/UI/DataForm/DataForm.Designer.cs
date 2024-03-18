using System.Drawing;
using System.Windows.Forms;

namespace EmcTester_DirectLIN.UI.DataForm
{
    partial class DataForm
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
            this.listViewData = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_Lines = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Run = new System.Windows.Forms.CheckBox();
            this.label_TickPerPoint = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Lines)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewData
            // 
            this.listViewData.BackColor = System.Drawing.SystemColors.Info;
            this.listViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewData.ForeColor = System.Drawing.SystemColors.InfoText;
            this.listViewData.FullRowSelect = true;
            this.listViewData.GridLines = true;
            this.listViewData.HideSelection = false;
            this.listViewData.Location = new System.Drawing.Point(0, 32);
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(800, 418);
            this.listViewData.TabIndex = 0;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_TickPerPoint);
            this.panel1.Controls.Add(this.checkBox_Run);
            this.panel1.Controls.Add(this.numericUpDown_Lines);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 32);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lines : ";
            // 
            // numericUpDown_Lines
            // 
            this.numericUpDown_Lines.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Lines.Location = new System.Drawing.Point(59, 6);
            this.numericUpDown_Lines.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown_Lines.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_Lines.Name = "numericUpDown_Lines";
            this.numericUpDown_Lines.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown_Lines.TabIndex = 3;
            this.numericUpDown_Lines.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_Lines.ValueChanged += new System.EventHandler(this.numericUpDown_Lines_ValueChanged);
            // 
            // checkBox_Run
            // 
            this.checkBox_Run.AutoSize = true;
            this.checkBox_Run.Checked = true;
            this.checkBox_Run.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Run.Location = new System.Drawing.Point(133, 8);
            this.checkBox_Run.Name = "checkBox_Run";
            this.checkBox_Run.Size = new System.Drawing.Size(46, 17);
            this.checkBox_Run.TabIndex = 7;
            this.checkBox_Run.Text = "Run";
            this.checkBox_Run.UseVisualStyleBackColor = true;
            // 
            // label_TickPerPoint
            // 
            this.label_TickPerPoint.AutoSize = true;
            this.label_TickPerPoint.Location = new System.Drawing.Point(204, 9);
            this.label_TickPerPoint.Name = "label_TickPerPoint";
            this.label_TickPerPoint.Size = new System.Drawing.Size(22, 13);
            this.label_TickPerPoint.TabIndex = 10;
            this.label_TickPerPoint.Text = "- - -";
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewData);
            this.Controls.Add(this.panel1);
            this.Name = "DataForm";
            this.Text = "Data from";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Lines)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewData;
        private System.Windows.Forms.Panel panel1;
        private NumericUpDown numericUpDown_Lines;
        private Label label1;
        private CheckBox checkBox_Run;
        private Label label_TickPerPoint;
    }
}