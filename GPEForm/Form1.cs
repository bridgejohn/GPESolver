using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using OurMaths;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace GPEForm
{
    public partial class Form1 : Form
    {
        private double mass; // atom mass
        private double sclength; //scattering length
        private int anzahl; // number of Atoms
        private double wx; //trap frequency
        private double wr; // radial trap frequency
        private int tsteps; // number of time steps

        public Form1()
        {
            InitializeComponent();
            //read out of parameters
            mass = Convert.ToDouble(massTextBox.Text)*PhysConst.amu;
            sclength = Convert.ToDouble(StreuTextBox.Text) * Math.Pow(10, -9);
            anzahl = Convert.ToInt32(AnzahlTextBox.Text);
            wx = Convert.ToDouble(FrequenzTextBox.Text) * 2 * Math.PI;
            wr = Convert.ToDouble(RadFrequenzTextBox.Text) * 2 * Math.PI;
            tsteps = Convert.ToInt32(TimeStepsTextBox.Text);

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            getParams();
            GPESolver gpe = new GPESolver(mass, anzahl, sclength, wx,wr);
            double[] normedPsi = new double[gpe.psi.Length];
            LineSeries plotPsi = new LineSeries();
            HeatMapSeries heatPsi = new HeatMapSeries();
            heatPsi.X0 = gpe.X[0];
            heatPsi.X1 = gpe.X[gpe.xSteps-1];
            heatPsi.Y0 = 0;
            heatPsi.Y1 = gpe.deltaT*tsteps;
            heatPsi.Interpolate = true;
            //heatPsi.RenderMethod = HeatMapRenderMethod.Bitmap;

            LinearColorAxis cAxis = new LinearColorAxis();
            cAxis.Palette = OxyPalettes.Jet(100);


            PlotModel myModel = new PlotModel { Title = "Example 1" };
            PlotModel myModel2 = new PlotModel { Title = "Time Evolution of the BEC" };

            myModel2.Axes.Add(cAxis);

            if (FFTCheckBox.Checked)
            {
                Stopwatch Stopwatch1 = new Stopwatch();
                Stopwatch1.Start();

                //FFT-CT
                double[,] dataMap = new double[gpe.psi.Length,tsteps/100];
                int writeOut = 0;
                for (int i=0; i<tsteps; i++)
                {
                    gpe.splitStepFourier("CT");

                    if (i == writeOut)
                    {
                        for (int k = 0; k < gpe.psi.Length; k++)
                        {
                            dataMap[k,i / 100] = Math.Pow(gpe.psi[k].Norm(), 2);

                        }
                        writeOut += 100;
                    }
                }

                Stopwatch1.Stop();
                LaufzeitTextBox.Text = Convert.ToString(Stopwatch1.ElapsedMilliseconds);
                //listBox1.Text = Convert.ToString(Stopwatch1.ElapsedMilliseconds);
                listBox1.Items.Insert(0, "CT-FFT:" + " " + Convert.ToString(Stopwatch1.ElapsedMilliseconds) + "ms" + "  Timesteps" + tsteps.ToString());


                for (int i = 0; i < gpe.psi.Length; i++)
                {
                    normedPsi[i] = Math.Pow(gpe.psi[i].Norm(), 2);
                    plotPsi.Points.Add(new DataPoint(gpe.X[i], normedPsi[i]));

                }
                heatPsi.Data = dataMap;
                myModel2.Series.Add(heatPsi);
                myModel.Series.Add(plotPsi);

            }

            if(DFTCheckBox.Checked)
            {
               
                Stopwatch Stopwatch2 = new Stopwatch();
                Stopwatch2.Start();

                double[,] dataMap = new double[gpe.psi.Length, tsteps / 10];
                int writeOut = 0;
                for (int i = 0; i < tsteps; i++)
                {
                    gpe.splitStepFourier("DFT");

                    if (i == writeOut)
                    {
                        for (int k = 0; k < gpe.psi.Length; k++)
                        {
                            dataMap[k,i / 10] = Math.Pow(gpe.psi[k].Norm(), 2);

                        }
                        writeOut += 10;
                    }
                }
                Stopwatch2.Stop();
                LaufzeitTextBox.Text = Convert.ToString(Stopwatch2.ElapsedMilliseconds);
                listBox1.Items.Insert(0, "DFT:" + " " + Convert.ToString(Stopwatch2.ElapsedMilliseconds) + "ms" + "  Timesteps" + tsteps.ToString());


                for (int i = 0; i < gpe.psi.Length; i++)
                {
                    normedPsi[i] = Math.Pow(gpe.psi[i].Norm(), 2);
                    plotPsi.Points.Add(new DataPoint(gpe.X[i], normedPsi[i]));

                }
                heatPsi.Data = dataMap;
                myModel2.Series.Add(heatPsi);
                myModel.Series.Add(plotPsi);


            }

            if (bitReverse.Checked)
            {

                Stopwatch Stopwatch2 = new Stopwatch();
                Stopwatch2.Start();
                //gpe.getGroundState();
                double[,] dataMap = new double[gpe.psi.Length, tsteps / 100];
                int writeOut = 0;
                for (int i = 0; i < tsteps; i++)
                {
                    gpe.splitStepFourier("BR");

                    if (i == writeOut)
                    {
                        for (int k = 0; k < gpe.psi.Length; k++)
                        {
                            dataMap[k,i/100] = Math.Pow(gpe.psi[k].Norm(), 2);

                        }
                        writeOut += 100;
                    }
                }
                Stopwatch2.Stop();
                LaufzeitTextBox.Text = Convert.ToString(Stopwatch2.ElapsedMilliseconds);
                listBox1.Items.Insert(0, "BR-FFT:" + " " + Convert.ToString(Stopwatch2.ElapsedMilliseconds) + "ms" + "  Timesteps" + tsteps.ToString());



                for (int i = 0; i < gpe.psi.Length; i++)
                {
                    normedPsi[i] = Math.Pow(gpe.psi[i].Norm(), 2);
                    plotPsi.Points.Add(new DataPoint(gpe.X[i], normedPsi[i]));

                }
                heatPsi.Data = dataMap;
                myModel2.Series.Add(heatPsi);
                myModel.Series.Add(plotPsi);


            }


            if(RKCheckBox.Checked)
            {
                Stopwatch Stopwatch3 = new Stopwatch();
                Stopwatch3.Start();

                //Runge-Kutta

                Stopwatch3.Stop();
                LaufzeitTextBox.Text = Convert.ToString(Stopwatch3.ElapsedMilliseconds);


                for (int i = 0; i < gpe.psi.Length; i++)
                {
                    normedPsi[i] = Math.Pow(gpe.psi[i].Norm(), 2);
                    plotPsi.Points.Add(new DataPoint(gpe.X[i], normedPsi[i]));

                }

                myModel.Series.Add(plotPsi);


            }
            
            //this.plot2.Model = myModel;
            this.plot1.Model = myModel2;




            //DrawFunction(normedPsi);

            //LinearAxis LAX = new LinearAxis()
            //{
            //    Position = OxyPlot.Axes.AxisPosition.Bottom,
            //    AbsoluteMaximum = Math.Pow(10,-6),
            //    AbsoluteMinimum = -Math.Pow(10, -6),
            //};
            //LinearAxis LAY = new LinearAxis()
            //{
            //    Position = OxyPlot.Axes.AxisPosition.Left,
            //    AbsoluteMaximum = 4*Math.Pow(10, 8),
            //    AbsoluteMinimum = 0,
            //};
            //myModel.Axes.Add(LAX);
            //myModel.Axes.Add(LAY);
            //myModel.Series.Add(plotPsi);



        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ParameterButton_Click(object sender, EventArgs e)
        {
            getParams();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void getParams()
        {
            mass = Convert.ToDouble(massTextBox.Text) * PhysConst.amu;
            sclength = Convert.ToDouble(StreuTextBox.Text) * Math.Pow(10, -9);
            anzahl = Convert.ToInt32(AnzahlTextBox.Text);
            wx = Convert.ToDouble(FrequenzTextBox.Text) * 2 * Math.PI;
            wr = Convert.ToDouble(RadFrequenzTextBox.Text) * 2 * Math.PI;
            tsteps = Convert.ToInt32(TimeStepsTextBox.Text);
        }

        private void AnzahlTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void massTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void StreuTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrequenzTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void RadFrequenzTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void TimeStepsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LaufzeitTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
