using SFML.Graphics;
using System;
using UnforgottenRealms.Controllers;
using UnforgottenRealms.GameStart;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.MainMenu
{
    public partial class MenuController : ControllerBase
    {
        private GameWindow window;
        private Type nextController = null;
        private ControllerCreationArguments controllerArgs = null;
        private MenuComponents components;
        private bool isRunning = true;
        private bool isDisposing = false;

        public MenuController(ControllerCreationArguments args)
        {
            window = args.Window;
            components = new MenuComponents(window);
            window.Context = components;
            components.Initialize();
            components.ExitRequested += (s, e) => Exit();
            components.StartRequested += (s, e) => Play();
        }

        public override (Type, ControllerCreationArguments) Run()
        {
            window.RenderWindow.SetFramerateLimit(60);
            window.RenderCallback = Render;

            while (isRunning && window.RenderWindow.IsOpen)
            {
                window.Perform();
            }
            
            return (nextController, controllerArgs);
        }

        public override void Dispose()
        {
            if (isDisposing)
                return;

            components.Clear();
        }

        private void Render(RenderWindow renderWindow)
        {
            renderWindow.DispatchEvents();
            renderWindow.Clear();
            renderWindow.Draw(components);
            renderWindow.Display();
        }

        private void Exit()
        {
            isRunning = false;
            nextController = typeof(ExitController);
            controllerArgs = null;
        }

        private void Play()
        {
            isRunning = false;
            nextController = typeof(GameController);
            controllerArgs = new ControllerCreationArguments
            {
                Window = window
            };
        }
    }
}
