using System.Windows.Forms;
using MordhauHud.Commands;

namespace MordhauHud
{
    public class Controller
    {
        public const string ProcessWindowTitle = "MORDHAU";

        private readonly MainWindow _mainWindow;

        private readonly KeyboardListener _keyboardListener;

        private readonly ICommandsMenu _voiceCommandsMenu;

        private readonly ProcessWindow _targetProcessWindow;

        public Controller(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _keyboardListener = new KeyboardListener();
            _voiceCommandsMenu = new VoiceCommandsMenu();
            _targetProcessWindow = new ProcessWindow(ProcessWindowTitle);
        }

        public void SetUp()
        {
            _mainWindow.SetVoiceCommands(_voiceCommandsMenu.Commands);
            ResizeMainWindow();
            ListenKeyboard();
        }

        private void ResizeMainWindow()
        {
            var rect = _targetProcessWindow.Rect;
            _mainWindow.Resize(rect);
        }

        private void ListenKeyboard()
        {
            _keyboardListener.ListenKey(Keys.C);
            _keyboardListener.KeyDown += HandleKeyDown;
            _keyboardListener.KeyUp += HandleKeyUp;
            StartKeyboardListening();
        }

        public void StartKeyboardListening()
        {
            var timer = new System.Timers.Timer(20);
            timer.Elapsed += (o, args) => _keyboardListener.Update();
            timer.Start();
        }

        private void HandleKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.C:
                    OpenVoiceCircleMenu();
                    break;
            }
        }

        private void HandleKeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.C:
                    CloseVoiceCircleMenu();
                    _voiceCommandsMenu.Execute();
                    break;
            }
        }

        private void OpenVoiceCircleMenu() =>
            _mainWindow.Dispatcher.Invoke(() => { _mainWindow.OpenVoiceMenu(); });

        private void CloseVoiceCircleMenu() =>
            _mainWindow.Dispatcher.Invoke(() => { _mainWindow.CloseVoiceMenu(); });

        public void SelectVoiceCommand(ICommand command) =>
            _voiceCommandsMenu.SelectCommand(command);
    }
}