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

        public void ListenKey(Keys key)
        {
            _observedKeys.Add(key);
        }

        public void Update()
        {
            foreach (var observerKey in _observedKeys)
            {
                var keyState = WinApi.GetKeyState(observerKey);

                if (((keyState >> 15) & 1) == 1 && !_pressedKeys.Contains(observerKey))
                {
                    _pressedKeys.Add(observerKey);
                    KeyDown?.Invoke(observerKey);
                }

                if (((keyState >> 15) & 1) == 0 && _pressedKeys.Contains(observerKey))
                {
                    _pressedKeys.Remove(observerKey);
                    KeyUp?.Invoke(observerKey);
                }
            }
        }
    }
}
