using System;
using System.Collections;
using System.Collections.Generic;
using UnforgottenRealms.UI.Components;

namespace UnforgottenRealms.UI.Containers
{
    public class ComponentsCollection : ICollection<ComponentBase>
    {
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => false;

        public event EventHandler<ComponentBase> ComponentAdded;
        public event EventHandler<ComponentBase> ComponentRemoved;

        public void Add(ComponentBase item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ComponentBase item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ComponentBase[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ComponentBase> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(ComponentBase item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
