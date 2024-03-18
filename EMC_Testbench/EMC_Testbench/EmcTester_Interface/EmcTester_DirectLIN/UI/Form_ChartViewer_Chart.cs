
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace EMC20_emuLambdaControl
{
    public partial class Form_ChartViewer : Form
    {
        public const int MAX_PARAMS_TO_DISPLAY_PER_CHART = 4;

        Dictionary<string, double> SeriesDef = new Dictionary<string, double>
        {
            { "OxyADC_Val", 1},
            { "OxyADC_Avg", 1},
            { "OxyADC_Filter", 1},
            { "DeltaDiff", 1},
            { "DeltaFilter", 1},
            { "DeltaIntgrate", 1},
            { "StepMotor_Speed", 1},
            { "StepMotor_Position", 1},
        };

        public void Chart_Init()
        {
            chart_1.Series.Clear();
            foreach (var x in SeriesDef)
            {
                var series = new Series
                {
                    Name = x.Key,
                    IsVisibleInLegend = true,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line
                };
                chart_1.Series.Add(series);
            }

            chart_1.Series[0].YAxisType = AxisType.Primary;
            chart_1.Series[1].YAxisType = AxisType.Secondary;


            chart_1.ChartAreas[0].AxisY.Title = "ADC values and step motor position x5";
            chart_1.ChartAreas[0].AxisY.Minimum = -100;
            chart_1.ChartAreas[0].AxisY.Maximum = 1400;
            chart_1.ChartAreas[0].AxisY.Interval = 100;

            chart_1.ChartAreas[0].AxisY2.Title = "Delta and step motor delta x10";
            chart_1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            chart_1.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            chart_1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart_1.ChartAreas[0].AxisY2.IsStartedFromZero = chart_1.ChartAreas[0].AxisY.IsStartedFromZero;
            chart_1.ChartAreas[0].AxisY2.Minimum = -200;
            chart_1.ChartAreas[0].AxisY2.Maximum = 200;
            chart_1.ChartAreas[0].AxisY2.Interval = 40;

            //int[] yValues = { 10, 25, 100 }; // Here y values is related to display three month values
            //string[] xValues = { "one", "two", "three" };
            //chart_1.Series[3].Points.DataBindXY(xValues, yValues);

            Chart_SizeChanged();
        }

        public void Chart_Update()
        {
            checkBox_Run.Checked = true;
        }

        private void Chart_SizeChanged()
        {
            timer1.Stop();

            chart_1.ChartAreas[0].AxisX.Maximum = (int)(1.1 * (int)numericUpDown_TimerFrame.Value);
            chart_1.ChartAreas[0].AxisX.Minimum = -10;
            //chart_1.ChartAreas[0].AxisX.Interval = 200;

            //chart_1.ChartAreas[0].AxisX.RoundAxisValues();
            //chart_1.ChartAreas[0].AxisY.RoundAxisValues();
            //chart_1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";

            timer1.Start();
        }

        private void Chart_AddSample(UInt32[] aPoins)
        {
            //label_Offset.Text = charTimeIndex.ToString();

            chart_1.Series.SuspendUpdates();
            int i = 0;
            while (i < chart_1.Series.Count)
            {
                DataPointCollection xPoints = chart_1.Series[i].Points;
                xPoints.Add(aPoins[i]);
                while (xPoints.Count > numericUpDown_TimerFrame.Value)
                {
                    xPoints.RemoveAt(0);
                }
                i++;
            }

            chart_1.Series.ResumeUpdates();
            chart_1.Series.Invalidate();
        }

    } // class

} // namespace
