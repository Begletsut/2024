using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using EmcTester_DirectLIN.UI.DataForm;

namespace EmcTester_DirectLIN
{
    public partial class Form_DirectLIN : Form
    {
        string AppTitle;
        DataForm DataForm;

        public Form_DirectLIN()
        {
            InitializeComponent();

            Assembly xAssembly = Assembly.GetEntryAssembly();
            AppTitle = xAssembly.GetName().Name; // !!!started app
            Text = AppTitle;

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            SerialPort_Init();

            timer1.Interval = 50;
            timer1.Start();
        }

        private void Form_ChartViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((DataForm != null) && (DataForm.Owner != null))
            {
                DataForm.Owner = null;
                DataForm.Close();
                DataForm = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            //if (DataForm == null) // TODO: fix problem in owner as child form
            //{
            //    Close();
            //}
            //else
            //{
            //    SerialPort_Update();
            //}
            SerialPort_Update();
            timer1.Start();
        }

        private void button_SetEmcToTestMode_Click(object sender, EventArgs e)
        {
            SerialPort_SetEmcToTestMode();
            //SerialPort_SetCommunicator_Line();
        }

        private void button_SendStr_Click(object sender, EventArgs e)
        {
            SerialPort_SendStr(TextBox_ToSend.Text);
        }

        private void checkBox_TestLoop_CheckedChanged(object sender, EventArgs e)
        {
            SerialPort_TestLoop_Run = true;
        }

    } // class

} // namespace
