using System.Windows;
using System.Windows.Controls;

namespace MordhauHud.CircleMenu
{
    public class CircleMenuCentralItem : Button
    {
        static CircleMenuCentralItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMenuCentralItem), new FrameworkPropertyMetadata(typeof(CircleMenuCentralItem)));
        }
    }
}
