using System;

namespace MordhauHud
{
    public class Command : ICommand
    {
        public string Name { get; set; }

        public Action Action { get; set; }

        public void Execute()
        {
            Action?.Invoke();
        }
    }
}