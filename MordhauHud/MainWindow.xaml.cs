using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using MordhauHud.Annotations;
using MordhauHud.CircleMenu;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Timer = System.Timers.Timer;

namespace MordhauHud
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _voiceCircleMenuIsOpen = false;
        public bool VoiceCircleMenuIsOpen
        {
            get => _voiceCircleMenuIsOpen;
            set
            {
                _voiceCircleMenuIsOpen = value;
                if (value)
                {
                    MakeHudWindowOpaque();
                }
                else
                {
                    MakeHudWindowTransparent();
                }
                OnPropertyChanged();
            }
        }

        private Controller _controller;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MakeHudWindowTransparent();

            _controller = new Controller(this);
            _controller.SetUp();
        }

        private void MakeHudWindowTransparent()
        {
            var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            WinApi.SetTransparent(mainWindowHandle);
        }

        private void MakeHudWindowOpaque()
        {
            var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            WinApi.SetOpaque(mainWindowHandle);
        }

        public void Resize(double width, double height, double left, double top)
        {
            Width = width;
            Height = height;
            Left = left;
            Top = top;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
