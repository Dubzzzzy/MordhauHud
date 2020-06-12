using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using MordhauHud.CircleMenu;
using Timer = System.Timers.Timer;

namespace MordhauHud
{
    public class Controller
    {
        public const string ProcessWindowTitle = "MORDHAU";

        private readonly IList<ICommand> _voiceCommands = new ICommand[]
        {
            new Command {Name = "Charge", Action = () => { WinApi.KeyPress(Keys.C, Keys.C, Keys.D5); }},
            new Command {Name = "Yes", Action = () => { WinApi.KeyPress(Keys.D1); }},
            new Command {Name = "No", Action = () => { WinApi.KeyPress(Keys.D2); }},
            new Command {Name = "Laugh", Action = () => { WinApi.KeyPress(Keys.C, Keys.D2); }},
            new Command {Name = "Insult", Action = () => { WinApi.KeyPress(Keys.D4); }},
            new Command {Name = "Help", Action = () => { WinApi.KeyPress(Keys.D3); }},
            new Command {Name = "Sorry", Action = () => { WinApi.KeyPress(Keys.C, Keys.D1); }},
            new Command {Name = "Thanks", Action = () => { WinApi.KeyPress(Keys.C, Keys.D3); }},
            new Command {Name = "Hello", Action = () => { WinApi.KeyPress(Keys.C, Keys.C, Keys.D2); }},
            new Command {Name = "Respect", Action = () => { WinApi.KeyPress(Keys.C, Keys.C, Keys.D4); }},
        };

        private readonly MainWindow _mainWindow;

        private readonly KeyboardListener _keyboardListener;

        private readonly VoiceCommandsMenu _voiceCommandsMenu;

        private ProcessWindow _targetProcessWindow;

        public Controller(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _voiceCommandsMenu = new VoiceCommandsMenu { Commands = _voiceCommands };
            _keyboardListener = new KeyboardListener();
        }

        public void SetUp()
        {
            _targetProcessWindow = new ProcessWindow(ProcessWindowTitle);
            FillMainWindow();
            ListenKeyboard();
        }

        private void ListenKeyboard()
        {
            _keyboardListener.ListenKey(Keys.C);
            _keyboardListener.KeyDown += HandleKeyDown;
            _keyboardListener.KeyUp += HandleKeyUp;
            StartKeyboardListening();
        }

        private void FillMainWindow()
        {
            _mainWindow.VoiceCircleMenu.CentralItem = GetVoiceCircleMenuCentralItem();
            _mainWindow.VoiceCircleMenu.Content = GetVoiceCircleMenuItems();

            var rect = _targetProcessWindow.Rect;

            _mainWindow.Resize(rect.Right - rect.Left, rect.Bottom - rect.Top, rect.Left, rect.Top);
        }

        private void HandleKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.C:
                    SetMousePositionInTheCenter();
                    OpenVoiceCircleMenu();
                    break;
                default:
                    break;
            }
        }

        private void OpenVoiceCircleMenu() =>
            _mainWindow.Dispatcher.Invoke(() => { _mainWindow.VoiceCircleMenuIsOpen = true; });

        private void HandleKeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.C:
                    CloseVoiceCircleMenu();
                    _voiceCommandsMenu.Execute();
                    break;
                default:
                    break;
            }
        }

        private void SetMousePositionInTheCenter()
        {
            var rect = _targetProcessWindow.Rect;
            WinApi.SetCursorPos(rect.Left + (rect.Right - rect.Left) / 2, rect.Top + (rect.Bottom - rect.Top) / 2);
        }

        private void CloseVoiceCircleMenu() =>
            _mainWindow.Dispatcher.Invoke(() => { _mainWindow.VoiceCircleMenuIsOpen = false; });

        public void StartKeyboardListening()
        {
            var timer = new Timer(10);
            timer.Elapsed += (o, args) => _keyboardListener.Update();
            timer.Start();
        }

        public CircleMenuCentralItem GetVoiceCircleMenuCentralItem() =>
            CreateCircleMenuCentralItem(_voiceCommandsMenu.Commands.First());

        public List<CircleMenuItem> GetVoiceCircleMenuItems() =>
            _voiceCommandsMenu.Commands.Skip(1).Select(CreateCircleMenuItem).ToList();

        private CircleMenuCentralItem CreateCircleMenuCentralItem(ICommand command)
        {
            var textBlock = new TextBlock { Text = command.Name, Margin = new Thickness(0, 5, 0, 0) };
            var circleMenuCentralItem = new CircleMenuCentralItem { Content = textBlock };
            circleMenuCentralItem.MouseEnter += (sender, args) => SelectVoiceCommand(command);

            return circleMenuCentralItem;
        }

        private CircleMenuItem CreateCircleMenuItem(ICommand command)
        {
            var textBlock = new TextBlock { Text = command.Name };
            var circleMenuItem = new CircleMenuItem { Content = textBlock };
            circleMenuItem.MouseEnter += (sender, args) => SelectVoiceCommand(command);

            return circleMenuItem;
        }

        public void SelectVoiceCommand(ICommand command) =>
            _voiceCommandsMenu.Select(command);
    }
}