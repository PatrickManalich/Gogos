using System;
using UnityEngine;

namespace Gogos
{
    public enum PlayerColor { Red, Green, Blue }

    public class Player
    {
        public event Action PointsAdded;

        public string Name { get; }

        public PlayerColor PlayerColor { get; }

        public int Points { get; private set; }

        public Collection Collection { get; }

        public Player(string name, PlayerColor playerColor)
        {
            Name = name;
            PlayerColor = playerColor;
            Collection = new Collection();
        }

        public void AddPoints(int points)
        {
            Points += Mathf.Max(0, points);
            PointsAdded?.Invoke();
        }
    }
}
