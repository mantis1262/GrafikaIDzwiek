using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Model
{
    public class Complex
    {
        private double _real;
        private double _imaginary;

        public Complex(double real, double imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public double Real { get => _real; set => _real = value; }
        public double Imaginary { get => _imaginary; set => _imaginary = value; }

        public double Abs()
        {
            return Math.Sqrt(_real * _real + _imaginary * _imaginary);
        }

        public double Phase()
        {
            return Math.Atan2(_imaginary, _real);
        }

        public Complex Plus(Complex second)
        {
            Complex first = this;
            double realPart = first._real + second._real;
            double imaginaryPart = first._imaginary + second._imaginary;
            return new Complex(realPart, imaginaryPart);
        }

        public Complex Minus(Complex second)
        {
            Complex first = this;
            double realPart = first._real - second._real;
            double imaginaryPart = first._imaginary - second._imaginary;
            return new Complex(realPart, imaginaryPart);
        }

        public Complex Times(Complex second)
        {
            Complex first = this;
            double realPart = first._real * second._real - first._imaginary * second._imaginary;
            double imaginaryPart = first._real * second._imaginary + first._imaginary * second._real;
            return new Complex(realPart, imaginaryPart);
        }

        public Complex Times(double alpha)
        {
            return new Complex(alpha * _real, alpha * _imaginary);
        }

        public Complex Conjugate()
        {
            return new Complex(_real, -_imaginary);
        }

        public static Complex FromPolar(double r, double theta)
        {
            double imaginary = (r * Math.Sin(theta));
            double real = (r * Math.Cos(theta));

            return new Complex(real, imaginary);
        }
    }
}
