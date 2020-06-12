using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MordhauHud.Converters
{
    internal class CircleMenuItemToContentPosition : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 6)
            {
                throw new ArgumentException("CircleMenuItemToContentPosition converter needs 6 values (double angle, double centerX, double centerY, double contentWidth, double contentHeight, double contentRadius) !", "values");
            }
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter", "CircleMenuItemToContentPosition converter needs the parameter (string axis) !");
            }

            var axis = (string)parameter;

            if (axis != "X" && axis != "Y")
            {
                throw new ArgumentException("CircleMenuItemToContentPosition parameter needs to be 'X' or 'Y' !", "parameter");
            }

            var angle = (double)values[0];
            var centerX = (double)values[1];
            var centerY = (double)values[2];
            var contentWidth = (double)values[3];
            var contentHeight = (double)values[4];
            var contentRadius = (double)values[5];

            var contentPosition = ComputeCartesianCoordinate(new Point(centerX, centerY), angle, contentRadius);

            if (axis == "X")
            {
                return contentPosition.X - (contentWidth / 2);
            }

            return contentPosition.Y - (contentHeight / 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("CircleMenuItemToContentPosition is a One-Way converter only !");
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