using SFML.Window;
using UnforgottenRealms.Core.Input;
using UnforgottenRealms.Window;
using UnforgottenRealms.Controllers;
using System;
using System.Linq;
using UnforgottenRealms.Core.Utils;
using UnforgottenRealms.MainMenu;
using UnforgottenRealms.GameStart;
using System.Diagnostics;

namespace UnforgottenRealms
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new ContextSettings
            {
                AntialiasingLevel = 8
            };

            VideoMode.FullscreenModes.ForEach(x => Console.WriteLine($"{x.Width}:{x.Height}"));
            var vm = VideoMode.FullscreenModes[0];
            using (var window = new GameWindow())
            {
                window.Initialize(vm, settings);

                var controllerType = typeof(MenuController);
                var controllerArgs = new ControllerArguments
                {
                    Window = window
                };

                ControllerBase controller = null;
                try
                {
                    while ((controller = ControllersResolver.Get(controllerType, controllerArgs)) != null)
                    {
                        (controllerType, controllerArgs) = controller.Run();
                        controller.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    controller?.Dispose();
                }
            }
        }
    }
}
