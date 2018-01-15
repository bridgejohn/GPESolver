using System;
namespace OurMaths
{
    public struct ComplexNumber
    {
        double rr, ii;
        public static ComplexNumber ImaginaryOne = new ComplexNumber(0, 1);
        public static ComplexNumber One = new ComplexNumber(1, 0);
        public static ComplexNumber Zero = new ComplexNumber(0, 0);

        public ComplexNumber(double realPart, double imaginaryPart)
        {
            rr = realPart;
            ii = imaginaryPart;
        }

        // Method on Instances

        //imaginary part
        public double imaginaryPart() => this.ii;

        //imaginary part
        public double realPart() => this.rr;

        // adds a complex number
        public ComplexNumber Add(ComplexNumber c) => new ComplexNumber(this.rr + c.rr, this.ii + c.ii);

        // substracts a complex number from the current one
        public ComplexNumber Substract(ComplexNumber c) => new ComplexNumber(this.rr - c.rr, this.ii - c.ii);
        
        // multiplies a complex number to the current one
        public ComplexNumber Multiply(ComplexNumber c) => new ComplexNumber(this.rr * c.rr - this.ii * c.ii, this.rr * c.ii + this.ii * c.rr);

        // divides this complex number through another one
        public ComplexNumber Divide(ComplexNumber c) => new ComplexNumber((this.rr * c.rr + this.ii * c.ii) / (c.rr * c.rr + c.ii * c.ii), (this.ii * c.rr - this.rr * c.ii) / (c.rr * c.rr + c.ii * c.ii));
       
        // retruns the norm/absolute value
        public double Norm() => Math.Sqrt(this.rr * this.rr + this.ii * this.ii);
        public double Abs() => Math.Sqrt(this.rr * this.rr + this.ii * this.ii);

        // retuns the complex argument
        public double Arg() => Math.Atan2(this.ii, this.rr);

        // returns the complex conjugate
        public ComplexNumber Conjugate() => new ComplexNumber(this.rr, -this.ii);


        // opereators

        // Add two complex numbers
        public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2) => new ComplexNumber(c1.rr + c2.rr, c1.ii + c2.ii);

        // Subtract two complex numbers
        public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2) => new ComplexNumber(c1.rr - c2.rr, c1.ii - c2.ii);

        // negate a complex number
        public static ComplexNumber operator -(ComplexNumber c) => new ComplexNumber(-c.rr, -c.ii);

        // multiply two complex numbers
        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2) => new ComplexNumber(c1.rr * c2.rr - c1.ii * c2.ii, c1.rr * c2.ii + c1.ii * c2.rr);
       
        // divide two complex numbers
        public static ComplexNumber operator / (ComplexNumber c1, ComplexNumber c2) => new ComplexNumber((c1.rr * c2.rr + c1.ii * c2.ii) / (c2.rr * c2.rr + c2.ii * c2.ii),(c1.ii * c2.rr - c1.rr * c2.ii) / (c2.rr * c2.rr + c2.ii * c2.ii));
       
        // Implicit type conversions
        public static implicit operator ComplexNumber(double d) => new ComplexNumber(d, 0);
        public static implicit operator ComplexNumber(int i) => new ComplexNumber(Convert.ToDouble(i), 0);






        //real part of a complex Number
        public static double realPart(ComplexNumber c) => c.realPart();

        //imaginary part of a Complex Number
        public static double imaginaryPart(ComplexNumber c) => c.imaginaryPart();

        //exponential of c, defined by exp(c) = exp(Re(c))*(cos(Im(c)) + i*sin(Im(c)))
        public static ComplexNumber Exp(ComplexNumber c) => Math.Exp(c.rr)*(new ComplexNumber(Math.Cos(c.ii), Math.Sin(c.ii)));



    }
}
