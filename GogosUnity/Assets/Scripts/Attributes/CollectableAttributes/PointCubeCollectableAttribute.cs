using UnityEngine;

namespace Gogos
{
    public class PointCubeCollectableAttribute : AbstractCollectableAttribute
    {
        [SerializeField]
        private TierTrackerReference m_TierTrackerReference;

        protected override void Collect()
        {
            var pointValueTierTracker = (PointValueTierTracker)m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.PointValue);
            PlayerTracker.Player.AddPoints(pointValueTierTracker.PointValue);
        }
    }
}
