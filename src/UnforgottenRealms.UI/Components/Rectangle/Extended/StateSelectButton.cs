using SFML.Window;
using System;

namespace UnforgottenRealms.UI.Components.Rectangle.Extended
{
    public class StateSelectButton : Button
    {
        private Action<StateSelectButton>[] states;
        public Action<StateSelectButton>[] States
        {
            get => states;
            set
            {
                states = value;
                SetState(0);
            }
        }

        private int currentIndex;

        public StateSelectButton()
        {
            MouseClicked += OnMouseClick;
        }

        public void SetState(int index)
        {
            states[index]?.Invoke(this);
            currentIndex = index;
        }

        protected virtual void OnMouseClick(object sedner, MouseButtonEventArgs args)
        {
            if (states != null)
            {
                if (args.Button == Mouse.Button.Left)
                {
                    SetState(currentIndex < states.Length - 1 ? currentIndex + 1 : 0);
                }
                else if (args.Button == Mouse.Button.Right)
                {
                    SetState(currentIndex > 0 ? currentIndex - 1 : states.Length - 1);
                }
            }
        }
    }
}
