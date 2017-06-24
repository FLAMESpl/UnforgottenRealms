using System;

namespace UnforgottenRealms.Controllers
{
    public class ExitController : ControllerBase
    {
        public override (Type, ControllerCreationArguments) Run()
        {
            throw new InvalidOperationException();
        }
    }
}
