using SFML.Graphics;
using SFML.Window;

namespace UnforgottenRealms.Window
{
    public class GameWindow : RenderWindow
    {
        public GameWindow(VideoMode videoMode, ContextSettings settings) : base(videoMode, "UnforgottenRealms", Styles.Default, settings)
        {
            Closed += (s, e) => Close();
        }
    }
}
