using SFML.Window;
using UnforgottenRealms.Core.Input;
using UnforgottenRealms.Window;
using UnforgottenRealms.Controllers;
using System;

namespace UnforgottenRealms
{
    class Program
    {
        static void Main(string[] args)
        {
            var controllers = new ControllersContainer
            {
                [typeof(MenuController)] = x => new MenuController(x),
                [typeof(ExitController)] = null
            };

            var settings = new ContextSettings
            {
                AntialiasingLevel = 8
            };

            using (var window = new GameWindow(new VideoMode(640, 480), settings))
            {
                var inputProcessor = new InputProcessor(window, 1);
                var controllerType = typeof(MenuController);
                var controllerArgs = new ControllerCreationArguments
                {
                    Window = window
                };

                Func<ControllerCreationArguments, ControllerBase> controllerFactory;
                while ((controllerFactory = controllers[controllerType]) != null)
                {
                    ControllerBase controller = null;
                    try
                    {
                        controller = controllerFactory.Invoke(controllerArgs);
                        (controllerType, controllerArgs) = controller.Run();
                    }
                    finally
                    {
                        controller?.Dispose();
                    }
                }
            }
        }
    }
}
