using System;
namespace OurMaths
{
    /// <summary>
    /// The class FFT provides all the necassary methods to perform a one dimensional Fourier transformation
    /// of an array, which needs to be of the power of two. The arrays is filled with complex numbers of the class <c>ComplexNumber</c>.
    /// </summary>
    /// <remarks>
    /// There are three main methods to perform the Fourier transformation:
    /// - the standard discrecte Forurier transformation (DFT)
    /// - a fast Fourier transformation which is is processed via the Cooley-Tukey algorithm (CT)
    /// - a FFT by using the Bit reversal structure encountered at the second last stage of the CT version
    /// as well as there inverse transformation.
    /// </remarks>
    public class FFT
    {

        /// <summary>
        /// The DFT calculates the discret Fourier transform of a complex array
        /// </summary>
        /// <returns>An array which is Fourier transformed</returns>
        /// <param name="f">Array to be Fourier transformed</param>
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

        /// <summary>
        /// The IDFT calculates the inverse discret Fourier transform of a complex array
        /// </summary>
        /// <returns>An array which is inverse Fourier transformed</returns>
        /// <param name="f">Array to be inverse Fourier transformed</param> 
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


        /// <summary>
        /// CT performs a FFT using the Cooley-Tukey algorithm, which requires an array of the power of two.
        /// The algorithm is recursively.
        /// </summary>
        /// <returns>Fourier transformed array</returns>
        /// <param name="f">Array to be Fourier transformed</param>
        public static ComplexNumber[] CT(ComplexNumber[] f)
        {
            int N = f.Length;
            ComplexNumber[] F = new ComplexNumber[N];

           
            if (N < 2)
            {
                F = f;   // breaks the recursion at its smallest array size of 1
            }
            else
            {   
                // Creates to halfsize arrays for even and odd indices
                ComplexNumber[] even = new ComplexNumber[N / 2];
                ComplexNumber[] odd = new ComplexNumber[N / 2];

                for (int i = 0; i < N / 2; i++)
                {
                    even[i] = f[2 * i];
                    odd[i] = f[2 * i + 1];
                }

                // prepares arrays, in which the Fouriertrans formed version will be saved
                ComplexNumber[] EVEN = new ComplexNumber[N / 2];
                ComplexNumber[] ODD = new ComplexNumber[N / 2];

                // Fourier transforms the smaller arrays by calling the function itself
                EVEN = CT(even);
                ODD = CT(odd);

                // reassambles the values in the correct order
                for (int i = 0; i < N / 2; i++)
                {
                    F[i] = EVEN[i] + ComplexNumber.Exp(-ComplexNumber.ImaginaryOne * 2 * Math.PI / N * i) * ODD[i];
                    F[i + N / 2] = EVEN[i] - ComplexNumber.Exp(-ComplexNumber.ImaginaryOne * 2 * Math.PI / N * i) * ODD[i];
                }
            }

            // returns the Fourier transformed array
            return F;
        }

        // inverts the Fourier transform of the Cooley-Tukey algorhitm via conjugating before and after the transform
        /// <summary>
        /// Calculates the inverse Fourier transformation using the CT algorithm.
        /// The inversion is accounted by complex conjugation of the whole array before and after the transformation with the CT algorithm.
        /// </summary>
        /// <returns>Inverse Fourier transformed array</returns>
        /// <param name="f">Array to be inverse Fourier transformed</param>
        public static ComplexNumber[] ICT(ComplexNumber[] f)
        {
            // Complex conjugation of the whole array
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = f[i].Conjugate();
            }

            f = FFT.CT(f);

            // Complex conjugation of the whole array again and resizing the parameters 
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = f[i].Conjugate();
                f[i] = f[i] / f.Length;
            }
            return f;
        }

       
        /// <summary>
        /// Transformation algorithm to perform a FFT using the Bit reversal structure.
        /// </summary>
        /// <remarks>
        /// The FT is performed by setting the <c>negativeTwidleFactor</c> to <c>true</c>, its inverse by setting it to <c>false</c>.
        /// </remarks>
        /// <returns>Transformed array</returns>
        /// <param name="negativeTwidleFactor">If set to <c>true</c> FT is performed, if <c>false</c> its inverse.</param>
        /// <param name="f">Array to be transformed</param>
        /// <param name="reversedBits">Reversed bit array, needs to be precalculated by <c>BitReverse</c></param>
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

            // sets the prefactor for the twiddle factor to perform either FFT or IFFT
            int preFactor;
            if (negativeTwidleFactor) { preFactor = -1; } else { preFactor = 1; }

            // prepare necassary variables
            ComplexNumber even, odd, wM, w;
            int shift, m;

            //orders the array according to the bit reversal   
            for (int i = 0; i < f.Length; i++)
            {
                workF[i] = f[reversedBits[i]]; 
            }

            // three for loops to process the full FFT/IFFT
            // the result of the loops gives the FFT/IFFT in the normal ordered form
            for (int s = 1; s <= a; s++)
            {
                shift = 1 << (s - 1);   // = 2^(s-1) via bitshift
                m = 1 << s;             // = 2^s
                wM = ComplexNumber.Exp(preFactor * ComplexNumber.ImaginaryOne * 2 * Math.PI / m); // twiddle factor, with prefactor for FFT/IFFT

                for (int i = 0; i < size; i += m)
                {
                    w = 1;
                    for (int j = 0; j < shift; j++)
                    {

                        // the call to the reverse bit structure accounts for the bitflipped order
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

        /// <summary>
        /// Fourier transformation using Bit Reversal
        /// </summary>
        /// <returns>Fourier transformed array</returns>
        /// <param name="f">Array to be Fourier transformed</param>
        /// <param name="reversedBits">Reversed bit array, needs to be precalculated by <c>BitReverse</c></param>
        public static ComplexNumber[] BR (ComplexNumber[] f, uint[] reversedBits)
        {
            return Transform(true,f, reversedBits);
        }

        /// <summary>
        /// Inverse Fourier transformation using Bit Reversal
        /// </summary>
        /// <returns>Inverse Fourier transformed array</returns>
        /// <param name="f">Array to be inverse Fourier transformed</param>
        /// <param name="reversedBits">Reversed bit array, needs to be precalculated by <c>BitReverse</c></param>
        public static ComplexNumber[] IBR(ComplexNumber[] f, uint[] reversedBits)
        {
            f = Transform(false, f, reversedBits);
            for (int i = 0; i < f.Length; i++) f[i] = f[i] / f.Length; // resize the values
            return f;
        }


        /// <summary>
        /// The output of any FFT is symmetric but the left half is computed to the right of the right half,
        /// so it is desirable to shift those to parts, which is done by this method
        /// </summary>
        /// <returns>Array, with the two halfs shifted</returns>
        /// <param name="psi">Array</param>
        public static ComplexNumber[] Shift(ComplexNumber[] psi)
        {
            ComplexNumber[] psi_firsthalf = new ComplexNumber[psi.Length / 2];                  // half size array buffer
            for (int i = 0; i < psi_firsthalf.Length; i++) psi_firsthalf[i] = psi[i];           // write the lower half in to the buffer
            for (int i = 0; i < psi_firsthalf.Length; i++) psi[i] = psi[i + psi.Length / 2];    // copy th upper half to the lower hlaf of the array
            for (int i = 0; i < psi_firsthalf.Length; i++) psi[i + psi_firsthalf.Length] = psi_firsthalf[i];   // overwrite th upper half with the content of the buffer
            return psi;
        }

        /// <summary>
        /// Mehtod to create a bit reversed version of an unsigned integer which can have a smaller length than the 32 bits.
        /// 
        /// </summary>
        /// <returns>The bit reversed version.</returns>
        /// <param name="B">input bit as an unsigned integer </param>
        /// <param name="bitLength">Formal length of the number in bits. Maximum 32 bits.</param>
        public static uint BitReverse(uint B, int bitLength)
        {
            // prepare a clear bit array
            uint x = 0;
            // shifts the bit (i) and (n-i)
            for (int i = 0; i < bitLength / 2; i++) // i: position of the second bit (bit2)
            {
                int j = bitLength - 1 - i;  // j: position of the first bit (bit1)
                uint bit1 = (B >> j) & 1;   // shift bit from position j to posistion 1 and delete all other bits by &1
                uint bit2 = (B >> i) & 1;   // do the same for the second bit
                x = x | (bit1 ^ bit2) << j | (bit1 ^ bit2) << i; //XOR the two bits and shift the rsult back to the two positions
            }
            return B ^ x; // XOR the input with the process variable which gives then the bit fliped result of the input bit
        }

    }
}
