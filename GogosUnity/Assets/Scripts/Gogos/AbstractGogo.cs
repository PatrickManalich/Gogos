using UnityEngine;

namespace Gogos
{
    public abstract class AbstractGogo : MonoBehaviour
    {
        public Player Player { get; private set; }

        public IdentifiableGogo IdentifiableGogo { get; private set; }

        [SerializeField]
        protected TierTrackerReference m_TierTrackerReference;

        [SerializeField]
        private Accelerometer m_Accelerometer;

        protected abstract void OnStartedMoving();

        protected abstract void OnStoppedMoving();

        protected virtual void Start()
        {
            Player = PlayerTracker.Player;
            SubscribeToMovementEvents();
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeFromMovementEvents();
        }

        public virtual void SetTiers(IdentifiableGogo identifiableGogo)
        {
            IdentifiableGogo = identifiableGogo;

            var scriptableGogo = IdentifiableGogo.ScriptableGogo;
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
