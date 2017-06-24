using SFML.Graphics;
using SFML.System;
using System.Collections;
using System.Collections.Generic;
using UnforgottenRealms.UI.Components;
using System;

namespace UnforgottenRealms.UI.Containers
{
    public class ComponentContainer : ICollection<ComponentBase>, Drawable
    {
        public event EventHandler<ComponentBase> ComponentAdded;
        public event EventHandler<ComponentBase> ComponentRemoved;

        private List<ComponentBase> items = new List<ComponentBase>();

        public ComponentBase this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public int Count => items.Count;
        public bool Enabled { get; set; } = true;
        public bool IsReadOnly => false;

        protected Vector2f position;
        public virtual Vector2f Position
        {
            get => position;
            set
            {
                position = value;
                foreach (var item in items)
                    item.Invalidate();
            }
        }

        public void Add(ComponentBase item)
        {
            item.OwningContainer?.items.Remove(item);
            item.OwningContainer = this;
            items.Add(item);
            OnComponentAdded(item);
        }

        public void Clear()
        {
            foreach (var component in items)
                component.OwningContainer = null;
            items.Clear();
        }

        public bool Contains(ComponentBase item) => items.Contains(item);

        public void CopyTo(ComponentBase[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        public IEnumerator<ComponentBase> GetEnumerator() => items.GetEnumerator();

        public bool Remove(ComponentBase item)
        {
            var contains = items.Remove(item);
            if (contains)
            {
                item.OwningContainer = NullContainer;
                OnComponentRemoved(item);
            }
            return contains;
        }

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (Enabled)
                foreach (var component in items)
                    target.Draw(component, states);
        }

        protected virtual void OnComponentAdded(ComponentBase component) => ComponentAdded?.Invoke(this, component);

        protected virtual void OnComponentRemoved(ComponentBase component) => ComponentRemoved?.Invoke(this, component);

        public static ComponentContainer NullContainer { get; } = new NullComponentContainer();
    }
}
