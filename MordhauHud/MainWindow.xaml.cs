using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MordhauHud.Annotations;
using MordhauHud.CircleMenu;
using MordhauHud.Commands;

namespace MordhauHud
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private IEnumerable<ICommand> _commands;

        private bool _voiceCircleMenuIsOpen;

        public bool VoiceCircleMenuIsOpen
        {
            get => _voiceCircleMenuIsOpen;
            set
            {
                _voiceCircleMenuIsOpen = value;
                OnPropertyChanged();
            }
        }

        private Controller _controller;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        // Entry Point
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MakeHudWindowTransparent();

            _controller = new Controller(this);
            _controller.SetUp();
        }

        public void OpenVoiceMenu()
        {
            SetMousePositionInCenter();
            MakeHudWindowOpaque();
            VoiceCircleMenuIsOpen = true;
            _controller.SelectVoiceCommand(GetCentralCommand());
        }

        public void CloseVoiceMenu()
        {
            MakeHudWindowTransparent();
            VoiceCircleMenuIsOpen = false;
        }

        private void MakeHudWindowTransparent()
        {
            Background = new SolidColorBrush(Colors.Transparent);
            var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            WinApi.SetTransparent(mainWindowHandle);
        }

        private void MakeHudWindowOpaque()
        {
            Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
            var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            WinApi.SetOpaque(mainWindowHandle);
        }

        private void SetMousePositionInCenter() => 
            WinApi.SetCursorPos(Left + Width / 2, Top + Height / 2);

        public void Resize(Rect rect)
        {
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;
            Resize(width, height, rect.Left, rect.Top);
        }

        private void Resize(double width, double height, double left, double top)
        {
            Width = width;
            Height = height;
            Left = left;
            Top = top;
        }

        public void SetVoiceCommands(IEnumerable<ICommand> commands)
        {
            _commands = commands;
            VoiceCircleMenu.CentralItem = CreateCircleMenuCentralItem(GetCentralCommand());
            VoiceCircleMenu.Content = GetCircleCommands().Select(CreateCircleMenuItem).ToList();
        }

        private ICommand GetCentralCommand() =>
            _commands.First();

        private IEnumerable<ICommand> GetCircleCommands() => 
            _commands.Skip(1);

        private CircleMenuCentralItem CreateCircleMenuCentralItem(ICommand command)
        {
            var textBlock = new TextBlock { Text = command.Name, Margin = new Thickness(0, 5, 0, 0) };
            var circleMenuCentralItem = new CircleMenuCentralItem { Content = textBlock };
            circleMenuCentralItem.MouseEnter += (sender, args) => _controller.SelectVoiceCommand(command);

            return circleMenuCentralItem;
        }

        private CircleMenuItem CreateCircleMenuItem(ICommand command)
        {
            var textBlock = new TextBlock { Text = command.Name };
            var circleMenuItem = new CircleMenuItem { Content = textBlock };
            circleMenuItem.MouseEnter += (sender, args) => _controller.SelectVoiceCommand(command);

            return circleMenuItem;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
