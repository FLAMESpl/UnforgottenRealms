using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnforgottenRealms.Controllers;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.GameStart
{
    public class GameController : ControllerBase
    {
        private GameWindow window;

        public GameController(ControllerCreationArguments args)
        {
            window = args.Window;
        }

        public override (Type, ControllerCreationArguments) Run()
        {
            window.RenderWindow.SetFramerateLimit(60);
            window.RenderCallback = Render;

            while (window.RenderWindow.IsOpen)
            {
                window.Perform();
            }

            return (typeof(ExitController), null);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        private void Render(RenderWindow renderWindow)
        {
            renderWindow.DispatchEvents();
            renderWindow.Clear();
            renderWindow.Display();
        }
    }
}
