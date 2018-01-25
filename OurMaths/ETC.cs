using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMaths
{
    public class ETC
    {

        public static ComplexNumber[] Derivate(ComplexNumber[] F, double dx)
        {
            // ComplexNumber[] F : Abzuleitender  komplexer 1D-Array
            // dx : Schrittweite zwischen benachbarten Werten in ComplexNumber[] F

            int n = F.Length - 1;
            ComplexNumber[] f = new ComplexNumber[n];

            for (int i = 0; i < n; i++)
            {
                f[i] = (F[i + 1] - F[i]) / dx; //Steigung zwischen benachbarten Werten
                Console.WriteLine("Fi1: " + F[i + 1].ToString() + " , Fi: " + F[i].ToString() + " , fi " + f[i].ToString() + " , dx " + dx.ToString() + " , test " + (F[i + 1] - F[i]).ToString());
            }
            return f;


        }

        public static Double Hamilton(ComplexNumber[] F, double[] V, double dx, double hbar, double m, double g1D)
        {
            double T = 0;
            double Pot = 0;
            double E = 0;
            ComplexNumber[] derivate = Derivate(F, dx);
            for (int i = 0; i<derivate.Length; i++)
            {
                T += derivate[i].Norm() * derivate[i].Norm();
            }
            T = T * hbar * hbar / (2 * m);

            for (int i = 0; i < F.Length; i++)
            {
                Pot += V[i] * F[i].Norm() * F[i].Norm() + F[i].Norm() * F[i].Norm() * F[i].Norm() * F[i].Norm() * g1D / 2;
            }

            E = T + Pot;
            return E;
        }
    }
}
