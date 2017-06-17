using System.Collections.Generic;
using System.Linq;

namespace UnforgottenRealms.Core.Input
{
    public class Layer
    {
        public ICollection<InputHandle> Handles { get; } = new List<InputHandle>();

        public InputHandle MatchHandle(int x, int y) => Handles.FirstOrDefault(h => h.ContainsMouse(x, y));
    }
}
