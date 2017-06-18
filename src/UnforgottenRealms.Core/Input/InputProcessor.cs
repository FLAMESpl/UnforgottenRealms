using SFML.Graphics;
using SFML.Window;
using System.Threading.Tasks;

namespace UnforgottenRealms.Core.Input
{
    public class InputProcessor
    {
        public LayerCollection Layers { get; }

        private RenderWindow window;

        public InputProcessor(RenderWindow window, int layersCount = 0)
        {
            this.window = window;
            Layers = new LayerCollection(layersCount);

            window.MouseButtonPressed += ProcessMouseButtonPress;
            window.MouseMoved += ProcessMouseMovement;
        }

        private void ProcessMouseButtonPress(object sender, MouseButtonEventArgs e)
        {        }

        private void ProcessMouseMovement(object sender, MouseMoveEventArgs e)
        {
            Layers.MatchHandle(e.X, e.Y)?.Trigger();
        }
    }
}
