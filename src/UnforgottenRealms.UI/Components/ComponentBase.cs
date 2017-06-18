using SFML.System;
using UnforgottenRealms.UI.Containers;

namespace UnforgottenRealms.UI.Components
{
    public abstract class ComponentBase
    {
        private ComponentContainer owningContainer;
        public ComponentContainer OwningContainer
        {
            get => owningContainer;
            set
            {
                owningContainer = value;
                Invalidate();
            }
        }

        public abstract Vector2f Position { get; set; }

        public abstract void Invalidate();
    }
}
