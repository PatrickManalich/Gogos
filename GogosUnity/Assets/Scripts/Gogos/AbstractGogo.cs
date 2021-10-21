using UnityEngine;

namespace Gogos
{
    public abstract class AbstractGogo : MonoBehaviour
    {
        public Player Player { get; private set; }

        public AbstractScriptableGogo ScriptableGogo { get; private set; }

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

        public virtual void SetTiers(AbstractScriptableGogo scriptableGogo)
        {
            ScriptableGogo = scriptableGogo;
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Rarity).SetTier((int)ScriptableGogo.RarityTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Weight).SetTier((int)ScriptableGogo.WeightTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.PointValue).SetTier((int)ScriptableGogo.PointValueTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range).SetTier((int)ScriptableGogo.RangeTier);
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
