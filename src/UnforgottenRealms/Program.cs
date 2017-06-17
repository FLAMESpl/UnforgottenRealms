using SFML.Graphics;
using SFML.Window;
using System;
using UnforgottenRealms.Core.Input;
using UnforgottenRealms.Window;

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

            var window = new GameWindow(new VideoMode(640, 480), settings);
            var inputProcessor = new InputProcessor(window, 1);

            while (window.IsOpen())
            {
                window.DispatchEvents();
                window.Clear(new Color(100, 100, 200));
                window.Display();
            }
        }
    }
}
