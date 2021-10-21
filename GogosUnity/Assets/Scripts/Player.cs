using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public enum PlayerColor { Red, Green, Blue }

    public class Player
    {
        public string Name { get; }

        public PlayerColor PlayerColor { get; }

        public int Points { get; private set; }

        public List<AbstractScriptableGogo> Collection { get; } = new List<AbstractScriptableGogo>();

        public Player(string name, PlayerColor playerColor)
        {
            Name = name;
            PlayerColor = playerColor;
        }

        public void AddPoints(int points)
        {
            Points += Mathf.Max(0, points);
        }
    }
}
