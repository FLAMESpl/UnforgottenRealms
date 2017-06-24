using SFML.System;

namespace UnforgottenRealms.Core.Utils
{
    public static class VectorExtensions
    {
        public static Vector2f SetX(this Vector2f v, float x) => new Vector2f(x, v.Y);

        public static Vector2f SetY(this Vector2f v, float y) => new Vector2f(v.X, y);
    }
}
