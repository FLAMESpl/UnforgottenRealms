using SFML.Graphics;
using SFML.Window;
using UnforgottenRealms.Core.Input.Events;

namespace UnforgottenRealms.Core.Input
{
    public class InputProcessor
    {
        public LayerCollection Layers { get; }

        private IHandle<MouseButtonEventArgs> focusedHandle = null;
        private IHandle<MouseMoveEventArgs> handleContainingMouse = null;
        private RenderWindow window;

        public InputProcessor(RenderWindow window, int layersCount = 0)
        {
            this.window = window;
            Layers = new LayerCollection(layersCount);

            window.MouseButtonPressed += ProcessMouseButtonPress;
            window.MouseMoved += ProcessMouseMovement;
            window.TextEntered += ProcessTextInput;
        }

        private void ProcessMouseButtonPress(object sender, MouseButtonEventArgs e)
        {
            var handle = Layers.MatchHandle(e);
            handle?.Trigger(e);
            if (focusedHandle != handle)
            {
                var @event = new FocusChangedEventArgs(handle);
                if (focusedHandle != null)
                    (focusedHandle as IHandle<FocusChangedEventArgs>).Trigger(@event);

                if (handle != null)
                    (handle as IHandle<FocusChangedEventArgs>).Trigger(@event);

                focusedHandle = handle;
            }
        }

        private void ProcessMouseMovement(object sender, MouseMoveEventArgs e)
        {
            var handle = Layers.MatchHandle(e);
            handle?.Trigger(e);
            if (handleContainingMouse != handle)
            {
                var @event = new MouseEnteredRegionEventArgs(e, handle);
                if (handleContainingMouse != null)
                    (handleContainingMouse as IHandle<MouseEnteredRegionEventArgs>).Trigger(@event);

                if (handle != null)
                    (handle as IHandle<MouseEnteredRegionEventArgs>).Trigger(@event);

                handleContainingMouse = handle;
            }
        }

        private void ProcessTextInput(object sender, TextEventArgs e)
        {
            var handle = Layers.MatchHandle(e);
            handle?.Trigger(e);
        }
    }
}
