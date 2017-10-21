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
        private bool isDisposed = false;

        private List<PlayerInfo> players = new List<PlayerInfo>();

        public MenuController(ControllerArguments args)
        {
            window = args.Window;
            window.Recreated += HandleWindowRecreate;
            components = new MenuComponents(window);
            components.Initialize();
            components.ExitRequested += HandleExitRequest;
            components.StartRequested += HandleStartRequest;
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
            if (isDisposed)
                return;

            components.Clear();
            isDisposed = true;
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

        private void HandleWindowRecreate(object sender, EventArgs e) => RecreateWindow();
        private void HandleExitRequest(object sender, EventArgs e) => Exit();
        private void HandleStartRequest(object sender, EventArgs e) => Play();
    }
}
