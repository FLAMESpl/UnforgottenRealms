using System;

namespace UnforgottenRealms.Controllers
{
    public static class ControllersResolver
    {
        public static ControllerBase Get(Type controller, ControllerCreationArguments args)
        {
            if (controller == typeof(ExitController))
                return null;

            if (!controller.IsSubclassOf(typeof(ControllerBase)))
                throw new InvalidOperationException($"Requested type does not derive from {nameof(ControllerBase)}");

            return controller.GetConstructor(new Type[] { typeof(ControllerCreationArguments) }).Invoke(new Object[] { args }) as ControllerBase;
        }
    }
}

