using System;
namespace OurMaths
{
    /// <summary>
    /// The class ComplexNumbers provides a complex data type and defines the basic calculation operations for them.
    /// </summary>
    /// <remarks>
    /// A ComplexNumber consists of two double values representing the real and the imaginary part.
    /// </remarks>
    public struct ComplexNumber
    {
        double rr, ii;

        /// <summary>
        /// Defines the number i as a complex public constant.
        /// </summary>
        public static ComplexNumber ImaginaryOne = new ComplexNumber(0, 1);

        /// <summary>
        /// Defines the number 1 as a complex public constant.
        /// </summary>
        public static ComplexNumber One = new ComplexNumber(1, 0);

        /// <summary>
        /// Defines the number 0 as a complex public constant.
        /// </summary>
        public static ComplexNumber Zero = new ComplexNumber(0, 0);

        /// <summary>
        /// Defines the ComplexNumber.
        /// </summary>
        /// <param name="realPart">Double value to be the real part.</param>
        /// <param name="imaginaryPart">Double value to be the imaginary part.</param>
        public ComplexNumber(double realPart, double imaginaryPart)
        {
            rr = realPart;
            ii = imaginaryPart;
        }

        /// <returns>
        /// The imaginary part of the current complex number.
        /// </returns>
        public double imaginaryPart() => this.ii;

        /// <returns>
        /// The real part of the current complex number.
        /// </returns>
        public double realPart() => this.rr;

        /// <returns>
        /// The sum of the current and another complex number.
        /// </returns>
        /// <param name="c">Complex number to be added.</param>
        public ComplexNumber Add(ComplexNumber c) => new ComplexNumber(this.rr + c.rr, this.ii + c.ii);

        /// <returns>
        /// The difference of the current and another complex number.
        /// </returns>
        /// <param name="c">Complex number to be substracted.</param>
        public ComplexNumber Substract(ComplexNumber c) => new ComplexNumber(this.rr - c.rr, this.ii - c.ii);

        /// <returns>
        /// The product of the current and another complex number.
        /// </returns>
        /// <param name="c">Complex number to be multiplicated.</param>
        public ComplexNumber Multiply(ComplexNumber c) => new ComplexNumber(this.rr * c.rr - this.ii * c.ii, this.rr * c.ii + this.ii * c.rr);

        /// <returns>
        /// The quotient of the current and another complex number.
        /// </returns>
        /// <param name="c">Complex number to be devided by.</param>
        public ComplexNumber Divide(ComplexNumber c) => new ComplexNumber((this.rr * c.rr + this.ii * c.ii) / (c.rr * c.rr + c.ii * c.ii), (this.ii * c.rr - this.rr * c.ii) / (c.rr * c.rr + c.ii * c.ii));

        /// <returns>
        /// The norm of the current complex number.
        /// </returns>
        public double Norm() => Math.Sqrt(this.rr * this.rr + this.ii * this.ii);

        /// <returns>
        /// The absolute of the current complex number.
        /// </returns>
        public double Abs() => Math.Sqrt(this.rr * this.rr + this.ii * this.ii);

        /// <returns>
        /// The complex argument of the current complex number.
        /// </returns>
        public double Arg() => Math.Atan2(this.ii, this.rr);

        /// <returns>
        /// The complex conjugated of the current complex number.
        /// </returns>
        public ComplexNumber Conjugate() => new ComplexNumber(this.rr, -this.ii);


        /// <summary>
        /// Defines the + operator for complex numbers.
        /// </summary>
        /// <param name="c1">First summand.</param>
        /// <param name="c2">Second summand.</param>
        /// <returns>The sum of the parameter values.</returns>
        public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2) => new ComplexNumber(c1.rr + c2.rr, c1.ii + c2.ii);

        /// <summary>
        /// Defines the - operator for complex numbers.
        /// </summary>
        /// <param name="c1">Minuend.</param>
        /// <param name="c2">SUbtrahend.</param>
        /// <returns>The difference of the parameter values.</returns>
        public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2) => new ComplexNumber(c1.rr - c2.rr, c1.ii - c2.ii);

        /// <summary>
        /// Defines the - operator for only one complex number.
        /// </summary>
        /// <param name="c">Value to be negated.</param>
        /// <returns>The negation of the parameter value.</returns>
        public static ComplexNumber operator -(ComplexNumber c) => new ComplexNumber(-c.rr, -c.ii);

        /// <summary>
        /// Defines the * operator for complex numbers.
        /// </summary>
        /// <param name="c1">First factor.</param>
        /// <param name="c2">Second factor.</param>
        /// <returns>The product of the parameter values.</returns>
        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2) => new ComplexNumber(c1.rr * c2.rr - c1.ii * c2.ii, c1.rr * c2.ii + c1.ii * c2.rr);

        /// <summary>
        /// Defines the / operator for complex numbers.
        /// </summary>
        /// <param name="c1">Dividend.</param>
        /// <param name="c2">Divisor.</param>
        /// <returns>The quotient of the parameter values.</returns>
        public static ComplexNumber operator / (ComplexNumber c1, ComplexNumber c2) => new ComplexNumber((c1.rr * c2.rr + c1.ii * c2.ii) / (c2.rr * c2.rr + c2.ii * c2.ii),(c1.ii * c2.rr - c1.rr * c2.ii) / (c2.rr * c2.rr + c2.ii * c2.ii));
       
        // Implicit type conversions
        public static implicit operator ComplexNumber(double d) => new ComplexNumber(d, 0);
        public static implicit operator ComplexNumber(int i) => new ComplexNumber(Convert.ToDouble(i), 0);



        /// <returns></returns>
        /// <param name="c">Complex number whose real part is desired.</param>
        public static double realPart(ComplexNumber c) => c.realPart();

        /// <returns></returns>
        /// <param name="c">Complex number whose imaginary part is desired.</param>
        public static double imaginaryPart(ComplexNumber c) => c.imaginaryPart();

        /// <summary>
        /// Computes the exponantial of a complex number.
        /// </summary>
        /// <param name="c">Complex exponent.</param>
        /// <returns>e to the power of the complex parameter.</returns>
        public static ComplexNumber Exp(ComplexNumber c) => Math.Exp(c.rr)*(new ComplexNumber(Math.Cos(c.ii), Math.Sin(c.ii)));



    }
}
