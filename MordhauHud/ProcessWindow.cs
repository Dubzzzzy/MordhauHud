using System;
using System.Diagnostics;
using System.Linq;

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
        
        public Rect Rect
        {
            get
            {
                WinApi.GetWindowRect(Handle, out var rect);
                return rect;
            }
        }
    }
}