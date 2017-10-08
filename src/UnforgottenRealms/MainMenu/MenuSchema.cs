using SFML.Graphics;
using SFML.System;
using System;
using UnforgottenRealms.Core.Utils;
using UnforgottenRealms.UI.Components.Rectangle;
using UnforgottenRealms.UI.Components.Rectangle.Extended;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.ComponentSchemes
{
    public class MenuSchema
    {
        public Color HighlightedComponentColor { get; set; } = Color.Red;
        public Color IdleComponentColor { get; set; } = Color.Black;
        public uint FontSize { get; set; } = 24;
        public Color TextColor { get; set; } = Color.White;

        public float NavigationButtonHeight { get; set; } = 200f;
        public Vector2f NavigationButtonSize { get; set; } = new Vector2f(128, 40);
        public float NavigationBarMargin { get; set; } = 50f;
        public Vector2f PlayerNameTextBoxSize { get; set; } = new Vector2f(256, 40);
        public Vector2f SettingLabelSize { get; set; } = new Vector2f(172, 40);
        public Vector2f SettingButtonSize { get; set; } = new Vector2f(40, 40);

        private GameWindow window;

        public MenuSchema(GameWindow window)
        {
            this.window = window;
        }

        public Button CreateNavigationButton(int heightOrder, string text)
        {
            var button = CreateBaseComponent<Button>(text, NavigationButtonSize, heightOrder, NavigationButtonHeight);
            button.HighlightColor = HighlightedComponentColor;
            button.IdleColor = IdleComponentColor;
            return button;
        }

        public TextBox CreatePlayerNameTextBox(int heightOrder, string text)
        {
            var textBox = CreateBaseComponent<TextBox>(text, PlayerNameTextBoxSize, heightOrder, NavigationButtonHeight);
            textBox.IdleColor = IdleComponentColor;
            textBox.HighlightColor = HighlightedComponentColor;
            textBox.MaxLength = 16;
            return textBox;
        }

        public Button CreatePlayerColorButton(int heightOrder, Action<StateSelectButton>[] states)
        {
            var button = CreateBaseComponent<StateSelectButton>(String.Empty, SettingButtonSize, heightOrder, NavigationButtonHeight, new Vector2f(PlayerNameTextBoxSize.X, 0));
            button.States = states;
            return button;
        }

        public Label CreateLabel(int heightOrder, string text)
        {
            var label = CreateBaseComponent<Label>(text, NavigationButtonSize, heightOrder, NavigationButtonHeight);
            return label;
        }

        public Frame CreateSectionFrame()
        {
            var navigationBarLength = NavigationButtonSize.X + NavigationBarMargin;
            var renderWindow = window.RenderWindow;
            var verticalMargin = renderWindow.Size.Y * 0.05f;
            
            var frame = CreateBaseComponent<Frame>();
            frame.Position = new Vector2f(navigationBarLength, verticalMargin);
            frame.Size = new Vector2f(renderWindow.Size.X - navigationBarLength * 2, renderWindow.Size.Y - verticalMargin * 2);
            frame.Shape.FillColor = Color.Cyan;

            return frame;
        }

        public Label CreateSettingLabel(int heightOrder, string text)
        {
            var label = CreateBaseComponent<Label>(text, SettingLabelSize, heightOrder, NavigationButtonHeight);
            return label;
        }

        public Button CreateSettingButton(int heightOrder, string text, float width)
        {
            var button = CreateBaseComponent<Button>(text, new Vector2f(width, NavigationButtonSize.Y), heightOrder, NavigationButtonHeight, new Vector2f(SettingLabelSize.X, 0));
            button.HighlightColor = HighlightedComponentColor;
            button.IdleColor = IdleComponentColor;
            return button;
        }

        public Button CreateSettingSquareButton(int heightOrder, Action<StateSelectButton>[] states)
        {
            var button = CreateBaseComponent<StateSelectButton>("", SettingButtonSize, heightOrder, NavigationButtonHeight, new Vector2f(SettingLabelSize.X, 0));
            button.HighlightColor = HighlightedComponentColor;
            button.IdleColor = IdleComponentColor;
            button.States = states;
            return button;
        }

        public T CreateBaseComponent<T>(string text = "") where T : RectangleComponentBase, new()
        {
            var component = new T();
            component.Shape = new RectangleShape
            {
                FillColor = IdleComponentColor
            };
            component.Text = new Text
            {
                Font = FontExtensions.Arial,
                FillColor = TextColor,
                CharacterSize = FontSize
            };
            component.TextPosition = new Vector2f(4, 4);
            component.Text.DisplayedString = text;
            return component;
        }

        public T CreateBaseComponent<T>(string text, Vector2f size, int heightOrder, float startingHeight, Vector2f? margin = null) where T : RectangleComponentBase, new()
        {
            var component = CreateBaseComponent<T>(text);
            component.Size = size;
            component.Position = new Vector2f(margin?.X ?? 0, heightOrder * (size.Y + (margin?.Y ?? 0)) + startingHeight);
            return component;
        }
    }
}
