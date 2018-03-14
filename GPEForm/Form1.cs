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

// Using Oxyplot to show the results
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace GPEForm
{
    /// <summary>
    /// Form 1 is the User Interface. 
    /// It is used to manage which simulation should be executed and which plot should be shown.
    /// The variations possible are a calculation of the time-development using the split-step-fourier 
    /// method, where you can choose between the Cooley-Tukey-algorithm and the bit-reverse-algorithm
    /// for the fast-fourier-transformation or the discrete fourier transformation.
    /// It is also possible to calculate the ground state of the wavefunction or to see the time development
    /// of two BECs or of a BEC in a shifted potential.
    /// </summary>
    /// 
    public partial class Form1 : Form
    {
        private double mass; // atom mass
        private double sclength; //scattering length
        private int anzahl; // number of Atoms
        private double wx; //trap frequency
        private double wr; // radial trap frequency
        private int tsteps; // number of time steps
        private int offsetDBEC; // offset of the two BECs
        //private double Energy; // Energy of Psi   kann die gelöscht werden?
        private double maxColor; //Max value of Colorbar

        //Creation of the plot area
        //Creation of the three plot models to picture the starting and final |Ψ|², the potential 
        //and the time evolution of |Ψ|²
        private PlotModel myModel = new PlotModel { Title = "|Ψ|²"};
        private PlotModel potModel = new PlotModel { Title = "V" };
        private PlotModel timeModel = new PlotModel { Title = "Time Evolution of the BEC" };
        //Creation of two color bars for the time-evolution plot
        private PlotModel ColorBarModel = new PlotModel { };
        private PlotModel ColorBarModelE = new PlotModel { };
        //Creation of the data series, that can be shown in the non-time-evolution plots 
        private LineSeries plotPsi = new LineSeries();      //|Ψ|²
        private LineSeries plotPsi0 = new LineSeries();     //|Ψ|² of the ground state
        private LineSeries plotPsiStart = new LineSeries(); //|Ψ|² in the beginning
        private LineSeries plotV = new LineSeries();        // the trap potential
        //Creation of the data series, that can be shown in the time-evolution plot
        private HeatMapSeries heatPsi = new HeatMapSeries();
        private HeatMapSeries ColorBarSeries = new HeatMapSeries();


        //declaration of the GPE solver
        private GPESolver gpe;


        /// <summary>
        /// Initializes the form. This includes the read out of the physical parameters and the
        /// preparation of the plot area.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //read out of parameters
            mass = Convert.ToDouble(massTextBox.Text) * PhysConst.amu;          // atom mass
            sclength = Convert.ToDouble(StreuTextBox.Text) * Math.Pow(10, -9);  // scattering length
            anzahl = Convert.ToInt32(AnzahlTextBox.Text);                       // number of atoms
            wx = Convert.ToDouble(FrequenzTextBox.Text) * 2 * Math.PI;          // trap frequency
            wr = Convert.ToDouble(RadFrequenzTextBox.Text) * 2 * Math.PI;       // radial trap frequency
            tsteps = Convert.ToInt32(TimeStepsTextBox.Text);                    // number of time steps

            // Initialization of the GPE solver, transferring the read-out parameters
            gpe = new GPESolver(mass, anzahl, sclength, wx, wr);                
            //Creation of the data series for the plot of the potential
            for (int k = 0; k < gpe.psi.Length; k++)
            {
                plotV.Points.Add(new DataPoint(gpe.X[k], gpe.V[k]));
            }
            potModel.Series.Add(plotV); //Adding the data series to the plot model for showing the potential

            //Generate Plots  noch fragen
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
        /// <summary>
        /// Starts the calculation of the time development of the wavefunction under given parameters.
        /// As starting wave function it is either used the ground state wave function, if calculated before
        /// or a default one, or another one, if the double BEC checkbox is selected.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            getParams(); //read-out of the parameters
            // Initialization of the GPE solver, transferring the read-out parameters
            gpe = new GPESolver(mass, anzahl, sclength, wx,wr);

            double[] normedPsi = new double[gpe.psi.Length];   //Creation of the array for |Ψ|²

            //Plot of the trap potential, that is shown during the calculation of the time-evolution calculation

            plotV.Points.Clear(); // Deletes previous data points from the data series

            //Creation of the new data series for the plot of the potential
            for (int k = 0; k < gpe.psi.Length; k++)
            {
                plotV.Points.Add(new DataPoint(gpe.X[k], gpe.V[k]));
            }

            potModel.Series.Clear();     // Deletes previous data series from the plotmodel
            potModel.Series.Add(plotV);  // Adds new data series to the plotmodel
            this.plot1.Model = potModel; // Shows the potential plot

            // Preparation of the time-dependent plot

            // Creation of the data grid for the time-dependent plot of |Ψ|²
            heatPsi.X0 = gpe.X[0];              // set xmin
            heatPsi.X1 = gpe.X[gpe.xSteps-1];   // set xmax
            heatPsi.Y0 = 0;                     
            heatPsi.Y1 = gpe.deltaT*tsteps;     //set the height of the plot

            heatPsi.Interpolate = true;         //switch on color gradient
            
            //Creation of two color scales, using the 'Jet-palette'
            LinearColorAxis cAxis = new LinearColorAxis();  
            cAxis.Palette = OxyPalettes.Jet(100);           
            LinearColorAxis cAxisC = new LinearColorAxis(); 
            cAxisC.Palette = OxyPalettes.Jet(100);          
            
            

            ColorBarModelE.Series.Clear();        
            this.ColorBar.Model = ColorBarModelE; //Prevents bug with colorbar if time evolution is selected before starting calculation. Reason unknown.



            timeModel.Axes.Add(cAxis); // Adding the color axis
            ColorBarModel.Axes.Add(cAxisC);
            double[,] dataMap = new double[gpe.psi.Length, tsteps / 100]; //Initializing data array for time-dependent plot
            double[,] ColorMap = new double[10, 10000];                   //Initializing data array for color bar

            

            Stopwatch Stopwatch1 = new Stopwatch(); // Initialisation of the stopwatch
            Stopwatch1.Start();                     // Start of the stopwatch

            //deciding which method to use for the time-development-calculation, 
            //if there is no method chosen, Bit-Reverse is used
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

            // The starting wave function is created, either using the default one (see GPESolver) or the ground state
            // is calculated, or the double BEC function is used with the offset chosen in the GUI
            ComplexNumber[] psiStart = (ComplexNumber[]) gpe.psi.Clone();

            if (getgroundstate.Checked)
            {
                gpe.getGroundState();
            }else if (DBECCheckBox.Checked)
            {
                offsetDBEC = Convert.ToInt32(OffsetDBECTextBox.Text);
                gpe.getDPsi(offsetDBEC);
            }


            int writeOut = 0;

            for (int i=0; i<tsteps; i++)
                {
                    // Calculation of Ψ(t) using the split-step-fourier method using the chosen algorithm for the FFT
                    gpe.splitStepFourier(method);
                    

                    // Writing every 100th calculated value into the plot array
                    if (i == writeOut)
                    {
                        for (int k = 0; k < gpe.psi.Length; k++)
                        {
                            dataMap[k,i / 100] = Math.Pow(gpe.psi[k].Norm(), 2);

                        }
                        writeOut += 100;
                    }
                }
           
            Stopwatch1.Stop();                                                       // Stops the time after calculation
            LaufzeitTextBox.Text = Convert.ToString(Stopwatch1.ElapsedMilliseconds); // Shows runtime in textbox
            listBox1.Items.Insert(0, method + "-FFT:" + " " + Convert.ToString(Stopwatch1.ElapsedMilliseconds) + "ms" + "  Timesteps" + tsteps.ToString()); // Adds runtime, used method and number of time steps to listbox

            maxColor = OxyPlot.ArrayExtensions.Max2D(dataMap); //@David ich weiß leider nicht so richtig was das macht
            for (int k = 0; k < 10000; k++)
            {
                for (int l = 0; l < 10; l++)
                {
                    ColorMap[l, k] = maxColor * k / 10000;
                }
            }
            // Creation of the data grid for the display of the colorbar 
            ColorBarSeries.X0 = 0; //set xmin
            ColorBarSeries.X1 = 10; //set xmax
            ColorBarSeries.Y0 = 0; //set height of the colorbar
            ColorBarSeries.Y1 = maxColor;
            ColorBarSeries.Interpolate = true; //switch on color gradient

            //Preparing Oxyplot 
            heatPsi.Data = dataMap;         // Write calculated data into plotarray
            ColorBarSeries.Data = ColorMap; // Write calculated data into plotarray
            timeModel.Series.Clear();
            timeModel.Series.Add(heatPsi);  // Add plotarray to plotmodel

            // Add the recent data series to the color bar model
            ColorBarModel.Series.Clear();
            ColorBarModel.Series.Add(ColorBarSeries);

            //Calculating |Ψ|² and |Ψstart|² and writing it into the plot arrays
            for (int k = 0; k < gpe.psi.Length; k++)
            {
                normedPsi[k] = Math.Pow(gpe.psi[k].Norm(), 2);
                plotPsi.Points.Add(new DataPoint(gpe.X[k], normedPsi[k]));
                normedPsi[k] = Math.Pow(psiStart[k].Norm(), 2);
                plotPsiStart.Points.Add(new DataPoint(gpe.X[k], normedPsi[k]));

            }

            // Deleting the old series and add the new ones
            myModel.Series.Clear();
            myModel.Series.Add(plotPsiStart);
            myModel.Series.Add(plotPsi); //

           
            this.plot1.Model = timeModel;        // Show the time-dependent plot
            this.ColorBar.Model = ColorBarModel; // Show the created color bar
            this.ColorBar.Visible = true;        // Make the color bar visible

            this.shiftPotButton.Enabled = true;  // The shift potential button is usable now

            //Energy = ETC.Hamilton(gpe.psi, gpe.V, gpe.deltaX, PhysConst.hbar, mass, gpe.g1D);
            //EnergieTextBox.Text = Convert.ToString(Energy); //Energie in TextBox 
        }

       
        /// <summary>
        /// Starts the calculation of the time development of the wavefunction under given parameters.
        /// As starting wave function it is either used the ground state wave function, if calculated before
        /// or a default one, or another one, if the double BEC checkbox is selected.
        /// </summary>
        private void ParameterButton_Click(object sender, EventArgs e)
        {
            //getParams();
            listBox1.Items.Clear();
        }

       
        /// <summary>
        /// Reads out the physical parameters that are written in the text boxes in the GUI and saves them
        /// as the corresponding variables.
        /// </summary>
        private void getParams()
        {
            mass = Convert.ToDouble(massTextBox.Text) * PhysConst.amu;
            sclength = Convert.ToDouble(StreuTextBox.Text) * Math.Pow(10, -9);
            anzahl = Convert.ToInt32(AnzahlTextBox.Text);
            wx = Convert.ToDouble(FrequenzTextBox.Text) * 2 * Math.PI;
            wr = Convert.ToDouble(RadFrequenzTextBox.Text) * 2 * Math.PI;
            tsteps = Convert.ToInt32(TimeStepsTextBox.Text);
        }

        /// <summary>
        /// Shows the plot of the time-evolution of |Ψ|².
        /// </summary>
        private void timeEvolutionButton_Click(object sender, EventArgs e)
        {
            this.plot1.Model = timeModel;
            this.ColorBar.Model = ColorBarModel;
            this.ColorBar.Visible = true;
        }

        /// <summary>
        /// Shows the plot of the start and end |Ψ|².
        /// </summary>
        private void psiPlotButton_Click(object sender, EventArgs e)
        {
            this.plot1.Model = myModel;
            this.ColorBar.Model = ColorBarModel;
            this.ColorBar.Visible = false;
        }

        /// <summary>
        /// Shows the plot of the trap potential.
        /// </summary>
        private void potentialButton_Click(object sender, EventArgs e)
        {
            this.plot1.Model = potModel;
            this.ColorBar.Model = ColorBarModel;
            this.ColorBar.Visible = false;
        }


        /// <summary>
        /// Shifts position of the potential in for 10µm in x direction in the beginning. Afterwards the
        /// time-evolution of the wave function is calculated, using the Bit Reversal algorithm.
        /// </summary>
        private void shiftPotButton_Click(object sender, EventArgs e)
        {
            gpe.changeCenterOfV(Math.Pow(10, -5) * 1);  // Shift of the potential
            plotV.Points.Clear();                       // Deleting the data points of the previous potential

            //Creates the data points for the new potential
            for (int k = 0; k < gpe.psi.Length; k++)
            {
                plotV.Points.Add(new DataPoint(gpe.X[k], gpe.V[k]));
            }

            potModel.Series.Clear();                    // deletes previous data series
            potModel.Series.Add(plotV);                 // adds new data series
            this.plot1.Model = potModel;                // shows new plot of the potential

            double[,] dataMap = new double[gpe.psi.Length, tsteps / 100];
            int writeOut = 0;
            for (int i = 0; i < tsteps; i++)
            {
                //Calculation of Ψ(t) using the split-step-fourier-method using the bit-reverse algorithm for the FFT
                gpe.splitStepFourier("BR");


                // Writing every 100th calculated value into the plot array
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

            heatPsi.Data = dataMap;         // Writes calculated data series into plot array
            timeModel.Series.Clear();       // clears plot model from previous data series
            timeModel.Series.Add(heatPsi);  // adds new data series

            this.plot1.Model = timeModel;   // shows new data series in plot

        }
        /// <summary>
        /// Enables writing in the offset textbox for the case, that the double BEC is chosen. It is 
        /// executed whenever the status of the checkbox is changed.
        /// </summary>
        private void DBECCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DBECCheckBox.Checked)
            {
                OffsetDBECTextBox.Enabled = true;
            }
            else
            {
                OffsetDBECTextBox.Enabled = false;
            }

        }

    }
}
