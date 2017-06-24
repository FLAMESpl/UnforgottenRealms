using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using UnforgottenRealms.UI.Containers;

namespace UnforgottenRealms.UI.Components.Rectangle
{
    public class Frame : RectangleComponentBase
    {
        private ComponentContainer componentsContainer = new ComponentContainer();
        public ICollection<ComponentBase> Components => componentsContainer;

        public override Vector2f Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                componentsContainer.Position = value + OwningContainer.Position;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            target.Draw(componentsContainer, states);
        }
    }
}
