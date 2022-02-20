using UnityEngine;

namespace Gogos
{
    public class LaunchPoint
    {
        public Vector3 Position { get; set; }

        public float TurnAngle { get; set; }

        public LaunchPoint(Vector3 position, float turnAngle)
        {
            Position = position;
            TurnAngle = turnAngle;
        }
    }
}
