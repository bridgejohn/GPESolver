using System;
namespace OurMaths
{
    public class FFT
    {

        // calculates the discret Fourier transform of a complex array
        public static ComplexNumber[] DFT(ComplexNumber[] f)
        {
            int N = f.Length;
            ComplexNumber[] F = new ComplexNumber[N];
            for (int j = 0; j < N; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    F[j] += ComplexNumber.Exp(-ComplexNumber.ImaginaryOne * 2 * Math.PI * j * i / N) * f[i];
                }
            }
            return F;
        }

        // calculates the inverse discret Fourier transform of a complex array, 
        public static ComplexNumber[] IDFT(ComplexNumber[] f)
        {
            int N = f.Length;
            ComplexNumber[] F = new ComplexNumber[N];
            for (int j = 0; j < N; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    F[j] += ComplexNumber.Exp(ComplexNumber.ImaginaryOne * 2 * Math.PI * j * i / N) * f[i];
                }
            }
            for (int i = 0; i < N; i++) F[i] = F[i] / N;
            return F;
        }


        // calculates the Fourier transform via the Cooley-Tukey algorhitm
        public static ComplexNumber[] CT(ComplexNumber[] f)
        {
            int N = f.Length;
            ComplexNumber[] F = new ComplexNumber[N];
            if (N < 2)
            {
                F = f;
            }
            else
            {   // Creates to halfsize vectors for even and odd indices
                ComplexNumber[] even = new ComplexNumber[N / 2];
                ComplexNumber[] odd = new ComplexNumber[N / 2];

                for (int i = 0; i < N / 2; i++)
                {
                    even[i] = f[2 * i];
                    odd[i] = f[2 * i + 1];
                }
                // Vectors for FT of even/odd vectors
                ComplexNumber[] EVEN = new ComplexNumber[N / 2];
                ComplexNumber[] ODD = new ComplexNumber[N / 2];

                // FFT even and odd vectors
                EVEN = CT(even);
                ODD = CT(odd);

                for (int i = 0; i < N / 2; i++)
                {
                    F[i] = EVEN[i] + ComplexNumber.Exp(-ComplexNumber.ImaginaryOne * 2 * Math.PI / N * i) * ODD[i];
                    F[i + N / 2] = EVEN[i] - ComplexNumber.Exp(-ComplexNumber.ImaginaryOne * 2 * Math.PI / N * i) * ODD[i];
                }
            }

            return F;
        }

        // inverts the Fourier transform of the Cooley-Tukey algorhitm via conjugating before and after the transform
        public static ComplexNumber[] ICT(ComplexNumber[] f)
        {
            //f = doICT(f);
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = f[i].Conjugate();
            }

            f = FFT.CT(f);


            for (int i = 0; i < f.Length; i++)
            {
                f[i] = f[i].Conjugate();
                f[i] = f[i] / f.Length;
            }
            return f;
        }

       
        // basics transform alg for a bit reversed fourier, divers between fft and ifft via the twidlefactor
        public static ComplexNumber[] Transform(bool negativeTwidleFactor, ComplexNumber[] f, uint[] reversedBits)
        {
            ComplexNumber[] workF = (ComplexNumber[]) f.Clone();
            int size = f.Length;
            if (size != reversedBits.Length) throw new Exception("The arrays dont have the same lenght");
            int a = 0;
            int findA = size;
            while (findA > 1)
            {
                findA = findA >> 1;
                a++;
            }

            int preFactor;
            if (negativeTwidleFactor) { preFactor = -1; } else { preFactor = 1; }
            ComplexNumber even, odd, wM, w;
            int shift, m;

            //bitReverse  
            for (int i = 0; i < f.Length; i++)
            {
                workF[i] = f[reversedBits[i]];
            }

            for (int s = 1; s <= a; s++)
            {
                shift = 1 << (s - 1); // = 2^(s-1) via bitshift
                m = 1 << s; //=2^s
                wM = ComplexNumber.Exp(preFactor * ComplexNumber.ImaginaryOne * 2 * Math.PI / m);

                for (int i = 0; i < size; i += m)
                {
                    w = 1;
                    for (int j = 0; j < shift; j++)
                    {

                        // the calll to the reverse bit structure accounts for the bitflipped order
                        even = workF[i + j];
                        odd = w * workF[i + j + shift];

                        // joins together the desired even and odd parts, with the twidle factor multiplied to the odd part
                        workF[i + j] = even + odd;
                        workF[i + j + shift] = even - odd;

                        // increase phase of twiddle factor
                        w = w * wM;

                    }
                }
            }
            return workF;
        }

        public static ComplexNumber[] BR (ComplexNumber[] f, uint[] reversedBits)
        {
            return Transform(true,f, reversedBits);
        }

        public static ComplexNumber[] IBR(ComplexNumber[] f, uint[] reversedBits)
        {
            f = Transform(false, f, reversedBits);
            for (int i = 0; i < f.Length; i++) f[i] = f[i] / f.Length;
            return f;
        }


        // shift the sides
        public static ComplexNumber[] Shift(ComplexNumber[] psi)
        {
            ComplexNumber[] psi_firsthalf = new ComplexNumber[psi.Length / 2];
            for (int i = 0; i < psi_firsthalf.Length; i++) psi_firsthalf[i] = psi[i];
            for (int i = 0; i < psi_firsthalf.Length; i++) psi[i] = psi[i + psi.Length / 2];
            for (int i = 0; i < psi_firsthalf.Length; i++) psi[i + psi_firsthalf.Length] = psi_firsthalf[i];
            return psi;
        }

        // reverses the bit order of a given length
        public static uint BitReverse(uint B, int bitLength)
        {
            uint x = 0;
            for (int i = 0; i < bitLength / 2; i++)
            {
                int j = bitLength - 1 - i;
                uint bit1 = (B >> j) & 1;
                uint bit2 = (B >> i) & 1;
                x = x | (bit1 ^ bit2) << j | (bit1 ^ bit2) << i;
            }
            return B ^ x;
        }

    }
}
