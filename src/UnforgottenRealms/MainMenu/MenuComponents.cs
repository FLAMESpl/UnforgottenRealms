using SFML.Graphics;
using SFML.Window;
using System;
using System.Linq;
using UnforgottenRealms.ComponentSchemes;
using UnforgottenRealms.Core.Definitions;
using UnforgottenRealms.Core.Input;
using UnforgottenRealms.Core.Input.Events;
using UnforgottenRealms.Core.Utils;
using UnforgottenRealms.UI.Components.Rectangle;
using UnforgottenRealms.UI.Components.Rectangle.Extended;
using UnforgottenRealms.UI.Containers;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.MainMenu
{
    public class MenuComponents : Drawable, IWindowContext
    {
        public event EventHandler ExitRequested;
        public event EventHandler StartRequested;

        private GameWindow window;
        private InputProcessor inputProcessor;
        private ComponentContainer activeContainer;
        private ComponentContainer homeComponents;
        private ComponentContainer optionsComponents;
        private ComponentContainer playComponents;

        public MenuComponents(GameWindow window)
        {
            this.window = window;
        }

        public void Initialize()
        {
            var HOME_LAYER = 0;
            var PLAY_LAYER = 1;
            var OPTIONS_LAYER = 2;
            var schema = new MenuSchema(window);

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
            exitButton.MouseClicked += (s, e) => OnExitRequest();
            homeComponents.Add(exitButton);

            var backFromOptionsButton = schema.CreateNavigationButton(2, "Back");
            RegisterButton(backFromOptionsButton, OPTIONS_LAYER);
            backFromOptionsButton.MouseClicked += EventHandlerToChangeLayer(HOME_LAYER, homeComponents);
            optionsComponents.Add(backFromOptionsButton);

            var optionsFrame = schema.CreateSectionFrame();
            optionsComponents.Add(optionsFrame);

            var startButton = schema.CreateNavigationButton(0, "Start");
            RegisterButton(startButton, PLAY_LAYER);
            startButton.MouseClicked += (s, e) => OnStartRequest();
            playComponents.Add(startButton);

            var backFromPlayButton = schema.CreateNavigationButton(2, "Back");
            RegisterButton(backFromPlayButton, PLAY_LAYER);
            backFromPlayButton.MouseClicked += EventHandlerToChangeLayer(HOME_LAYER, homeComponents);
            playComponents.Add(backFromPlayButton);

            var playFrame = schema.CreateSectionFrame();
            playComponents.Add(playFrame);

            var levelLabel = schema.CreateSettingLabel(0, "Level:");
            playFrame.Components.Add(levelLabel);

            var levelButton = schema.CreateSettingButton(0, "SELECT LEVEL", 256);
            RegisterButton(levelButton, PLAY_LAYER);
            playFrame.Components.Add(levelButton);

            var playersCountLabel = schema.CreateSettingLabel(2, "Players count:");
            playFrame.Components.Add(playersCountLabel);

            var playersCountButton = schema.CreateSettingSquareButton(2, CreatePlayerCountStates(4));
            RegisterButton(playersCountButton, PLAY_LAYER);
            playFrame.Components.Add(playersCountButton);

            for (int i = 1; i <= 4; i++)
            {
                var playerNameTextBox = schema.CreatePlayerNameTextBox(i + 2, $"PLAYER {i}");
                RegisterTextBox(playerNameTextBox, PLAY_LAYER);
                playFrame.Components.Add(playerNameTextBox);

                var playerColorButton = schema.CreatePlayerColorButton(i + 2, CreatePlayerColorStates(PlayerColorDefinitions.HumanPlayerColors));
                RegisterButton(playerColorButton, PLAY_LAYER);
                playFrame.Components.Add(playerColorButton);
            }

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

            void RegisterTextBox(TextBox textBox, int layerIndex)
            {
                var layer = inputProcessor.Layers[layerIndex];
                layer.AddHandle<MouseButtonEventArgs>(textBox);
                layer.AddHandle<TextEventArgs>(textBox);
            }

            Action<StateSelectButton>[] CreatePlayerCountStates(int maxPlayers)
            {
                var states = new Action<StateSelectButton>[maxPlayers - 1];
                for (int i = 2; i <= maxPlayers; i++)
                {
                    var playerNumber = i.ToString();
                    states[i - 2] = button =>
                    {
                        if (button.Text != null)
                            button.Text.DisplayedString = playerNumber;
                    };
                }
                return states;
            }

            Action<StateSelectButton>[] CreatePlayerColorStates(params PlayerColor[] colors)
            {;
                return colors.Select<PlayerColor, Action<StateSelectButton>>(color =>
                {
                    var idleColor = color.ToRGB();
                    var highlightColor = idleColor.Dim(50);
                    return (StateSelectButton button) =>
                    {
                        if (button.Shape != null)
                        {
                            button.IdleColor = idleColor;
                            button.HighlightColor = highlightColor;
                        }
                    };
                }
                ).ToArray();
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

        public void Draw(RenderTarget target, RenderStates states) => target.Draw(activeContainer, states);

        private void OnExitRequest() => ExitRequested?.Invoke(this, EventArgs.Empty);

        private void OnStartRequest() => StartRequested?.Invoke(this, EventArgs.Empty);
    }
}
