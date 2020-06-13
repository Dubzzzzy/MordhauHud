using System.Collections.Generic;

namespace MordhauHud.Commands
{
    public interface ICommandsMenu
    {
        IEnumerable<ICommand> Commands { get; }

        void SelectCommand(ICommand command);

        void Execute();
    }
}