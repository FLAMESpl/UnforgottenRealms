using SFML.Window;
using System;
using UnforgottenRealms.ComponentSchemes;
using UnforgottenRealms.Core.Input;
using UnforgottenRealms.Core.Input.Events;
using UnforgottenRealms.UI.Components.Rectangle;
using UnforgottenRealms.UI.Containers;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.Controllers
{
    public partial class MenuController : IWindowContext
    {
        private InputProcessor inputProcessor;
        private ComponentContainer activeContainer;
        private ComponentContainer homeComponents;
        private ComponentContainer optionsComponents;
        private ComponentContainer playComponents;

        public void Initialize()
        {
            var HOME_LAYER = 0;
            var PLAY_LAYER = 1;
            var OPTIONS_LAYER = 2;
            var schema = new MainMenuSchema(window);

            homeComponents = new ComponentContainer();
            optionsComponents = new ComponentContainer();
            playComponents = new ComponentContainer();
            inputProcessor = new InputProcessor(window.RenderWindow, 3);
            
            var playButton = schema.CreateNavigationButton(0, "Play");
            RegisterButton(playButton, HOME_LAYER);
            playButton.MouseClicked += EventHandlerToChangeLayer(PLAY_LAYER, playComponents);
            homeComponents.Add(playButton);

            var optionsButton = schema.CreateNavigationButton(1, "Options");
            RegisterButton(optionsButton, HOME_LAYER);
            optionsButton.MouseClicked += EventHandlerToChangeLayer(OPTIONS_LAYER, optionsComponents);
            homeComponents.Add(optionsButton);

            var exitButton = schema.CreateNavigationButton(2, "Exit");
            RegisterButton(exitButton, HOME_LAYER);
            exitButton.MouseClicked += (s, e) => Exit();
            homeComponents.Add(exitButton);

            var backFromOptionsButton = schema.CreateNavigationButton(2, "Back");
            RegisterButton(backFromOptionsButton, OPTIONS_LAYER);
            backFromOptionsButton.MouseClicked += EventHandlerToChangeLayer(HOME_LAYER, homeComponents);
            optionsComponents.Add(backFromOptionsButton);

            var optionsFrame = schema.CreateSectionFrame();
            optionsComponents.Add(optionsFrame);

            var buttona = schema.CreateNavigationButton(0, "AA");
            RegisterButton(buttona, OPTIONS_LAYER);
            buttona.MouseClicked += EventHandlerToChangeLayer(HOME_LAYER, homeComponents);
            optionsFrame.Components.Add(buttona);

            var backFromPlayButton = schema.CreateNavigationButton(2, "Back");
            RegisterButton(backFromPlayButton, PLAY_LAYER);
            backFromPlayButton.MouseClicked += EventHandlerToChangeLayer(HOME_LAYER, homeComponents);
            playComponents.Add(backFromPlayButton);

            activeContainer = homeComponents;
            inputProcessor.Layers[HOME_LAYER].Enabled = true;
            inputProcessor.Layers[PLAY_LAYER].Enabled = false;
            inputProcessor.Layers[OPTIONS_LAYER].Enabled = false;

            void RegisterButton(Button button, int layerIndex)
            {
                var layer = inputProcessor.Layers[layerIndex];
                layer.AddHandle<MouseButtonEventArgs>(button);
                layer.AddHandle<MouseMoveEventArgs>(button);
                layer.AddHandle<MouseEnteredRegionEventArgs>(button);
            }

            EventHandler<MouseButtonEventArgs> EventHandlerToChangeLayer(int layer, ComponentContainer container)
            {
                return (s, e) =>
                {
                    inputProcessor.Layers.ActivateSingle(layer);
                    activeContainer.Enabled = false;
                    container.Enabled = true;
                    activeContainer = container;
                };
            }
        }

        public void Clear()
        {

        }
    }
}
