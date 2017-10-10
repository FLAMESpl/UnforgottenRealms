using System;

namespace UnforgottenRealms.Controllers
{
    public class ExitController : ControllerBase
    {
        public override (Type, ControllerArguments) Run()
        {
            throw new InvalidOperationException();
        }
    }
}
