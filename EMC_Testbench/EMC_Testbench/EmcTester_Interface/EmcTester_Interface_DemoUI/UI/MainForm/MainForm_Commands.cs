using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace EmcTester_Interface_DemoUI
{

    // MainForm Control
    public partial class MainForm : Form
    {

        private void Command_CreateAndSend(Func<byte[], bool> aSend_CAN)
        {
            if (!checkBox_Devices_SelectNone.Checked)
            {
                if (checkBox_Devices_SelectALL.Checked)
                {
                    aSend_CAN(null);
                }
                else
                {
                    List<byte> xListTesterIds = new List<byte>();
                    foreach (Control xControl in panel_Devices.Controls)
                    {
                        if (((byte)xControl.Tag > 0) && ((byte)xControl.Tag < byte.MaxValue) && (xControl is CheckBox) && ((CheckBox)xControl).Checked)
                        {
                            xListTesterIds.Add((byte)((CheckBox)xControl).Tag);
                        }
                    }
                    while (xListTesterIds.Count > 0)
                    {
                        byte xLen = (byte)(xListTesterIds.Count > 8 ? 8 : xListTesterIds.Count);
                        byte[] xTesterIds = Command_GetBytesFromList(xListTesterIds, xLen);
                        if (xTesterIds != null)
                        {
                            aSend_CAN(xTesterIds);
                        }
                    }
                }
            }
        }

        private byte[] Command_GetBytesFromList(List<byte> aListTesterIds, byte aLen)
        {
            byte[] xResult = null;
            if ((aListTesterIds != null) && (aListTesterIds.Count >= aLen))
            {
                xResult = new byte[aLen];
                int i = 0;
                while (i < aLen)
                {
                    xResult[i] = aListTesterIds[0];
                    aListTesterIds.RemoveAt(0);
                    i++;
                }
            }
            return xResult;
        }

    } // class

} // namespace
