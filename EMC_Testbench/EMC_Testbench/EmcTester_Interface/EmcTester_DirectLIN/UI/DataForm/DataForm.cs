using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EmcTester_DirectLIN.UI.DataForm
{
    public partial class DataForm : Form
    {

        UInt32 LineCount = 0;
        DateTime DtTm0 = default(DateTime);
        DateTime DtTm1 = default(DateTime);
        bool flFirst = true;

        public DataForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
        public DataForm(Form aOwner, List<string> aColumns): this()
        {
            Owner = aOwner;
            Text = Text + " (" + Owner.Text + ")";
            Icon = Owner.Icon;
            CreateColumns(aColumns);
        }

        private void DataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Owner != null)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void CreateColumns(List<string> aColumns)
        {
            ColumnHeader xColumnHeader = listViewData.Columns.Add("#");
            xColumnHeader.Width = 48;
            xColumnHeader = listViewData.Columns.Add("Ticks");
            xColumnHeader.Width = 60;
            foreach (string xColumnName in aColumns)
            {
                xColumnHeader = listViewData.Columns.Add(xColumnName);
                xColumnHeader.Width = 80;
            }
        }

        public void AddPoints(int[] aPoints)
        {
            LineCount++;

            if (flFirst)
            {
                flFirst = false;
                DtTm0 = DateTime.Now;
                DtTm1 = DtTm0;
            }
            DateTime xDtTm= DateTime.Now;
            label_TickPerPoint.Text = (xDtTm - DtTm1).TotalSeconds.ToString("0.000");
            DtTm1 = xDtTm;

            if (checkBox_Run.Checked)
            {
                listViewData.BeginUpdate();
                ListViewItem xItem = listViewData.Items.Add(LineCount.ToString());
                //xItem.SubItems.Add(((UInt32)(Ticks1 - Ticks0)).ToString());
                xItem.SubItems.Add((DtTm1 - DtTm0).TotalSeconds.ToString("0.000"));
                foreach (int xPoint in aPoints)
                {
                    xItem.SubItems.Add(xPoint.ToString());
                }

                numericUpDown_Lines_ValueChanged(null, null);
                listViewData.EndUpdate();
            }
        }

        private void numericUpDown_Lines_ValueChanged(object sender, EventArgs e)
        {
            while (listViewData.Items.Count > numericUpDown_Lines.Value)
            {
                listViewData.Items.RemoveAt(0);
            }
        }

    } // class

} // namespace
