using System;

namespace UnforgottenRealms.Controllers
{
    public static class ControllersResolver
    {
        public static ControllerBase Get(Type controller, ControllerArguments args)
        {
            if (controller == typeof(ExitController))
                return null;

            if (!controller.IsSubclassOf(typeof(ControllerBase)))
                throw new InvalidOperationException($"Requested type does not derive from {nameof(ControllerBase)}");

            return controller.GetConstructor(new Type[] { typeof(ControllerArguments) }).Invoke(new Object[] { args }) as ControllerBase;
        }
    }
}

