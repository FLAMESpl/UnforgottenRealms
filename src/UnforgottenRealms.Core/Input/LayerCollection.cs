using System;
using System.Collections.ObjectModel;

namespace UnforgottenRealms.Core.Input
{
    public class LayerCollection : Collection<Layer>
    {
        public LayerCollection(int layers)
        {
            for (int i = 0; i < layers; i++)
            {
                Add(new Layer());
            }
        }

        public void Add() => new Layer();

        public IHandle<T> MatchHandle<T>(T eventArgs) where T : EventArgs
        {
            IHandle<T> handle = null;
            for (int i = 0; i < Count; i++)
            {
                handle = Items[i].MatchHandle(eventArgs);
                if (handle != null)
                    break;
            }
            return handle;
        }
    }
}
