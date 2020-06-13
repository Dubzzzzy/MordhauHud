using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MordhauHud.CircleMenu
{
    public class CircleMenu : ContentControl
    {
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(CircleMenu),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty HalfShiftedItemsProperty =
            DependencyProperty.Register(nameof(HalfShiftedItems), typeof(bool), typeof(CircleMenu),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool HalfShiftedItems
        {
            get => (bool)GetValue(HalfShiftedItemsProperty);
            set => SetValue(HalfShiftedItemsProperty, value);
        }

        public static readonly DependencyProperty CentralItemProperty =
            DependencyProperty.Register(nameof(CentralItem), typeof(CircleMenuCentralItem), typeof(CircleMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public CircleMenuCentralItem CentralItem
        {
            get => (CircleMenuCentralItem)GetValue(CentralItemProperty);
            set => SetValue(CentralItemProperty, value);
        }

        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(List<CircleMenuItem>), typeof(CircleMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public new List<CircleMenuItem> Content
        {
            get => (List<CircleMenuItem>)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public List<CircleMenuItem> Items
        {
            get => Content;
            set => Content = value;
        }

        static CircleMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMenu), new FrameworkPropertyMetadata(typeof(CircleMenu)));
        }

        public override void BeginInit()
        {
            Content = new List<CircleMenuItem>();
            base.BeginInit();
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            for (int i = 0, count = Items.Count; i < count; i++)
            {
                Items[i].Index = i;
                Items[i].Count = count;
                Items[i].HalfShifted = HalfShiftedItems;
            }
            return base.ArrangeOverride(arrangeSize);
        }
    }
}
