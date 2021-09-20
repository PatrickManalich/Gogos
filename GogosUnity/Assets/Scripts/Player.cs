using System.Collections.Generic;

namespace Gogos
{
    public enum PlayerColor { Red, Green, Blue }

    public class Player
    {
        public string Name { get; }

        public PlayerColor PlayerColor { get; }

        public List<AbstractScriptableGogo> Collection { get; } = new List<AbstractScriptableGogo>();

        public Player(string name, PlayerColor playerColor)
        {
            Name = name;
            PlayerColor = playerColor;
        }
    }
}
