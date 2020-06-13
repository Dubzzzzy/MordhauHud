using System;

namespace MordhauHud.Commands
{
    public class Command : ICommand
    {
        public string Name { get; }

        private readonly Action _action;

        public Command(string name, Action action)
        {
            Name = name;
            _action = action;
        }

        public void Execute()
        {
            _action?.Invoke();
        }
    }
}