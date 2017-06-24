using System;

namespace UnforgottenRealms.Controllers
{
    public abstract class ControllerBase : IDisposable
    {
        public abstract (Type, ControllerCreationArguments) Run();

        public virtual void Dispose()
        {
        }
    }
}
