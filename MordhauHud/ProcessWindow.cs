using System;

namespace MordhauHud
{
    public class ProcessWindow
    {
        public readonly string ProcessWindowTitle;

        public readonly IntPtr Handle;

        public ProcessWindow(string processWindowTitle)
        {
            ProcessWindowTitle = processWindowTitle;
            Handle = WinApi.FindWindow(processWindowTitle);
        }

        public Rect Rect => 
            WinApi.GetWindowRect(Handle);
    }
}