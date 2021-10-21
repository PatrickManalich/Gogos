namespace Gogos
{
    public class PointCubeCollectableAttribute : AbstractCollectableAttribute
    {
        protected override void Collect()
        {
            var pointValueTierTracker = (PointValueTierTracker)m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.PointValue);
            PlayerTracker.Player.AddPoints(pointValueTierTracker.PointValue);
        }
    }
}
