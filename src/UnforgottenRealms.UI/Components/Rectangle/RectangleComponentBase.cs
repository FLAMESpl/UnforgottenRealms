using SFML.System;

namespace UnforgottenRealms.UI.Components.Rectangle
{
    public abstract class RectangleComponentBase : ComponentBase
    {
        private Vector2f position;
        public override Vector2f Position
        {
            get => position;
            set
            {
                //position = 
            }
        }

        private Vector2f textPosition;
        public Vector2f TextPosition
        {
            get => textPosition;
            set
            {

            }
        }

        public override void Invalidate()
        {

        }
    }
}
