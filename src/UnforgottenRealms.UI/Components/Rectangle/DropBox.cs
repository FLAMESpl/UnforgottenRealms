using System.Collections.Generic;
using SFML.Window;
using UnforgottenRealms.Core.Input.Events;
using UnforgottenRealms.UI.Containers;
using SFML.Graphics;
using System.Linq;
using SFML.System;
using System;
using UnforgottenRealms.Core.Utils;

namespace UnforgottenRealms.UI.Components.Rectangle
{
    public class DropBox : Button
    {
        private ComponentContainer componentContainer = new ComponentContainer();
        public ICollection<ComponentBase> Components => componentContainer;
        public bool Expanded { get; private set; } = false;

        private FloatRect menuRegion = new FloatRect(0, 0, 150, 0);

        private float menuWidth = 150;
        public float MenuWidth
        {
            get => menuWidth;
            set
            {
                menuWidth = value;
                foreach (var item in Components)
                    item.Size = item.Size.SetX(menuWidth);
                menuRegion.Width = value;
            }
        }

        public override Vector2f Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                componentContainer.Position = MenuConatinerPosition();
            }
        }

        public override Vector2f Size
        {
            get => base.Size;
            set
            {
                Shape.Size = value;
                componentContainer.Position = MenuConatinerPosition();
            }
        }

        public DropBox()
        {
            componentContainer.ComponentAdded += ComponentAdded;
            componentContainer.ComponentRemoved += ComponentRemoved;
        }

        public override void Trigger(FocusChangedEventArgs eventArgs)
        {
            base.Trigger(eventArgs);
            Expand(eventArgs.FocusedHandle == this || Components.Contains(eventArgs.FocusedHandle));
        }

        public void Expand(bool expanded)
        {
            if (expanded != Expanded)
            {
                Expanded = expanded;
                componentContainer.Enabled = expanded;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Enabled)
            {
                Shape?.Draw(target, states);
                Text?.Draw(target, states);
                
                if (Expanded)
                {
                    target.Draw(componentContainer, states);
                }
            }
        }

        private void ComponentAdded(object sender, ComponentBase component)
        {
            component.FocusLost += MenuItemLostFocus;
            component.Position = new Vector2f(0, menuRegion.Height);
            menuRegion.Height += component.Size.Y;
            component.Size = component.Size.SetX(menuWidth);
        }

        private void ComponentRemoved(object sender, ComponentBase component)
        {
            component.FocusLost -= MenuItemLostFocus;
            menuRegion.Height -= component.Size.Y;

            var heightSoFar = 0f;
            foreach (var item in Components)
            {
                item.Position = new Vector2f(0, heightSoFar);
                heightSoFar += item.Size.Y;
            }
        }

        private void MenuItemLostFocus(object sender, FocusChangedEventArgs e)
        {
            if (!Components.Contains(e.FocusedHandle))
                Expand(false);
        }

        private Vector2f MenuConatinerPosition() => new Vector2f(Position.X, Position.Y + Size.Y);
    }
}
