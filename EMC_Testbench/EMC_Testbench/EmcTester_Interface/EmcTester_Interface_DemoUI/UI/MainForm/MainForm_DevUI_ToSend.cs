using System;
using System.Drawing;
using System.Windows.Forms;

using EmcTester_Interface;

namespace EmcTester_Interface_DemoUI
{

    // MainForm DevicesUI 
    public partial class MainForm : Form
    {

        private const UInt16 CONTROLS_WIDTH_MIN = 64;
        private const UInt16 CONTROLS_HEIGHT = 24;
        private const UInt16 CONTROLS_X0 = 6;
        private const UInt16 CONTROLS_Y0 = 6;

        public void ToSend_Init()
        {
            panel_Devices.AutoScroll = false;
            panel_Devices.HorizontalScroll.Enabled = false;
            panel_Devices.HorizontalScroll.Visible = false;
            panel_Devices.HorizontalScroll.Maximum = 0;
            panel_Devices.AutoScroll = true;

            while (panel_Devices.Controls.Count > 0)
            {
                panel_Devices.Controls.Remove(panel_Devices.Controls[0]);
            }

            for (int i = 0; i < DEVICES_COUNT_MAX; i++)
            {
                ToSend_CreateCheckBox(panel_Devices, string.Format("Id_{0:d3}", i + 1), (byte)(i + 1));
            }
            ToSend_Resize();
        }

        private CheckBox ToSend_CreateCheckBox(Control aParent, string aTitle, byte aTag)
        {
            CheckBox xResult = new CheckBox();
            xResult.Tag = aTag;
            xResult.Text = aTitle;
            xResult.AutoSize = false;
            xResult.Size = new Size(CONTROLS_WIDTH_MIN, CONTROLS_HEIGHT);
            xResult.FlatStyle = FlatStyle.Flat;
            xResult.FlatAppearance.BorderSize = 0;
            xResult.CheckedChanged += DeviceUI_CheckBox_CheckedChanged;
            xResult.Enabled = false;
            aParent.Controls.Add(xResult);
            return xResult;
        }

        private void DeviceUI_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool xAll = true;
            bool xNone = true;
            foreach (Control xControl in panel_Devices.Controls)
            {
                if ((xControl.Tag is byte) && (xControl is CheckBox))
                {
                    if (((CheckBox)xControl).Checked)
                    {
                        xNone = false;
                    }
                    else
                    {
                        xAll = false;
                    }
                    if ((!xAll) && (!xNone))
                    {
                        break;
                    }
                }
            }
            checkBox_Devices_SelectALL.Checked = xAll;
            checkBox_Devices_SelectNone.Checked = xNone;
        }

        private void ToSend_CheckAll(bool aChecked)
        {
            foreach (Control xControl in panel_Devices.Controls)
            {
                if ((xControl.Tag is byte) && (xControl is CheckBox))
                {
                    ((CheckBox)xControl).Checked = aChecked;
                }
            }
        }

        private void ToSend_Resize()
        {
            int W = panel_Devices.Width - CONTROLS_X0 - CONTROLS_X0;
            if (W > CONTROLS_WIDTH_MIN)
            {
                int W1 = (W - CONTROLS_X0) / CONTROLS_WIDTH_MIN;
                int X_delta = (W - CONTROLS_X0) / W1;
                int X0 = CONTROLS_X0;
                int Y0 = CONTROLS_Y0;

                foreach (Control xControl in panel_Devices.Controls)
                {
                    if ((xControl.Tag is byte) && (xControl is CheckBox))
                    {
                        xControl.Location = new Point(X0, Y0);
                        X0 += X_delta;
                        if ((X0 + X_delta) > W)
                        {
                            X0 = CONTROLS_X0;
                            Y0 += CONTROLS_HEIGHT;
                        }
                    }
                }
            }
        }

        private Control ToSend_GetControlById(byte aTesterId)
        {
            foreach (Control xControl in panel_Devices.Controls)
            {
                if ((xControl.Tag is byte) && ((byte)xControl.Tag == aTesterId))
                {
                    return xControl;
                }
            }
            return null;
        }

        private void ToSend_TimerUI_Tick()
        {
            foreach (Control xControl in panel_Devices.Controls)
            {
                if (xControl.Tag is byte)
                {
                    EmcTester_Device xDevice = Communicator_GetDeviceById((byte)(xControl.Tag));
                    if (xDevice != null)
                    {
                        ((CheckBox)xControl).Enabled = xDevice.StateTester == EmcTester_Device.State_Tester.Ready;
                    }
                }
            }
        }


    } // class

} // namespace
