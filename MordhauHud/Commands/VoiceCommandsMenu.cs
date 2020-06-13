using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MordhauHud.Commands
{
    public class VoiceCommandsMenu : ICommandsMenu
    {
        private ICommand _selectedCommand;

        public IEnumerable<ICommand> Commands { get; } = new ICommand[]
        {
            new Command ("Charge", () => { WinApi.KeyPress(Keys.C, Keys.C, Keys.D5); }),
            new Command ("Yes", () => { WinApi.KeyPress(Keys.D1); }),
            new Command ("No", () => { WinApi.KeyPress(Keys.D2); }),
            new Command ("Laugh", () => { WinApi.KeyPress(Keys.C, Keys.D2); }),
            new Command ("Insult", () => { WinApi.KeyPress(Keys.D4); }),
            new Command ("Help", () => { WinApi.KeyPress(Keys.D3); }),
            new Command ("Sorry", () => { WinApi.KeyPress(Keys.C, Keys.D1); }),
            new Command ("Thanks", () => { WinApi.KeyPress(Keys.C, Keys.D3); }),
            new Command ("Hello", () => { WinApi.KeyPress(Keys.C, Keys.C, Keys.D2); }),
            new Command ("Respect", () => { WinApi.KeyPress(Keys.C, Keys.C, Keys.D4); }),
        };

        public void Execute() => _selectedCommand?.Execute();

        public void SelectCommand(ICommand command)
        {
            if (!Commands.Contains(command))
                throw new InvalidOperationException("Voice Command Menu does not contain this command");

            _selectedCommand = command;
        }
    }
}
