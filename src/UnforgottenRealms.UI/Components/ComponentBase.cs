using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using UnforgottenRealms.Core.Input;
using UnforgottenRealms.UI.Containers;
using UnforgottenRealms.Core.Input.Events;

namespace UnforgottenRealms.UI.Components
{
    public abstract class ComponentBase : Drawable, 
        IHandle<MouseMoveEventArgs>, 
        IHandle<MouseButtonEventArgs>,
        IHandle<MouseEnteredRegionEventArgs>,
        IHandle<FocusChangedEventArgs>
    {
        public event EventHandler<MouseMoveEventArgs> MouseMoved;
        public event EventHandler<MouseButtonEventArgs> MouseClicked;
        public event EventHandler<MouseMoveEventArgs> MouseEntered;
        public event EventHandler<MouseMoveEventArgs> MouseLeft;
        public event EventHandler<FocusChangedEventArgs> FocusGained;
        public event EventHandler<FocusChangedEventArgs> FocusLost;

        public ComponentBase()
        {
            ComponentContainer.NullContainer.Add(this);
        }

        private ComponentContainer owningContainer;
        public ComponentContainer OwningContainer
        {
            get => owningContainer;
            set
            {
                owningContainer = value;
                Invalidate();
            }
        }

        public bool Enabled { get; set; } = true;
        public bool OverallEnabled => owningContainer.Enabled && Enabled;
        public bool Focused { get; set; } = false;
        public abstract Vector2f Position { get; set; }
        public abstract Vector2f Size { get; set; }

        public abstract void Invalidate();

        public abstract void Draw(RenderTarget target, RenderStates states);

        public abstract bool DoesApply(MouseMoveEventArgs eventArgs);

        public virtual void Trigger(MouseMoveEventArgs eventArgs) => MouseMoved?.Invoke(this, eventArgs);

        public abstract bool DoesApply(MouseButtonEventArgs eventArgs);

        public virtual void Trigger(MouseButtonEventArgs eventArgs) => MouseClicked?.Invoke(this, eventArgs);

        public virtual bool DoesApply(FocusChangedEventArgs eventArgs) => OverallEnabled;

        public virtual void Trigger(FocusChangedEventArgs eventArgs)
        {
            Focused = eventArgs.FocusedHandle == this;
            if (Focused)
                FocusGained?.Invoke(this, eventArgs);
            else
                FocusLost?.Invoke(this, eventArgs);
        }

        public virtual bool DoesApply(MouseEnteredRegionEventArgs eventArgs) => OverallEnabled;

        public virtual void Trigger(MouseEnteredRegionEventArgs eventArgs)
        {
            if (eventArgs.Region == this)
                MouseEntered?.Invoke(this, eventArgs.MouseState);
            else
                MouseLeft?.Invoke(this, eventArgs.MouseState);
        }
    }
}
