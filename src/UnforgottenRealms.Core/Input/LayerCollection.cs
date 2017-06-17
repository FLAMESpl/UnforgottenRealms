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

        public InputHandle MatchHandle(int x, int y)
        {
            InputHandle handle = null;
            for (int i = 0; i < Count; i++)
            {
                handle = Items[i].MatchHandle(x, y);
                if (handle != null)
                    break;
            }
            return handle;
        }
    }
}
