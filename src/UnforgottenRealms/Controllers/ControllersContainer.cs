using System;
using System.Collections.Generic;

namespace UnforgottenRealms.Controllers
{
    public class ControllersContainer : Dictionary<Type, Func<ControllerCreationArguments, ControllerBase>>
    {
    }
}
