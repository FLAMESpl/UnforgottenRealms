using System;

namespace UnforgottenRealms.Core.Input
{
    public abstract class InputHandle
    {
        public abstract bool ContainsMouse(int x, int y);
        public abstract void Trigger();
    }
}
