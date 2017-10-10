using System;

namespace UnforgottenRealms.Controllers
{
    public abstract class ControllerBase : IDisposable
    {
        public abstract (Type, ControllerArguments) Run();

        public virtual void Dispose()
        {
        }
    }
}
