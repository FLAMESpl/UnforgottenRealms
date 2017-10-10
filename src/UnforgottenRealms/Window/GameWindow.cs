using SFML.Graphics;
using SFML.Window;
using System;
using UnforgottenRealms.Core;

namespace UnforgottenRealms.Window
{
    public class GameWindow : IDisposable, IRenderWindowProvider
    {
        public event EventHandler Recreated;

        private ContextSettings contextSettings;
        private VideoMode videoMode;
        private bool needsRecreating = false;

        private Action<RenderWindow> renderCallback;
        public Action<RenderWindow> RenderCallback
        {
            get => renderCallback;
            set => renderCallback = value ?? throw new InvalidOperationException("Render callback cannot be null");
        }

        public RenderWindow RenderWindow { get; private set; }

        public GameWindow()
        {
            renderCallback = x => { };
        }

        public void Perform()
        {
            RenderWindow.DispatchEvents();

            if (needsRecreating)
            {
                Initialize(videoMode, contextSettings);
                OnRecreate();
                needsRecreating = false;
            }

            RenderCallback.Invoke(RenderWindow);
        }

        public void Initialize(VideoMode videoMode, ContextSettings contextSettings)
        {
            DisposeRenderWindow();
            RenderWindow = new RenderWindow(videoMode, "UnforgottenRealms", Styles.Fullscreen, contextSettings);

            RenderWindow.Closed += (s, e) => RenderWindow.Close();
            RenderWindow.KeyPressed += (s, e) =>
            {
                switch (e.Code)
                {
                    case Keyboard.Key.F12:
                        Recreate(VideoMode.FullscreenModes[0]);
                        break;
                    case Keyboard.Key.F11:
                        Recreate(VideoMode.FullscreenModes[2]);
                        break;
                    case Keyboard.Key.F10:
                        Recreate(VideoMode.FullscreenModes[6]);
                        break;
                }
            };
        }

        public void Recreate(VideoMode videoMode) => Recreate(videoMode, contextSettings);

        public void Recreate(VideoMode videoMode, ContextSettings contextSettings)
        {
            this.contextSettings = contextSettings;
            this.videoMode = videoMode;
            needsRecreating = true;
        }

        public void Dispose()
        {
            DisposeRenderWindow();
        }

        private void DisposeRenderWindow()
        {
            if (RenderWindow != null)
            {
                RenderWindow.Close();
                RenderWindow.Dispose();
            }
        }

        private void OnRecreate() => Recreated?.Invoke(this, EventArgs.Empty);
    }
}
