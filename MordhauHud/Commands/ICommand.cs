using System;

namespace MordhauHud
{
    public interface ICommand
    {
        string Name { get; set; }

        void Execute();
    }
}