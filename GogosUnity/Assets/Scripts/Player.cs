using System;
using UnityEngine;

namespace Gogos
{
    public enum PlayerColor { Red, Green, Blue }

    public class Player
    {
        public string Name { get; }

        public PlayerColor PlayerColor { get; }

        public Collection Collection { get; }

        public Player(string name, PlayerColor playerColor)
        {
            Name = name;
            PlayerColor = playerColor;
            Collection = new Collection();
        }
    }
}
