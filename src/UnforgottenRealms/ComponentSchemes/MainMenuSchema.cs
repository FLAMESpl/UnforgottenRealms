using SFML.Graphics;
using SFML.System;
using UnforgottenRealms.Core.Utils;
using UnforgottenRealms.UI.Components.Rectangle;

namespace UnforgottenRealms.ComponentSchemes
{
    public class MainMenuSchema
    {
        public Color HighlightedComponentColor { get; set; } = Color.Red;
        public Color IdleComponentColor { get; set; } = Color.Black;
        public uint FontSize { get; set; } = 24;
        public Color TextColor { get; set; } = Color.White;

        public int NavigationButtonHeight { get; set; }
        public Vector2f NavigationButtonSize { get; set; } = new Vector2f(128, 40);

        public Button CreateNavigationButton(int heightOrder, string text)
        {
            var button = new Button();
            SetBaseValues(button);
            button.Text.DisplayedString = text;
            button.HighlightColor = HighlightedComponentColor;
            button.IdleColor = IdleComponentColor;
            button.Position = new Vector2f(0, heightOrder * NavigationButtonSize.Y + NavigationButtonHeight);
            return button;
        }

        private void SetBaseValues<T>(T component) where T : RectangleComponentBase
        {
            component.Shape = new RectangleShape
            {
                FillColor = IdleComponentColor,
                Size = NavigationButtonSize
            };
            component.Text = new Text
            {
                Font = FontExtensions.Arial,
                FillColor = TextColor,
                CharacterSize = FontSize
            };
            component.TextPosition = new Vector2f(4, 4);
        }
    }
}
