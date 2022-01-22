using UnityEngine;

namespace Gogos
{
    public class LaunchPoint
    {
        public int Turn { get; }

        public Vector3 Position { get; }

        public LaunchPoint(int turn, Vector3 position)
        {
            Turn = turn;
            Position = position;
        }
    }
}
