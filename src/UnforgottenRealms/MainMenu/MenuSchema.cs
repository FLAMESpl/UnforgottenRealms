using SFML.Graphics;
using SFML.System;
using UnforgottenRealms.Core.Utils;
using UnforgottenRealms.UI.Components.Rectangle;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.ComponentSchemes
{
    public class MenuSchema
    {
        public Color HighlightedComponentColor { get; set; } = Color.Red;
        public Color IdleComponentColor { get; set; } = Color.Black;
        public uint FontSize { get; set; } = 24;
        public Color TextColor { get; set; } = Color.White;

        public int NavigationButtonHeight { get; set; } = 200;
        public Vector2f NavigationButtonSize { get; set; } = new Vector2f(128, 40);
        public float NavigationBarMargin { get; set; } = 50;

        private GameWindow window;

        public MenuSchema(GameWindow window)
        {
            this.window = window;
        }

        public Button CreateNavigationButton(int heightOrder, string text)
        {
            var button = CreateBaseComponent<Button>(text);
            button.Shape.Size = NavigationButtonSize;
            button.HighlightColor = HighlightedComponentColor;
            button.IdleColor = IdleComponentColor;
            button.Position = new Vector2f(0, heightOrder * NavigationButtonSize.Y + NavigationButtonHeight);
            return button;
        }

        public TextBox CreatePlayerNameTextBox(int heightOrder, string text)
        {
            var textBox = CreateBaseComponent<TextBox>(text);
            textBox.Shape.Size = NavigationButtonSize;
            textBox.IdleColor = IdleComponentColor;
            textBox.Position = new Vector2f(0, heightOrder * NavigationButtonSize.Y + NavigationButtonHeight);
            textBox.MaxLength = 16;
            return textBox;
        }

        public Label CreateLabel(int heightOrder, string text)
        {
            var label = CreateBaseComponent<Label>(text);
            label.Shape.Size = NavigationButtonSize;
            label.Shape.FillColor = IdleComponentColor;
            label.Position = new Vector2f(0, heightOrder * NavigationButtonSize.Y + NavigationButtonHeight);
            return label;
        }

        public Frame CreateSectionFrame()
        {
            var navigationBarLength = NavigationButtonSize.X + NavigationBarMargin;
            var renderWindow = window.RenderWindow;
            var verticalMargin = renderWindow.Size.Y * 0.05f;
            
            var frame = CreateBaseComponent<Frame>(string.Empty);
            frame.Position = new Vector2f(navigationBarLength, verticalMargin);
            frame.Size = new Vector2f(renderWindow.Size.X - navigationBarLength * 2, renderWindow.Size.Y - verticalMargin * 2);
            frame.Shape.FillColor = Color.Blue;

            return frame;
        }

        public T CreateBaseComponent<T>(string text) where T : RectangleComponentBase, new()
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
    }
}
