﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MordhauHud.CircleMenu
{
    internal class PieShape : Shape
    {
        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register(nameof(InnerRadius), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double InnerRadius
        {
            get => (double)GetValue(InnerRadiusProperty);
            set => SetValue(InnerRadiusProperty, value);
        }

        public static readonly DependencyProperty OuterRadiusProperty =
            DependencyProperty.Register(nameof(OuterRadius), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double OuterRadius
        {
            get => (double)GetValue(OuterRadiusProperty);
            set => SetValue(OuterRadiusProperty, value);
        }

        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register(nameof(Padding), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Padding
        {
            get => (double)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public static readonly DependencyProperty PushOutProperty =
            DependencyProperty.Register(nameof(PushOut), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double PushOut
        {
            get => (double)GetValue(PushOutProperty);
            set => SetValue(PushOutProperty, value);
        }

        public static readonly DependencyProperty AngleDeltaProperty =
            DependencyProperty.Register(nameof(AngleDelta), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double AngleDelta
        {
            get => (double)GetValue(AngleDeltaProperty);
            set => SetValue(AngleDeltaProperty, value);
        }

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double StartAngle
        {
            get => (double)GetValue(StartAngleProperty);
            set => SetValue(StartAngleProperty, value);
        }

        public static readonly DependencyProperty CenterXProperty =
            DependencyProperty.Register(nameof(CenterX), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double CenterX
        {
            get => (double)GetValue(CenterXProperty);
            set => SetValue(CenterXProperty, value);
        }

        public static readonly DependencyProperty CenterYProperty =
            DependencyProperty.Register(nameof(CenterY), typeof(double), typeof(PieShape),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double CenterY
        {
            get => (double)GetValue(CenterYProperty);
            set => SetValue(CenterYProperty, value);
        }

        static PieShape()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PieShape), new FrameworkPropertyMetadata(typeof(PieShape)));
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                // Creates a StreamGeometry for describing the shape
                var geometry = new StreamGeometry { FillRule = FillRule.EvenOdd };

                using (var context = geometry.Open())
                {
                    DrawGeometry(context);
                }

                // Freezes the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

        private void DrawGeometry(StreamGeometryContext context)
        {
            if (AngleDelta <= 0)
            {
                return;
            }

            var outerStartAngle = StartAngle;
            var outerAngleDelta = AngleDelta;
            var innerStartAngle = StartAngle;
            var innerAngleDelta = AngleDelta;
            var arcCenter = new Point(CenterX, CenterY);
            var outerArcSize = new Size(OuterRadius, OuterRadius);
            var innerArcSize = new Size(InnerRadius, InnerRadius);

            // If have to draw a full-circle, draws two semi-circles, because 'ArcTo()' can not draw a full-circle
            if (AngleDelta >= 360 && Padding <= 0)
            {
                var outerArcTopPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle, OuterRadius + PushOut);
                var outerArcBottomPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle + 180, OuterRadius + PushOut);
                var innerArcTopPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle, InnerRadius + PushOut);
                var innerArcBottomPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle + 180, InnerRadius + PushOut);

                context.BeginFigure(innerArcTopPoint, true, true);
                context.LineTo(outerArcTopPoint, true, true);
                context.ArcTo(outerArcBottomPoint, outerArcSize, 0, false, SweepDirection.Clockwise, true, true);
                context.ArcTo(outerArcTopPoint, outerArcSize, 0, false, SweepDirection.Clockwise, true, true);
                context.LineTo(innerArcTopPoint, true, true);
                context.ArcTo(innerArcBottomPoint, innerArcSize, 0, false, SweepDirection.Counterclockwise, true, true);
                context.ArcTo(innerArcTopPoint, innerArcSize, 0, false, SweepDirection.Counterclockwise, true, true);
            }
            // Else draws as always
            else
            {
                if (Padding > 0)
                {
                    // Offsets the angle by the padding
                    var outerAngleVariation = (180 * (Padding / OuterRadius)) / Math.PI;
                    var innerAngleVariation = (180 * (Padding / InnerRadius)) / Math.PI;

                    outerStartAngle += outerAngleVariation;
                    outerAngleDelta -= outerAngleVariation * 2;
                    innerStartAngle += innerAngleVariation;
                    innerAngleDelta -= innerAngleVariation * 2;
                }

                var outerArcStartPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle, OuterRadius + PushOut);
                var outerArcEndPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle + outerAngleDelta, OuterRadius + PushOut);
                var innerArcStartPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle, InnerRadius + PushOut);
                var innerArcEndPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle + innerAngleDelta, InnerRadius + PushOut);

                var largeArcOuter = outerAngleDelta > 180.0;
                var largeArcInner = innerAngleDelta > 180.0;

                context.BeginFigure(innerArcStartPoint, true, true);
                context.LineTo(outerArcStartPoint, true, true);
                context.ArcTo(outerArcEndPoint, outerArcSize, 0, largeArcOuter, SweepDirection.Clockwise, true, true);
                context.LineTo(innerArcEndPoint, true, true);
                context.ArcTo(innerArcStartPoint, innerArcSize, 0, largeArcInner, SweepDirection.Counterclockwise, true, true);
            }
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
