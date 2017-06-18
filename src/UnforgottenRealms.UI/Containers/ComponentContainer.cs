using SFML.System;
using System.Collections.Generic;
using UnforgottenRealms.UI.Components;

namespace UnforgottenRealms.UI.Containers
{
    public class ComponentContainer
    {
        public ICollection<ComponentBase> Components { get; } = new List<ComponentBase>();

        private Vector2f position;
        public Vector2f Position
        {
            get => position;
            set
            {
                position = value;
                foreach (var component in Components)
                    component.Invalidate();
            }
        }
    }
}
