using SFML.Graphics;
using UnforgottenRealms.Core.Input;
using UnforgottenRealms.Core.Input.Events;

namespace UnforgottenRealms.UI.Components.Rectangle
{
    public class Button : RectangleComponentBase
    {
        public bool Highlighted { get; protected set; } = false;

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
                if (Highlighted && Shape != null)
                    Shape.FillColor = highlightColor;
            }
        }
        
        public void TurnHighlight(bool highlighted)
        {
            Highlighted = highlighted;
            Shape.FillColor = highlighted ? highlightColor : idleColor;
        }

        public override void Trigger(MouseEnteredRegionEventArgs eventArgs)
        {
            base.Trigger(eventArgs);
            TurnHighlight(eventArgs.Region == this);
        }
    }
}
