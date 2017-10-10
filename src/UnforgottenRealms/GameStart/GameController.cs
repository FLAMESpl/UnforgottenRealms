using SFML.Graphics;
using System;
using UnforgottenRealms.Controllers;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.GameStart
{
    public class GameController : ControllerBase
    {
        private GameWindow window;

        public GameController(ControllerArguments args)
        {
            var gameArgs = args as GameControllerArguments;
            window = gameArgs.Window;
        }

        public override (Type, ControllerArguments) Run()
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
