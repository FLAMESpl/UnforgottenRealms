using System;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.Controllers
{
    public partial class MenuController : ControllerBase
    {
        private GameWindow window;
        private Type nextController = null;
        private ControllerCreationArguments controllerArgs = null;
        private bool isRunning = true;

        public MenuController(ControllerCreationArguments args)
        {
            window = args.Window;
            window.Context = this;
            Initialize();
        }

        public override (Type, ControllerCreationArguments) Run()
        {
            window.RenderWindow.SetFramerateLimit(60);
            window.RenderCallback = x =>
            {
                x.DispatchEvents();
                x.Clear();
                x.Draw(activeContainer);
                x.Display();
            };

            while (isRunning && window.RenderWindow.IsOpen)
            {
                window.Perform();
            }
            
            return (typeof(ExitController), null);
        }

        private void Exit()
        {
            isRunning = false;
            nextController = typeof(ExitController);
            controllerArgs = null;
        }
    }
}
