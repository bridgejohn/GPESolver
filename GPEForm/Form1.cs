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
        private int offsetDBEC; // offset of the two BECs
        private double Energy; // Energy of Psi
        private double maxColor; //Max value of Colorbar

        //Erstellung des Plotbereichs
        private PlotModel myModel = new PlotModel { Title = "|Ψ|²"};
        private PlotModel potModel = new PlotModel { Title = "V" };
        private PlotModel timeModel = new PlotModel { Title = "Time Evolution of the BEC" };
        private PlotModel ColorBarModel = new PlotModel { };
        private PlotModel ColorBarModelE = new PlotModel { };
        private LineSeries plotPsi = new LineSeries(); //Erstellen des Datenreihe für Psi²
        private LineSeries plotPsi0 = new LineSeries(); //Erstellen des Datenreihe für Psi², Grundzustand
        private LineSeries plotPsiStart = new LineSeries();
        private LineSeries plotV = new LineSeries();
        private HeatMapSeries heatPsi = new HeatMapSeries();
        private HeatMapSeries ColorBarSeries = new HeatMapSeries();



        private GPESolver gpe;



        public Form1()
        {
            InitializeComponent();
            //read out of parameters
            mass = Convert.ToDouble(massTextBox.Text) * PhysConst.amu;
            sclength = Convert.ToDouble(StreuTextBox.Text) * Math.Pow(10, -9);
            anzahl = Convert.ToInt32(AnzahlTextBox.Text);
            wx = Convert.ToDouble(FrequenzTextBox.Text) * 2 * Math.PI;
            wr = Convert.ToDouble(RadFrequenzTextBox.Text) * 2 * Math.PI;
            tsteps = Convert.ToInt32(TimeStepsTextBox.Text);
            gpe = new GPESolver(mass, anzahl, sclength, wx, wr);
            for (int k = 0; k < gpe.psi.Length; k++)
            {
                plotV.Points.Add(new DataPoint(gpe.X[k], gpe.V[k]));
            }
            potModel.Series.Add(plotV);

            //Generate Plots
            this.plot1.Model = myModel; //Darstellen des Plots
            this.plot1.Model.Axes.Add(new LinearAxis() //Generate X-Axis
                                          {
                                           Title = "Postition [m]",
                                            Position = AxisPosition.Bottom,
                                          });
            this.plot1.Model.Axes.Add(new LinearAxis() //Generate Y-Axis
                                          {
                                            Title = "Density [1/m]",
                                            Position = AxisPosition.Left,
                                          });
            this.plot1.Model = timeModel; //Darstellen des Plots
            this.plot1.Model.Axes.Add(new LinearAxis() //Generate X-Axis
                                          {
                                           Title = "Position [m]",
                                            Position = AxisPosition.Bottom,
                                          });
            this.plot1.Model.Axes.Add(new LinearAxis() //Generate Y-Axis
                                          {
                                            Title = "Time [s]",
                                            Position = AxisPosition.Left,
                                          });
            this.plot1.Model = potModel; //Darstellen des Plots
            this.plot1.Model.Axes.Add(new LinearAxis() //Generate X-Axis
                                          {
                                           Title = "Position [m]",
                                            Position = AxisPosition.Bottom,
                                          });
            this.plot1.Model.Axes.Add(new LinearAxis() //Generate Y-Axis
                                          {
                                            Title = "Potential [J/hbar]",
                                            Position = AxisPosition.Left,
                                          });
            this.ColorBar.Model = ColorBarModel; //Darstellen des Plots
            this.ColorBar.Model.Axes.Add(new LinearAxis() //Generate Y-Axis
                                          {
                                            Title = "Density [1/m]",
                                            Position = AxisPosition.Right,
                                          });
            this.ColorBar.Model.Axes.Add(new LinearAxis() //Generate X-Axis
                                          {
                                            Position = AxisPosition.Bottom,
                                            IsAxisVisible = false,
                                          });
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            getParams(); //Auslesen der Parameter
            gpe = new GPESolver(mass, anzahl, sclength, wx,wr);// Erstellen des GPE-Solvers
            double[] normedPsi = new double[gpe.psi.Length]; //Erstellen eines Arrays für Psi²
            //Vorbereitung des Plots
            
            //Erstellen des Datenrasters zur Darstellung von Psi(t)
            heatPsi.X0 = gpe.X[0]; //Festlegen von xmin
            heatPsi.X1 = gpe.X[gpe.xSteps-1]; //Festlegen von xmin
            heatPsi.Y0 = 0; //Festlegen der Höhe des PLots
            heatPsi.Y1 = gpe.deltaT*tsteps;
            heatPsi.Interpolate = true; //Farbverlauf ein
            //heatPsi.RenderMethod = HeatMapRenderMethod.Bitmap;

            //Erstellen des Datenrasters zur Darstellung der Colorbar
            //ColorBarSeries.X0 = 0; //Festlegen von xmin
            //ColorBarSeries.X1 = 10; //Festlegen von xmin
            //ColorBarSeries.Y0 = 0; //Festlegen der Höhe des PLots
            //ColorBarSeries.Y1 = 10000;
            //ColorBarSeries.Interpolate = true; //Farbverlauf ein

            LinearColorAxis cAxis = new LinearColorAxis(); //Erstellen der Farbskala
            cAxis.Palette = OxyPalettes.Jet(100); //Verwendung der 'Jet-Skala'
            LinearColorAxis cAxisC = new LinearColorAxis(); //Erstellen der Farbskala
            cAxisC.Palette = OxyPalettes.Jet(100); //Verwendung der 'Jet-Skala'

            plotV.Points.Clear();
            for (int k = 0; k < gpe.psi.Length; k++)
            {
                plotV.Points.Add(new DataPoint(gpe.X[k], gpe.V[k]));
            }
            potModel.Series.Clear();
            potModel.Series.Add(plotV);
            this.plot1.Model = potModel; //Darstellen des Plots

            ColorBarModelE.Series.Clear();        //
            this.ColorBar.Model = ColorBarModelE; //Prevents bug with colorbar if time evolution is selected before starting calculation. Reason unknown.



            timeModel.Axes.Add(cAxis); //Hinzufügen der Farbskala
            ColorBarModel.Axes.Add(cAxisC);
            double[,] dataMap = new double[gpe.psi.Length, tsteps / 100]; //Erstellen des Datenarrays für den zeitabhängigen Plot
            double[,] ColorMap = new double[10, 10000]; //Erstellen des Datenarrays für die Colorbar

            int writeOut = 0; 

            Stopwatch Stopwatch1 = new Stopwatch(); //Erstellung der Stopuhr zum Messen der Laufzeit
            Stopwatch1.Start(); //Starten der Laufzeit

            //Entscheidung der Methode zur Lösung der GPE
            string method;


            if (FFTCheckBox.Checked)
            {
                method = "CT";
            } else if (DFTCheckBox.Checked)
            {
                method = "DFT";
            } else if (bitReverse.Checked)
            {
                method = "BR";
            } else
            {
                method = "BR";
            }

            ComplexNumber[] psiStart = (ComplexNumber[]) gpe.psi.Clone();

            if (getgroundstate.Checked)
            {
                gpe.getGroundState();
            }else if (DBECCheckBox.Checked)
            {
                offsetDBEC = Convert.ToInt32(OffsetDBECTextBox.Text);
                gpe.getDPsi(offsetDBEC);
            }




                for (int i=0; i<tsteps; i++)
            {
                    //Berechnung der Funktion Ψ(t) mit der Split-Step-Fourier Methode unter Benutzung des Cooley-Tukey-Algorithmus
                    gpe.splitStepFourier(method);
                    

                    //Füllen des Plotarrays mit den Werten aus der Berechnung für alle 100 Schritte
                    if (i == writeOut)
                    {
                        for (int k = 0; k < gpe.psi.Length; k++)
                        {
                            dataMap[k,i / 100] = Math.Pow(gpe.psi[k].Norm(), 2);

                        }
                        writeOut += 100;
                    }
            }
           
            Stopwatch1.Stop(); //Stoppen der Zeit nach Berechnung
            LaufzeitTextBox.Text = Convert.ToString(Stopwatch1.ElapsedMilliseconds); //Zeitausgabe in TextBox 
            listBox1.Items.Insert(0, method + "-FFT:" + " " + Convert.ToString(Stopwatch1.ElapsedMilliseconds) + "ms" + "  Timesteps" + tsteps.ToString()); //Hinzufügen der Laufzeit, der verwendeten Methode und der Anzahl der Zeitschritte in ListBox

            maxColor = OxyPlot.ArrayExtensions.Max2D(dataMap);
            for (int k = 0; k < 10000; k++)
            {
                for (int l = 0; l < 10; l++)
                {
                    ColorMap[l, k] = maxColor * k / 10000;
                }
            }

            //Erstellen des Datenrasters zur Darstellung der Colorbar
            ColorBarSeries.X0 = 0; //Festlegen von xmin
            ColorBarSeries.X1 = 10; //Festlegen von xmin
            ColorBarSeries.Y0 = 0; //Festlegen der Höhe des PLots
            ColorBarSeries.Y1 = maxColor;
            ColorBarSeries.Interpolate = true; //Farbverlauf ein

            // oxyplot models vorbereiten um dann in 
            heatPsi.Data = dataMap; //Überschreiben der berechneten Daten in das Plotarray
            ColorBarSeries.Data = ColorMap; //Überschreiben der berechneten Daten in das Plotarray
            timeModel.Series.Clear();
            timeModel.Series.Add(heatPsi); //
                                           //Schreiben der Daten von |Ψ|² in das Plotarray

            ColorBarModel.Series.Clear();
            ColorBarModel.Series.Add(ColorBarSeries);

            for (int k = 0; k < gpe.psi.Length; k++)
            {
                normedPsi[k] = Math.Pow(gpe.psi[k].Norm(), 2);
                plotPsi.Points.Add(new DataPoint(gpe.X[k], normedPsi[k]));
                normedPsi[k] = Math.Pow(psiStart[k].Norm(), 2);
                plotPsiStart.Points.Add(new DataPoint(gpe.X[k], normedPsi[k]));

            }

            myModel.Series.Clear();
            myModel.Series.Add(plotPsiStart);
            myModel.Series.Add(plotPsi); //

           
            this.plot1.Model = timeModel; //Darstellen des Plots
            this.ColorBar.Model = ColorBarModel; //Darstellen des Plots
            this.ColorBar.Visible = true;

            this.shiftPotButton.Enabled = true;

            //Energy = ETC.Hamilton(gpe.psi, gpe.V, gpe.deltaX, PhysConst.hbar, mass, gpe.g1D);
            //EnergieTextBox.Text = Convert.ToString(Energy); //Energie in TextBox 


            //if(RKCheckBox.Checked)
            //{
            //    Stopwatch Stopwatch3 = new Stopwatch();
            //    Stopwatch3.Start();

            //    //Runge-Kutta

            //    Stopwatch3.Stop();
            //    LaufzeitTextBox.Text = Convert.ToString(Stopwatch3.ElapsedMilliseconds);


            //    for (int i = 0; i < gpe.psi.Length; i++)
            //    {
            //        normedPsi[i] = Math.Pow(gpe.psi[i].Norm(), 2);
            //        plotPsi.Points.Add(new DataPoint(gpe.X[i], normedPsi[i]));

            //    }

            //    myModel.Series.Add(plotPsi);


            //}





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
            //getParams();
            listBox1.Items.Clear();
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

        private void timeEvolutionButton_Click(object sender, EventArgs e)
        {
            this.plot1.Model = timeModel;
            this.ColorBar.Model = ColorBarModel;
            this.ColorBar.Visible = true;
        }

        private void psiPlotButton_Click(object sender, EventArgs e)
        {
            this.plot1.Model = myModel;
            this.ColorBar.Model = ColorBarModel;
            this.ColorBar.Visible = false;
        }

        private void potentialButton_Click(object sender, EventArgs e)
        {
            this.plot1.Model = potModel;
            this.ColorBar.Model = ColorBarModel;
            this.ColorBar.Visible = false;
        }

        private void StreuTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void massTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AnzahlTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void shiftPotButton_Click(object sender, EventArgs e)
        {
            gpe.changeCenterOfV(Math.Pow(10, -5) * 1);
            plotV.Points.Clear();
            for (int k = 0; k < gpe.psi.Length; k++)
            {
                plotV.Points.Add(new DataPoint(gpe.X[k], gpe.V[k]));
            }
            potModel.Series.Clear();
            potModel.Series.Add(plotV);
            this.plot1.Model = potModel;

            double[,] dataMap = new double[gpe.psi.Length, tsteps / 100];
            int writeOut = 0;
            for (int i = 0; i < tsteps; i++)
            {
                //Berechnung der Funktion Ψ(t) mit der Split-Step-Fourier Methode unter Benutzung des Cooley-Tukey-Algorithmus
                gpe.splitStepFourier("BR");


                //Füllen des Plotarrays mit den Werten aus der Berechnung für alle 100 Schritte
                if (i == writeOut)
                {
                    for (int k = 0; k < gpe.psi.Length; k++)
                    {
                        dataMap[k, i / 100] = Math.Pow(gpe.psi[k].Norm(), 2);

                    }
                    writeOut += 100;
                }
            }

            //ColorMap
            maxColor = OxyPlot.ArrayExtensions.Max2D(dataMap);
            ColorBarSeries.Y1 = maxColor;

            heatPsi.Data = dataMap; //Überschreiben der berechneten Daten in das Plotarray
            timeModel.Series.Clear();
            timeModel.Series.Add(heatPsi);

            this.plot1.Model = timeModel;

        }

        private void LaufzeitTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DBECCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DBECCheckBox.Checked)
            {
                OffsetDBECTextBox.Enabled = true;
            }else
            {
                OffsetDBECTextBox.Enabled = false;
            }
            
        }

        private void OffsetDBEC_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrequenzTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
