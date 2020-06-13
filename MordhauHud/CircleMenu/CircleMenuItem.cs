using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MordhauHud.CircleMenu
{
    public class CircleMenuItem : Button
    {
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, UpdateItemRendering));

        public int Index
        {
            get => (int)GetValue(IndexProperty);
            set => SetValue(IndexProperty, value);
        }

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register(nameof(Count), typeof(int), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, UpdateItemRendering));

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public static readonly DependencyProperty HalfShiftedProperty =
            DependencyProperty.Register(nameof(HalfShifted), typeof(bool), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, UpdateItemRendering));

        public bool HalfShifted
        {
            get => (bool)GetValue(HalfShiftedProperty);
            set => SetValue(HalfShiftedProperty, value);
        }

        public static readonly DependencyProperty CenterXProperty =
            DependencyProperty.Register(nameof(CenterX), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double CenterX
        {
            get => (double)GetValue(CenterXProperty);
            set => SetValue(CenterXProperty, value);
        }

        public static readonly DependencyProperty CenterYProperty =
            DependencyProperty.Register(nameof(CenterY), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double CenterY
        {
            get => (double)GetValue(CenterYProperty);
            set => SetValue(CenterYProperty, value);
        }

        public static readonly DependencyProperty OuterRadiusProperty =
            DependencyProperty.Register(nameof(OuterRadius), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double OuterRadius
        {
            get => (double)GetValue(OuterRadiusProperty);
            set => SetValue(OuterRadiusProperty, value);
        }

        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register(nameof(InnerRadius), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double InnerRadius
        {
            get => (double)GetValue(InnerRadiusProperty);
            set => SetValue(InnerRadiusProperty, value);
        }

        public new static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ContentRadiusProperty =
            DependencyProperty.Register(nameof(ContentRadius), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ContentRadius
        {
            get => (double)GetValue(ContentRadiusProperty);
            set => SetValue(ContentRadiusProperty, value);
        }

        public static readonly DependencyProperty EdgeOuterRadiusProperty =
            DependencyProperty.Register(nameof(EdgeOuterRadius), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double EdgeOuterRadius
        {
            get => (double)GetValue(EdgeOuterRadiusProperty);
            set => SetValue(EdgeOuterRadiusProperty, value);
        }

        public static readonly DependencyProperty EdgeInnerRadiusProperty =
            DependencyProperty.Register(nameof(EdgeInnerRadius), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double EdgeInnerRadius
        {
            get => (double)GetValue(EdgeInnerRadiusProperty);
            set => SetValue(EdgeInnerRadiusProperty, value);
        }

        public static readonly DependencyProperty EdgePaddingProperty =
            DependencyProperty.Register(nameof(EdgePadding), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double EdgePadding
        {
            get => (double)GetValue(EdgePaddingProperty);
            set => SetValue(EdgePaddingProperty, value);
        }

        public static readonly DependencyProperty EdgeBackgroundProperty =
            DependencyProperty.Register(nameof(EdgeBackground), typeof(Brush), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Brush EdgeBackground
        {
            get => (Brush)GetValue(EdgeBackgroundProperty);
            set => SetValue(EdgeBackgroundProperty, value);
        }

        public static readonly DependencyProperty EdgeBorderThicknessProperty =
            DependencyProperty.Register(nameof(EdgeBorderThickness), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double EdgeBorderThickness
        {
            get => (double)GetValue(EdgeBorderThicknessProperty);
            set => SetValue(EdgeBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty EdgeBorderBrushProperty =
            DependencyProperty.Register(nameof(EdgeBorderBrush), typeof(Brush), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Brush EdgeBorderBrush
        {
            get => (Brush)GetValue(EdgeBorderBrushProperty);
            set => SetValue(EdgeBorderBrushProperty, value);
        }

        public static readonly DependencyProperty ArrowBackgroundProperty =
            DependencyProperty.Register(nameof(ArrowBackground), typeof(Brush), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Brush ArrowBackground
        {
            get => (Brush)GetValue(ArrowBackgroundProperty);
            set => SetValue(ArrowBackgroundProperty, value);
        }

        public static readonly DependencyProperty ArrowBorderThicknessProperty =
            DependencyProperty.Register(nameof(ArrowBorderThickness), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowBorderThickness
        {
            get => (double)GetValue(ArrowBorderThicknessProperty);
            set => SetValue(ArrowBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty ArrowBorderBrushProperty =
            DependencyProperty.Register(nameof(ArrowBorderBrush), typeof(Brush), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Brush ArrowBorderBrush
        {
            get => (Brush)GetValue(ArrowBorderBrushProperty);
            set => SetValue(ArrowBorderBrushProperty, value);
        }

        public static readonly DependencyProperty ArrowWidthProperty =
            DependencyProperty.Register(nameof(ArrowWidth), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowWidth
        {
            get => (double)GetValue(ArrowWidthProperty);
            set => SetValue(ArrowWidthProperty, value);
        }

        public static readonly DependencyProperty ArrowHeightProperty =
            DependencyProperty.Register(nameof(ArrowHeight), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowHeight
        {
            get => (double)GetValue(ArrowHeightProperty);
            set => SetValue(ArrowHeightProperty, value);
        }

        public static readonly DependencyProperty ArrowRadiusProperty =
            DependencyProperty.Register(nameof(ArrowRadius), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowRadius
        {
            get => (double)GetValue(ArrowRadiusProperty);
            set => SetValue(ArrowRadiusProperty, value);
        }

        protected static readonly DependencyPropertyKey AngleDeltaPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(AngleDelta), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty AngleDeltaProperty = AngleDeltaPropertyKey.DependencyProperty;

        public double AngleDelta
        {
            get => (double)GetValue(AngleDeltaProperty);
            protected set => SetValue(AngleDeltaPropertyKey, value);
        }

        protected static readonly DependencyPropertyKey StartAnglePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(StartAngle), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty StartAngleProperty = StartAnglePropertyKey.DependencyProperty;

        public double StartAngle
        {
            get => (double)GetValue(StartAngleProperty);
            protected set => SetValue(StartAnglePropertyKey, value);
        }

        protected static readonly DependencyPropertyKey RotationPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Rotation), typeof(double), typeof(CircleMenuItem),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty RotationProperty = RotationPropertyKey.DependencyProperty;

        public double Rotation
        {
            get => (double)GetValue(RotationProperty);
            protected set => SetValue(RotationPropertyKey, value);
        }

        static CircleMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMenuItem), new FrameworkPropertyMetadata(typeof(CircleMenuItem)));
        }

        private static void UpdateItemRendering(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CircleMenuItem item)
            {
                var angleDelta = 360.0 / item.Count;
                var angleShift = item.HalfShifted ? -angleDelta / 2 : 0;
                var startAngle = angleDelta * item.Index + angleShift;
                var rotation = startAngle + angleDelta / 2;

                item.AngleDelta = angleDelta;
                item.StartAngle = startAngle;
                item.Rotation = rotation;
            }
        }
    }
}
