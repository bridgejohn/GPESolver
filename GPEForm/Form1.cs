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
            getParams(); //Auslesen der Parameter
            GPESolver gpe = new GPESolver(mass, anzahl, sclength, wx,wr);// Erstellen des GPE-Solvers
            double[] normedPsi = new double[gpe.psi.Length]; //Erstellen eines Arrays für Psi²

            //Vorbereitung des Plots
            LineSeries plotPsi = new LineSeries(); //Erstellen des Datenreihe für Psi²
            HeatMapSeries heatPsi = new HeatMapSeries(); //Erstellen des Datenrasters zur Darstellung von Psi(t)
            heatPsi.X0 = gpe.X[0]; //Festlegen von xmin
            heatPsi.X1 = gpe.X[gpe.xSteps-1]; //Festlegen von xmin
            heatPsi.Y0 = 0; //Festlegen der Höhe des PLots
            heatPsi.Y1 = gpe.deltaT*tsteps;
            heatPsi.Interpolate = true; //Farbverlauf ein
            //heatPsi.RenderMethod = HeatMapRenderMethod.Bitmap;

            LinearColorAxis cAxis = new LinearColorAxis(); //Erstellen der Farbskala
            cAxis.Palette = OxyPalettes.Jet(100); //Verwendung der 'Jet-Skala'

            //Erstellung des Plotbereichs
            PlotModel myModel = new PlotModel { Title = "|Ψ|²" }; 
            PlotModel timeModel = new PlotModel { Title = "Time Evolution of the BEC" };

            timeModel.Axes.Add(cAxis); //Hinzufügen der Farbskala
            double[,] dataMap = new double[gpe.psi.Length, tsteps / 100]; //Erstellen des Datenarrays für den zeitabhängigen Plot

            int writeOut = 0; 

            Stopwatch Stopwatch1 = new Stopwatch(); //Erstellung der Stopuhr zum Messen der Laufzeit
            Stopwatch1.Start(); //Starten der Laufzeit

            //Entscheidung der Methode zur Lösung der GPE
            if (FFTCheckBox.Checked)
            {
                for (int i=0; i<tsteps; i++)
                {
                    //Berechnung der Funktion Ψ(t) mit der Split-Step-Fourier Methode unter Benutzung des Cooley-Tukey-Algorithmus
                    gpe.splitStepFourier("CT"); 

                    //Füllen des Plotarrays mit den Werten aus der Berechnung für alle 100 Schritte
                    if (i == writeOut)
                    {
                        for (int k = 0; k < gpe.psi.Length; k++)
                        {
                            dataMap[k,i / 100] = Math.Pow(gpe.psi[k].Norm(), 2);

                        }
                        writeOut += 100;
                    }
                    //Schreiben der Daten von |Ψ|² in das Plotarray
                    for (int k = 0; k < gpe.psi.Length; k++)
                    {
                        normedPsi[k] = Math.Pow(gpe.psi[k].Norm(), 2);
                        plotPsi.Points.Add(new DataPoint(gpe.X[k], normedPsi[k]));
                    }

                }
            }
            if (DFTCheckBox.Checked)
            {
                for (int i = 0; i < tsteps; i++)
                {
                    //Berechnung der Funktion Ψ(t) mit der Split-Step-Fourier Methode unter Benutzung der Diskreten FT
                    gpe.splitStepFourier("DFT");

                    //Füllen des Plotarrays mit den Werten aus der Berechnung für alle 10 Schritte
                    if (i == writeOut)
                    {
                        for (int k = 0; k < gpe.psi.Length; k++)
                        {
                            dataMap[k, i / 10] = Math.Pow(gpe.psi[k].Norm(), 2);

                        }
                        writeOut += 10;
                    }
                    //Schreiben der Daten von |Ψ|² in das Plotarray
                    for (int k = 0; k < gpe.psi.Length; k++)
                    {
                        normedPsi[k] = Math.Pow(gpe.psi[k].Norm(), 2);
                        plotPsi.Points.Add(new DataPoint(gpe.X[k], normedPsi[k]));
                    }

                }
            }
            if (bitReverse.Checked)
            {
                for (int i = 0; i < tsteps; i++)
                {
                    //Berechnung der Funktion Ψ(t) mit der Split-Step-Fourier Methode unter Benutzung der Bit-Reverse Methode
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
                    //Schreiben der Daten von |Ψ|² in das Plotarray
                    for (int k = 0; k < gpe.psi.Length; k++)
                    {
                        normedPsi[k] = Math.Pow(gpe.psi[k].Norm(), 2);
                        plotPsi.Points.Add(new DataPoint(gpe.X[k], normedPsi[k]));
                    }

                }
            }
            
            Stopwatch1.Stop(); //Stoppen der Zeit nach Berechnung
            LaufzeitTextBox.Text = Convert.ToString(Stopwatch1.ElapsedMilliseconds); //Zeitausgabe in TextBox 
            listBox1.Items.Insert(0, "CT-FFT:" + " " + Convert.ToString(Stopwatch1.ElapsedMilliseconds) + "ms" + "  Timesteps" + tsteps.ToString()); //Hinzufügen der Laufzeit, der verwendeten Methode und der Anzahl der Zeitschritte in ListBox

            

            
            

            //Plotauswahl
            if (timePlotCheckBox.Checked)
            {
                heatPsi.Data = dataMap; //Überschreiben der berechneten Daten in das Plotarray
                timeModel.Series.Add(heatPsi); //
                this.plot1.Model = timeModel; //Darstellen des Plots
            }
            else
            {
                myModel.Series.Add(plotPsi); //
                this.plot1.Model = myModel; // Darstellen des Plots
            }

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
