using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EmcTester_Interface_DemoUI.UI.Forms_PriliminaryTest
{
    public partial class FormPreliminaryTest_ADC_Grid : Form
    {
        private int CellWidth = 200;
        private int CellHeight = 24;
        private Dictionary<UInt32, Label> ValuesADC = new Dictionary<UInt32, Label>();
        private UInt32[] Cnt = new UInt32[40];

        public FormPreliminaryTest_ADC_Grid()
        {
            InitializeComponent();
        }

        private void FormPriliminaryTest_ADC_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void NewValueADC(UInt32 aId, bool aIsValid, int aValue)
        {
            if (!ValuesADC.ContainsKey(aId))
            {
                Label xLabel = new Label();
                xLabel.Text = "Label_ADC_" + aId.ToString();
                xLabel.AutoSize = false;
                xLabel.Size = new Size(CellWidth, CellHeight);
                xLabel.Tag = aId;
                xLabel.BackColor = Color.Yellow;
                Controls.Add(xLabel);
                ValuesADC.Add(aId, xLabel);
                FormPriliminaryTest_ADC_Resize(null, null);
            }
            if (aId<40)
            {
                Cnt[aId]++;
            }
            ValuesADC[aId].Text = string.Format("{0,4}{1}:{2,6} ({3})", aId, aIsValid ? "v" : "_", aValue, Cnt[aId]); 
        }

        private void FormPriliminaryTest_ADC_Resize(object sender, System.EventArgs e)
        {
            int xCount = (Width / CellWidth) + 1;
            int xPosDlt = Width / xCount;
            foreach (Control xControl in Controls)
            {
                if (xControl.Tag is UInt32)
                {
                    xControl.Size = new Size(xPosDlt, CellHeight);
                    UInt32 xIndex = (UInt32)xControl.Tag;
                    int xPosX = (int)(xPosDlt * (xIndex % xCount));
                    int xPosY = (int)(CellHeight * (1 + (xIndex / xCount)));
                    xControl.Location = new Point(xPosX, xPosY);
                }
            }
        }

    } // class

} // namespace
