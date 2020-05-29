using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sounds.Helpers
{
    public class Complex
    {
        private float _real;
        private float _imaginary;

        public Complex(float real, float imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public float Real { get => _real; set => _real = value; }
        public float Imaginary { get => _imaginary; set => _imaginary = value; }

        public float Modulus()
        {
            return (float)Math.Sqrt(_real * _real + _imaginary * _imaginary);
        }

        public float Phase()
        {
            return (float)Math.Atan2(_imaginary, _real);
        }

        public Complex Plus(Complex second)
        {
            Complex first = this;
            float realPart = first._real + second._real;
            float imaginaryPart = first._imaginary + second._imaginary;
            return new Complex(realPart, imaginaryPart);
        }

        public Complex Minus(Complex second)
        {
            Complex first = this;
            float realPart = first._real - second._real;
            float imaginaryPart = first._imaginary - second._imaginary;
            return new Complex(realPart, imaginaryPart);
        }

        public Complex Times(Complex second)
        {
            Complex first = this;
            float realPart = first._real * second._real - first._imaginary * second._imaginary;
            float imaginaryPart = first._real * second._imaginary + first._imaginary * second._real;
            return new Complex(realPart, imaginaryPart);
        }

        public Complex Times(float alpha)
        {
            return new Complex(alpha * _real, alpha * _imaginary);
        }

        public Complex Conjugate()
        {
            return new Complex(_real, -_imaginary);
        }

        public static Complex FromPolar(float r, float angle)
        {
            float real = (float)(r * Math.Cos(angle));
            float imaginary = (float)(r * Math.Sin(angle));

            return new Complex(real, imaginary);
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static Complex operator +(float a, Complex b)
        {
            return new Complex(b.Real + a, b.Imaginary);
        }

        public static Complex operator +(Complex a, float b)
        {
            return new Complex(a.Real + b, a.Imaginary);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static Complex operator *(float a, Complex b)
        {
            return new Complex(b.Real * a, b.Imaginary * a);
        }

        public static Complex operator *(Complex a, float b)
        {
            return new Complex(a.Real * b, a.Imaginary * b);
        }

        public static Complex operator /(Complex b, float a)
        {
            return new Complex(b.Real / a, b.Imaginary / a);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        public static Complex operator /(float a, Complex b)
        {
            return new Complex(a / b.Real, a / b.Imaginary);
        }

        public static bool IsNaN(Complex a)
        {
            return float.IsNaN(a.Imaginary) || float.IsNaN(a.Real);
        }
    }
}
