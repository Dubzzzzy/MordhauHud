using System.Collections.Generic;
using System.Linq;

namespace MordhauHud
{
    public class VoiceCommandsMenu
    {
        public IList<ICommand> Commands { get; set; } = new List<ICommand>();

        public ICommand SelectedCommand { get; set; }

        public void Execute() => SelectedCommand?.Execute();

        public void Select(ICommand command) =>
            SelectedCommand = command;
    }
}
