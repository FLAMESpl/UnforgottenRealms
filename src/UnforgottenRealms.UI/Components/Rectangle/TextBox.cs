using System;
using SFML.Window;
using UnforgottenRealms.Core.Input;
using SFML.Graphics;
using UnforgottenRealms.Core.Input.Events;

namespace UnforgottenRealms.UI.Components.Rectangle
{
    public class TextBox : RectangleComponentBase, IHandle<TextEventArgs>
    {
        public event EventHandler<TextEventArgs> TextEntered;

        public bool Highlighted { get; protected set; } = false;

        private int maxLength = Int32.MaxValue;
        public int MaxLength
        {
            get => maxLength;
            set
            {
                maxLength = value;
                if (Text != null && Text.DisplayedString.Length > maxLength)
                    Text.DisplayedString = Text.DisplayedString.Substring(0, maxLength);
            }
        }

        private bool readOnly = false;
        public bool ReadOnly
        {
            get => readOnly;
            set
            {
                readOnly = value;
                if (readOnly)
                    TurnHighlight(false);
                else
                    TurnHighlight(Enabled);
            }
        }

        private Color idleColor;
        public Color IdleColor
        {
            get => idleColor;
            set
            {
                idleColor = value;
                if (!Highlighted && Shape != null)
                    Shape.FillColor = value;
            }
        }

        private Color highlightColor;
        public Color HighlightColor
        {
            get => highlightColor;
            set
            {
                highlightColor = value;
                if (Highlighted)
                    Shape.FillColor = highlightColor;
            }
        }

        public void TurnHighlight(bool highlighted)
        {
            if (highlighted != Highlighted)
            {
                Highlighted = highlighted;
                Shape.FillColor = highlighted ? highlightColor : idleColor;
            }
        }

        public bool DoesApply(TextEventArgs eventArgs) => Focused;

        public void Trigger(TextEventArgs eventArgs)
        {
            TextEntered?.Invoke(this, eventArgs);
            EnterText(eventArgs.Unicode);
        }

        public void EnterText(string input)
        {
            if (!ReadOnly)
            {
                var letter = input[0];
                if ('\b' != letter)
                {
                    if (Text.DisplayedString.Length != MaxLength)
                        Text.DisplayedString += letter;
                }
                else
                {
                    if (Text.DisplayedString.Length != 0)
                        Text.DisplayedString = Text.DisplayedString.Substring(0, Text.DisplayedString.Length - 1);
                }
            }
        }

        public override void Trigger(FocusChangedEventArgs eventArgs)
        {
            base.Trigger(eventArgs);
            TurnHighlight(eventArgs.FocusedHandle == this);
        }
    }
}
