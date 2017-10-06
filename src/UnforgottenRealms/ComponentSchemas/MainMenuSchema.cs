using SFML.Graphics;
using SFML.System;
using UnforgottenRealms.Core.Utils;
using UnforgottenRealms.UI.Components.Rectangle;
using UnforgottenRealms.Window;

namespace UnforgottenRealms.ComponentSchemes
{
    public class MainMenuSchema
    {
        public Color HighlightedComponentColor { get; set; } = Color.Red;
        public Color IdleComponentColor { get; set; } = Color.Black;
        public uint FontSize { get; set; } = 24;
        public Color TextColor { get; set; } = Color.White;

        public int NavigationButtonHeight { get; set; } = 200;
        public Vector2f NavigationButtonSize { get; set; } = new Vector2f(128, 40);
        public float NavigationBarMargin { get; set; } = 50;

        private GameWindow window;

        public MainMenuSchema(GameWindow window)
        {
            this.window = window;
        }

        public Button CreateNavigationButton(int heightOrder, string text)
        {
            var button = CreateBaseComponent<Button>();
            button.Shape.Size = NavigationButtonSize;
            button.Text.DisplayedString = text;
            button.HighlightColor = HighlightedComponentColor;
            button.IdleColor = IdleComponentColor;
            button.Position = new Vector2f(0, heightOrder * NavigationButtonSize.Y + NavigationButtonHeight);
            return button;
        }

        public Frame CreateSectionFrame()
        {
            var navigationBarLength = NavigationButtonSize.X + NavigationBarMargin;
            var renderWindow = window.RenderWindow;
            var verticalMargin = renderWindow.Size.Y * 0.05f;
            
            var frame = CreateBaseComponent<Frame>();
            frame.Position = new Vector2f(navigationBarLength, verticalMargin);
            frame.Size = new Vector2f(renderWindow.Size.X - navigationBarLength * 2, renderWindow.Size.Y - verticalMargin * 2);
            frame.Shape.FillColor = Color.Blue;

            return frame;
        }

        private T CreateBaseComponent<T>() where T : RectangleComponentBase, new()
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
            return component;
        }
    }
}
