using SFML.System;
using System;

namespace UnforgottenRealms.UI.Containers
{
    internal class NullComponentContainer : ComponentContainer
    {
        public NullComponentContainer()
        {
            position.X = 0;
            position.Y = 0;
        }

        public override Vector2f Position
        {
            get => base.Position;
            set => throw new InvalidOperationException("Cannot set position of NullComponentContainer");
        }
    }
}
