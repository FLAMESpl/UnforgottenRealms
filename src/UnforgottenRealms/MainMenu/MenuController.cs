using SFML.Graphics;
using System;
using System.Collections.Generic;
using UnforgottenRealms.Controllers;
using UnforgottenRealms.GameStart;
using UnforgottenRealms.MainMenu.Settings;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.MainMenu
{
    public partial class MenuController : ControllerBase
    {
        private GameWindow window;
        private Type nextController = null;
        private ControllerArguments controllerArgs = null;
        private MenuComponents components;
        private bool isRunning = true;
        private bool isDisposing = false;

        private List<PlayerInfo> players = new List<PlayerInfo>();

        public MenuController(ControllerArguments args)
        {
            window = args.Window;
            window.Recreated += (s, e) => RecreateWindow();
            components = new MenuComponents(window);
            components.Initialize();
            components.ExitRequested += (s, e) => Exit();
            components.StartRequested += (s, e) => Play();
        }

        public override (Type, ControllerArguments) Run()
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

        private void RecreateWindow()
        {

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
            controllerArgs = new ControllerArguments
            {
                Window = window
            };
        }
    }
}
