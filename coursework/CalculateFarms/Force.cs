namespace CalculateFarms
{
    using System;

    public class Force
    {
        private readonly double _angle;
        private double _value = -1;

        public Force(double value, double angle)
        {
            _value = value;
            _angle = ConverRadian(angle);
        }

        public Force(double angle)
        {
            _angle = ConverRadian(angle);
        }

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public double Angle
        {
            get { return _angle; }
        }

        public double GetProjectionX()
        {
            return Math.Cos(_angle) * _value;
        }

        public double GetProjectionY()
        {
            return Math.Sin(_angle) * _value;
        }

        private double ConverRadian(double angle)
        {
            return Math.PI * angle / 180;
        }
    }
}
