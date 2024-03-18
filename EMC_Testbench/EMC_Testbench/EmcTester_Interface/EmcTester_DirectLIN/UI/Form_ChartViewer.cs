using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using isf_core;

namespace EMC20_emuLambdaControl
{
    public partial class Form_ChartViewer : Form
    {
        string AppTitle;

        public Form_ChartViewer()
        {
            InitializeComponent();

            Assembly xAssembly = Assembly.GetEntryAssembly();
            AppTitle = xAssembly.GetName().Name; // !!!started app
            Text = AppTitle + ": " + Common.GetVersion(xAssembly);

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            timer1.Interval = 50;

            Chart_Init();
            SerialPort_Init();
        }

        private void Form_ISF_ChartViewer_Load(object sender, EventArgs e)
        {
            Chart_Update();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SerialPort_Update();
            Chart_Update();
        }

        private void numericUpDown_TimerFrame_ValueChanged(object sender, EventArgs e)
        {
            Chart_SizeChanged();
        }

    } // class

} // namespace
