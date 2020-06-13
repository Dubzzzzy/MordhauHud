using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MordhauHud
{
    public static class WinApi
    {
        private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_RESTORE = 9;
        private const short SWP_NOMOVE = 0X2;
        private const short SWP_NOSIZE = 1;
        private const short SWP_NOZORDER = 0X4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int GWL_EXSTYLE = -20;
        private const int GWLP_HINSTANCE = -6;
        private const int GWLP_ID = -12;
        private const int GWL_STYLE = -16;
        private const int GWLP_USERDATA = -21;
        private const int GWLP_WNDPROC = -4;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_TOPMOST = 0x00000008;
        private const int WS_VISIBLE = 0x10000000;
        private const int LWA_ALPHA = 0x2;
        private const int LWA_COLORKEY = 0x1;

        public static IntPtr FindWindow(string windowName) =>
            Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle.Contains(windowName))?.MainWindowHandle ?? default;

        public static void SetTransparent(IntPtr handle)
        {
            SetWindowLong(handle, GWL_STYLE, new IntPtr(WS_VISIBLE));
            SetWindowLong(handle, GWL_EXSTYLE, new IntPtr(WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOPMOST));
        }

        public static void SetOpaque(IntPtr handle)
        {
            SetWindowLong(handle, GWL_STYLE, new IntPtr(WS_VISIBLE));
            SetWindowLong(handle, GWL_EXSTYLE, new IntPtr(WS_EX_LAYERED | WS_EX_TOPMOST));
        }

        public static bool IsForegroundWindow(IntPtr handle)
        {
            return GetForegroundWindow() == handle;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool AllowSetForegroundWindow(uint dwProcessId);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        public static int MakeCOLORREF(byte r, byte g, byte b)
        {
            return (int)(r | ((uint)g << 8) | ((uint)b << 16));
        }

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        public static Rect GetWindowRect(IntPtr hWnd)
        {
            GetWindowRect(hWnd, out var rect);
            return rect;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        public static bool ReadProcessMemory(IntPtr handle, IntPtr baseAddress, byte[] buffer) => 
            ReadProcessMemory(handle, baseAddress, buffer, buffer.Length, out _);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hWnd, IntPtr baseAddr, byte[] buffer, int size, out IntPtr bytesRead);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        public static bool SetCursorPos(double x, double y) =>
            SetCursorPos((int)x, (int)y);


        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static void KeyDown(Keys key) =>
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY, 0);

        public static void KeyUp(Keys key) =>
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);

        public static void KeyPress(Keys key)
        {
            KeyDown(key);
            KeyUp(key);
        }

        public static void KeyPress(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                KeyPress(key);
            }
        }

        [DllImport("user32.dll")]
        public static extern ushort GetKeyState(Keys vKey);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    }
}