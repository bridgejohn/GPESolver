using System;
using System.Numerics;
using OurMaths;


namespace OurMaths
{
    public class GPESolver
    {
        public double deltaX;
        public int xSteps;
        public double deltaK;
        public int kSteps;
        public double[] X, V, K;

        public double deltaT;
        public int timeSteps;

        public ComplexNumber[] psi;
        public double mass;        // mass of rubidium
        public double aSc;         // Scattering Length 
        public int N;              // Atom number
        public double wX;          // trap freq
        public double lX;          // ho length of trap
        public double wR;          // radial trap freq
        public double lR;          // ho length of trap

        public double g1D;

        public uint[] reversedBits;


        public GPESolver()
        {
            // Physical Defintion
            mass = 87 * PhysConst.amu;       // mass of rubidium
            aSc = 5.8 * Math.Pow(10, -9);    // Scattering Length 
            N = 1000;                        // Atom number
            wX = 40 * 2 * Math.PI;           // trap freq
            lX = Math.Sqrt(PhysConst.hbar / (mass * wX)); //ho length of trap
            wR = 100 * 2 * Math.PI;          // radial trap freq
            lR = Math.Sqrt(PhysConst.hbar / (mass * wR)); // ho length of trap

            g1D = 2 * Math.Pow(PhysConst.hbar, 2) * aSc / (mass * lR * lR); // 1D interaction coefficient


            /// Grid definitions
            xSteps = 512;  // number of points im Ortsraum; power of two
            kSteps = xSteps;     // number of points in momentum space
            deltaX = 2 * Math.Pow(10, -7);      // distance between points im Ortsraum
            deltaK = 2 * Math.PI / ((xSteps - 1) * deltaX);   //distance between points in momentum space
            deltaT = Math.Pow(10, -8);       // time intervall
            timeSteps = 100;   // Anzahl der Zeitentwicklungsschritte

            // create Grids
            X = new double[xSteps];
            for (int i = 0; i < xSteps; i++) { X[i] = (i - xSteps / 2 + 1) * deltaX; }
            V = new double[xSteps];
            for (int i = 0; i < xSteps; i++) { V[i] = 0.5 * mass * wX * wX * X[i] * X[i] / PhysConst.hbar; }
            K = new double[xSteps];
            for (int i = 0; i < xSteps; i++) { K[i] = (i - xSteps / 2 + 1) * deltaK; }

            // prepare for bitReversal
            reversedBits = new uint[xSteps];
            for (uint i = 0; i < xSteps; i++) reversedBits[i] = FFT.BitReverse(i, (int) Math.Log(xSteps, 2));

            // create Starting wave function  
            // psi_0=sqrt(N/lx)*(1/pi)ˆ(1/4)*exp(-x.ˆ2/(2*lxˆ2));
            psi = new ComplexNumber[xSteps];
            for (int i = 0; i < xSteps; i++)
            {
                psi[i] = Math.Sqrt(N / lX) * Math.Pow(Math.PI, -0.25) * Math.Exp(-X[i] * X[i] / (2.2 * lX * lX));
            }

        }

        public GPESolver(double atomMass, int numberOfAtoms, double scatteringlength, double wx, double wr)
        {
            // Physical Defintion
            mass = atomMass;       // mass of rubidium
            aSc = scatteringlength;    // Scattering Length 
            N = numberOfAtoms;                        // Atom number
            wX = wx;           // trap freq
            lX = Math.Sqrt(PhysConst.hbar / (mass * wX)); //ho length of trap
            wR = wr;          // radial trap freq
            lR = Math.Sqrt(PhysConst.hbar / (mass * wR)); // ho length of trap

            g1D = 2 * Math.Pow(PhysConst.hbar, 2) * aSc / (mass * lR * lR); // 1D interaction coefficient


            /// Grid definitions
            xSteps = 512;  // number of points im Ortsraum; power of two
            kSteps = xSteps;     // number of points in momentum space
            deltaX = 2 * Math.Pow(10, -7);      // distance between points im Ortsraum
            deltaK = 2 * Math.PI / ((xSteps - 1) * deltaX);   //distance between points in momentum space
            deltaT = Math.Pow(10, -7);       // time intervall
            timeSteps = 10000;   // Anzahl der Zeitentwicklungsschritte

            // create Grids
            X = new double[xSteps];
            for (int i = 0; i < xSteps; i++) { X[i] = (i - xSteps / 2 + 1) * deltaX; }
            V = new double[xSteps];
            for (int i = 0; i < xSteps; i++) { V[i] = 0.5 * mass * wX * wX * X[i] * X[i] / PhysConst.hbar; }
            K = new double[xSteps];
            for (int i = 0; i < xSteps; i++) { K[i] = (i - xSteps / 2 + 1) * deltaK; }

            // prepare for bitReversal
            reversedBits = new uint[xSteps];
            int a = 0;
            int findA = xSteps;
            while (findA > 1)
            {
                findA = findA >> 1;
                a++;
            }
            for (uint i = 0; i < xSteps; i++) reversedBits[i] = FFT.BitReverse(i, a);


            // create Starting wave function  
            // psi_0=sqrt(N/lx)*(1/pi)ˆ(1/4)*exp(-x.ˆ2/(2*lxˆ2));
            psi = new ComplexNumber[xSteps];
            for (int i = 0; i < xSteps; i++)
            {
                psi[i] = Math.Sqrt(N / lX) * Math.Pow(Math.PI, -0.25) * Math.Exp(-X[i] * X[i] / (2.2 * lX * lX));
            }

        }



        public void splitStepFourier(string FT)
        {
            
            int size = this.psi.Length;


            // psi=psi.*exp(-0.5*1i*dt*(V+(g1d/hbar)*abs(psi).ˆ2));
            for (int i = 0; i < size; i++)
            {
                psi[i] = psi[i] * ComplexNumber.Exp(-0.5 * ComplexNumber.ImaginaryOne * deltaT
                                                    * (V[i] + g1D / PhysConst.hbar
                                                    * Math.Pow(psi[i].Norm(), 2)));
            }

            switch (FT)
            {
                case "DFT":
                    psi = FFT.DFT(psi);
                    break;
                case "CT":
                    psi = FFT.CT(psi);
                    break;
                case "BR":
                    psi = FFT.BR(psi,reversedBits);
                    break;
                default:
                    break;
            }

            psi = FFT.Shift(psi);
            for (int i = 0; i < size; i++) psi[i] = psi[i] / size;


            // psi_k=psi_k*exp(-0.5*dt*1i*(hbar/m)*kˆ2)
            for (int i = 0; i < size; i++)
            {
                psi[i] = psi[i] * ComplexNumber.Exp(-0.5 * ComplexNumber.ImaginaryOne * deltaT
                                                    * PhysConst.hbar / mass * Math.Pow(K[i], 2));
            }


            psi = FFT.Shift(psi);
            switch (FT)
            {
                case "DFT":
                    psi = FFT.IDFT(psi);
                    break;
                case "CT":
                    psi = FFT.ICT(psi);
                    break;
                case "BR":
                    psi = FFT.IBR(psi, reversedBits);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < size; i++)
            {
                psi[i] = psi[i] * size;
            }

            //psi = psi.* exp(-0.5 * 1i * dt * (V + (g1d / hbar) * abs(psi).ˆ2));
            for (int i = 0; i < size; i++)
            {
                psi[i] = psi[i] * ComplexNumber.Exp(-0.5 * ComplexNumber.ImaginaryOne * deltaT
                                                    * (V[i] + g1D / PhysConst.hbar
                                                    * Math.Pow(psi[i].Norm(), 2)));
            }




        }



        public void getGroundState()
        {
            //ComplexNumber[] psi_0 = psi;
            double mu = 1;
            double mu_old;
            double mu_error = 1;
            double[] NormOfPsi = new double[psi.Length];
            for (int i = 0; i < psi.Length; i++)
            {
                NormOfPsi[i] = deltaX * Math.Pow(psi[i].Norm(), 2);
            }
            int j = 0;
            while (mu_error > Math.Pow(10, -8))
            {
                // creation of new wave function
                ComplexNumber[] psi_old = psi;
                for (int i = 0; i < psi.Length; i++)
                {
                    psi[i] = psi[i]*Math.Exp(-0.5 * deltaT * (V[i] + g1D / PhysConst.hbar * Math.Pow(psi[i].Norm(), 2)));
                }

                ComplexNumber[] psi_k = new ComplexNumber[psi.Length];
                psi = FFT.CT(psi); // Fourier transformation of the wave function with the Cooley-Tukey algorithm
                psi = FFT.Shift(psi);  //
                for (int i = 0; i < psi.Length; i++) psi[i] = psi[i] / xSteps;


                // psi = psi*exp((-0.5*deltaT*hbar*|k|^2)/m)
                for (int i = 0; i < psi.Length; i++)
                {
                    psi[i] = psi[i]*Math.Exp(-0.5 * deltaT * (PhysConst.hbar / this.mass * Math.Pow(psi[i].Norm(), 2)));
                }
                psi = FFT.Shift(psi);
                psi = FFT.ICT(psi); //Inverse fourier transformation of the wave function with the Cooley-tukey algorithm

                for (int i = 0; i < psi.Length; i++) psi[i] = psi[i] * kSteps;


                // psi = psi*exp(-0.5*deltaT*V(x)+g1D/hbar * |psi|^2)
                for (int i = 0; i < psi.Length; i++)
                {
                    psi[i] = psi[i]*Math.Exp(-0.5 * deltaT * (V[i] + g1D / PhysConst.hbar * Math.Pow(psi[i].Norm(), 2)));
                }

                mu_old = mu;
                mu = Math.Log((psi_old[psi_old.Length / 2] / psi[psi.Length / 2]).Norm()) / deltaT;
                mu_error = Math.Abs(mu - mu_old) / mu;

                for (int i = 0; i < psi.Length; i++)
                {
                    psi[i] = psi[i] * Math.Sqrt(NormOfPsi[i]) / Math.Sqrt((deltaX * Math.Pow(psi[i].Norm(), 2)));
                }
                if (j > Math.Pow(10, 8))
                {
                    break;
                }

            }

        }

        // psi=psi.*exp(-0.5*1i*dt*(V+(g1d/hbar)*abs(psi).ˆ2));
        public void addPhaseSpatial()
        {
            for (int i = 0; i < psi.Length; i++)
            {
                psi[i] = psi[i] * ComplexNumber.Exp(-0.5 * ComplexNumber.ImaginaryOne * deltaT * (V[i] + g1D / PhysConst.hbar * Math.Pow(psi[i].Norm(), 2)));
            }
        }

        // psi_k=psi_k*exp(-0.5*dt*1i*(hbar/m)*kˆ2)
        public void addPhaseMomentum()
        {
            for (int i = 0; i < psi.Length; i++)
            {
                psi[i] = psi[i] * ComplexNumber.Exp(-0.5 * ComplexNumber.ImaginaryOne * deltaT * PhysConst.hbar / mass * Math.Pow(K[i], 2));
            }
        }
    }
}
