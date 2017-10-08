using SFML.Graphics;
using System;
using UnforgottenRealms.Core.Definitions;

namespace UnforgottenRealms.Core.Utils
{
    public static class ColorExtensions
    {
        public static Color ToRGB(this PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red: return Color.Red;
                case PlayerColor.Blue: return Color.Blue;
                case PlayerColor.Green: return Color.Green;
                case PlayerColor.Yellow: return Color.Yellow;
                default:
                    throw new ArgumentException($"No RGB mapping for {color} player color.");
            }
        }

        public static byte Dim(this byte saturation, int amount)
        {
            var temp = saturation - amount;
            return (byte)(temp < 0 ? 0 : temp);
        }

        public static Color Dim(this Color color, int amount)
        {
            return new Color(color.R.Dim(amount), color.G.Dim(amount), color.B.Dim(amount));
        }
    }
}
