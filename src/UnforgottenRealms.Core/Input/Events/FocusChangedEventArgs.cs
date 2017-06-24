using SFML.Window;
using System;

namespace UnforgottenRealms.Core.Input.Events
{
    public class FocusChangedEventArgs : EventArgs
    {
        public IHandle<MouseButtonEventArgs> FocusedHandle { get; }

        public FocusChangedEventArgs(IHandle<MouseButtonEventArgs> focusedHandle)
        {
            FocusedHandle = focusedHandle;
        }
    }
}
