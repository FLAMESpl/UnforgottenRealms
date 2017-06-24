using System;

namespace UnforgottenRealms.Core.Input
{
    public interface IHandle<T> where T : EventArgs
    {
        bool DoesApply(T eventArgs);
        void Trigger(T eventArgs);
    }
}
