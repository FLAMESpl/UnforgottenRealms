using System;
using System.Collections.Generic;
using System.Linq;

namespace UnforgottenRealms.Core.Input
{
    public class Layer
    {
        private Dictionary<Type, ICollection<object>> handles = new Dictionary<Type, ICollection<object>>();

        public void AddHandle<T>(IHandle<T> handle) where T : EventArgs
        {
            if (!handles.TryGetValue(typeof(T), out var handlesByType))
            {
                handlesByType = new List<object>();
                handles.Add(typeof(T), handlesByType);
            }

            handlesByType.Add(handle);
        }

        public IHandle<T> MatchHandle<T>(T eventArgs) where T : EventArgs
        {
            handles.TryGetValue(typeof(T), out var handlesByType);
            return handlesByType?.Cast<IHandle<T>>().FirstOrDefault(h => h.DoesApply(eventArgs));
        }
    }
}
