using System;
using System.Windows.Forms;

namespace EmcTester_Interface_DemoUI
{

    // MainForm FromRecv
    public partial class MainForm : Form
    {

        private void FromRecv_Clear()
        {
            richTextBox_RecvLog.Clear();
        }

        private void FromRecv(string aFormat, params Object[] aArgs)
        {
            if (!string.IsNullOrEmpty(aFormat))
            {
                richTextBox_RecvLog.AppendText(string.Format(aFormat, aArgs));
            }
        }

        private void FromRecvLn(string aFormat = null, params Object[] aArgs)
        {
            FromRecv(aFormat, aArgs);
            richTextBox_RecvLog.AppendText(Environment.NewLine);
        }

        private void FromRecv_Update()
        {
            richTextBox_RecvLog.AppendText("--------------------" + DateTime.Now.ToString("G") + Environment.NewLine);
            string xDevicesInfo = Communicator_GetDevicesInfo();
            if (!string.IsNullOrEmpty(xDevicesInfo))
            {
                richTextBox_RecvLog.AppendText(xDevicesInfo + Environment.NewLine);
                richTextBox_RecvLog.Select(richTextBox_RecvLog.Text.Length - 1, 0);
                richTextBox_RecvLog.ScrollToCaret();
            }
        }

    } // class

} // namespace
