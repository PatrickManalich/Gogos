using UnityEngine;

namespace Gogos
{
	public abstract class AbstractGogo : MonoBehaviour
	{
        [SerializeField]
        protected TierTrackerReference m_TierTrackerReference;

        [SerializeField]
		private Accelerometer m_Accelerometer;

        protected abstract void OnStartedMoving();
        protected abstract void OnStoppedMoving();

        protected virtual void Start()
        {
            SubscribeToMovementEvents();
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeFromMovementEvents();
        }

        protected virtual void SetTiers(AbstractScriptableGogo scriptableGogo)
        {
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Rarity).SetTier((int)scriptableGogo.RarityTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Weight).SetTier((int)scriptableGogo.WeightTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.PointValue).SetTier((int)scriptableGogo.PointValueTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range).SetTier((int)scriptableGogo.RangeTier);
        }

        protected void SubscribeToMovementEvents()
        {
            m_Accelerometer.StartedMoving += Accelerometer_OnStartedMoving;
            m_Accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;
        }

        protected void UnsubscribeFromMovementEvents()
        {
            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
            m_Accelerometer.StartedMoving -= Accelerometer_OnStartedMoving;
        }

        private void Accelerometer_OnStartedMoving()
        {
            OnStartedMoving();
        }

        private void Accelerometer_OnStoppedMoving()
        {
            OnStoppedMoving();
        }
    }
}