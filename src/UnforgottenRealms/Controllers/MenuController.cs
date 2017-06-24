using System;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.Controllers
{
    public class MenuController : ControllerBase
    {
        private GameWindow window;

        public MenuController(ControllerCreationArguments args)
        {
            window = args.Window;
        }

        public override (Type, ControllerCreationArguments) Run()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();
                window.Display();
            }
            
            return (typeof(ExitController), null);
        }
    }
}
