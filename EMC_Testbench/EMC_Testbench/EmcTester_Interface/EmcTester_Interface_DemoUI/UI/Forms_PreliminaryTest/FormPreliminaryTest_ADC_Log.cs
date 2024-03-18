using System;
using System.Text;
using System.Windows.Forms;

namespace EmcTester_Interface_DemoUI.UI.Forms_PriliminaryTest
{
    public partial class FormPreliminaryTest_ADC_Log : Form
    {
        StringBuilder sb = new StringBuilder();

        public FormPreliminaryTest_ADC_Log()
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
            if (checkBox_StartSop.Checked)
            {
                sb.AppendFormat("{0,4}{1}:{2,6}", aId, aIsValid ? "v" : "_", aValue);
                if (aId == 15)
                {
                    richTextBox_Log.AppendText(sb.ToString() + Environment.NewLine);
                    sb.Clear();
                    if (richTextBox_Log.Text.Length>100000)
                    {
                        richTextBox_Log.Text = richTextBox_Log.Text.Substring(20000);
                    }
                    checkBox_GoToEnd_CheckedChanged(null, null);
                }
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            richTextBox_Log.Clear();
        }

        private void checkBox_GoToEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_GoToEnd.Checked)
            {
                richTextBox_Log.SelectionStart = richTextBox_Log.Text.Length;
                richTextBox_Log.ScrollToCaret();
            }
        }

    } // class

} // namespace
