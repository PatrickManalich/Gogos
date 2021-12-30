using UnityEngine;

namespace Gogos
{
    public class PointCubeCollectedEffect : AbstractCollectedEffect
    {
        [SerializeField]
        private PointValueTierTracker m_PointValueTierTracker;

        protected override void OnCollected()
        {
            PlayerTracker.Player.AddPoints(m_PointValueTierTracker.PointValue);
        }
    }
}
