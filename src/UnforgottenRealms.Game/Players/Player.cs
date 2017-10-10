using UnforgottenRealms.Core.Definitions;

namespace UnforgottenRealms.Game.Players
{
    public class Player
    {
        public int Id { get; private set; }
        public PlayerColor Color { get; private set; }
        public string Name { get; private set; }

        public Player(int id, PlayerColor color, string name)
        {
            Id = id;
            Color = color;
            Name = name;
        }
    }
}
