using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MordhauHud.Converters
{
    internal class CircleMenuItemToArrowPosition : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 5)
            {
                throw new ArgumentException("CircleMenuItemToArrowPosition converter needs 7 values (double centerX, double centerY, double arrowWidth, double arrowHeight, double arrowRadius) !", "values");
            }
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter", "CircleMenuItemToArrowPosition converter needs the parameter (string axis) !");
            }

            var axis = (string)parameter;

            if (axis != "X" && axis != "Y")
            {
                throw new ArgumentException("CircleMenuItemToArrowPosition parameter needs to be 'X' or 'Y' !", "parameter");
            }

            var centerX = (double)values[0];
            var centerY = (double)values[1];
            var arrowWidth = (double)values[2];
            var arrowHeight = (double)values[3];
            var arrowRadius = (double)values[4];

            if (axis == "X")
            {
                return centerX - (arrowWidth / 2);
            }

            return centerY - arrowRadius - (arrowHeight / 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("CircleMenuItemToArrowPosition is a One-Way converter only !");
        }

        private static Point ComputeCartesianCoordinate(Point center, double angle, double radius)
        {
            // Converts to radians
            var radiansAngle = (Math.PI / 180.0) * (angle - 90);
            var x = radius * Math.Cos(radiansAngle);
            var y = radius * Math.Sin(radiansAngle);
            return new Point(x + center.X, y + center.Y);
        }
    }
}