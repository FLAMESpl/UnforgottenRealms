using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UnforgottenRealms.UI.Components.Rectangle
{
    public abstract class RectangleComponentBase : ComponentBase
    {
        public RectangleShape Shape { get; set; }
        public Text Text { get; set; }

        private Vector2f position;
        public override Vector2f Position
        {
            get => position;
            set
            {
                position = value;
                if (Shape != null)
                    Shape.Position = value + OwningContainer.Position;
                TextPosition = textPosition;
            }
        }

        public override Vector2f Size
        {
            get => Shape?.Size ?? new Vector2f();
            set => Shape.Size = value;
        }

        private Vector2f textPosition;
        public Vector2f TextPosition
        {
            get => textPosition;
            set
            {
                textPosition = value;
                if (Text != null)
                    Text.Position = value + OwningContainer.Position + position;
            }
        }

        public override bool DoesApply(MouseMoveEventArgs eventArgs) => OverallEnabled && ContainsMouse(eventArgs.X, eventArgs.Y);

        public override bool DoesApply(MouseButtonEventArgs eventArgs) => OverallEnabled && ContainsMouse(eventArgs.X, eventArgs.Y);

        public override void Invalidate()
        {
            Position = position;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Enabled)
            {
                Shape?.Draw(target, states);
                Text?.Draw(target, states);
            }
        }

        protected virtual bool ContainsMouse(int x, int y) => Shape != null && Shape.GetGlobalBounds().Contains(x, y);
    }
}
