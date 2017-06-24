using SFML.Window;
using System;

namespace UnforgottenRealms.Core.Input.Events
{
    public class MouseEnteredRegionEventArgs : EventArgs
    {
        public MouseMoveEventArgs MouseState { get; }
        public IHandle<MouseMoveEventArgs> Region { get; }

        public MouseEnteredRegionEventArgs(MouseMoveEventArgs mouseState, IHandle<MouseMoveEventArgs> region)
        {
            MouseState = mouseState;
            Region = region;
        }
    }
}
