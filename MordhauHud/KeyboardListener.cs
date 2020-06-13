using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace MordhauHud
{
    public class KeyboardListener
    {
        private readonly ICollection<Keys> _observedKeys = new Collection<Keys>();

        private readonly ISet<Keys> _pressedKeys = new HashSet<Keys>();

        public event Action<Keys> KeyDown;

        public event Action<Keys> KeyUp;

        public KeyboardListener()
        {
            KeyDown += key => _pressedKeys.Add(key);
            KeyUp += key => _pressedKeys.Remove(key);
        }

        public void ListenKey(Keys key)
        {
            _observedKeys.Add(key);
        }

        public void Update()
        {
            foreach (var key in _observedKeys)
            {
                var keyWasPressed = _pressedKeys.Contains(key);
                var keyIsDown = ((WinApi.GetKeyState(key) >> 15) & 1) == 1;
                var keyIsPressed = keyIsDown && !keyWasPressed;
                var keyIsReleased = !keyIsDown && keyWasPressed;

                if (keyIsPressed)
                {
                    KeyDown?.Invoke(key);
                }

                else if (keyIsReleased)
                {
                    KeyUp?.Invoke(key);
                }

            }
        }
    }
}
