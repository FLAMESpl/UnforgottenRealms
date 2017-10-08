namespace UnforgottenRealms.Core.Definitions
{
    public enum PlayerColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }

    public static class PlayerColorDefinitions
    {
        public static PlayerColor[] HumanPlayerColors => new PlayerColor[] { PlayerColor.Red, PlayerColor.Blue, PlayerColor.Green, PlayerColor.Yellow };
    }
}
