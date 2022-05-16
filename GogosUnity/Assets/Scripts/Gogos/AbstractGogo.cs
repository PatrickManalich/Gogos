using UnityEngine;

namespace Gogos
{
    public abstract class AbstractGogo : MonoBehaviour
    {
        public Player Player { get; private set; }

        public IdentifiableGogo IdentifiableGogo { get; private set; }

        public AttributesReference AttributesReference => m_AttributesReference;

        public TierTrackerReference TierTrackerReference => m_TierTrackerReference;

        protected GameObject TriggerRange => m_TriggerRange;

        [SerializeField]
        private AttributesReference m_AttributesReference;

        [SerializeField]
        private TierTrackerReference m_TierTrackerReference;

        [SerializeField]
        private Accelerometer m_Accelerometer;

        [SerializeField]
        private GameObject m_TriggerRange;

        private Transform m_TriggerRangeParent;

        protected abstract void OnStartedMoving();

        protected abstract void OnStoppedMoving();

        protected virtual void Start()
        {
            m_TriggerRangeParent = m_TriggerRange.transform.parent;

            SubscribeToMovementEvents();
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeFromMovementEvents();
            if (m_TriggerRange != null)
            {
                Destroy(m_TriggerRange);
            }
        }

        public virtual void SetPlayer(Player player)
        {
            Player = player;
            foreach (var attribute in m_AttributesReference.Attributes)
            {
                attribute.Player = Player;
            }
        }

        public virtual void SetTiers(IdentifiableGogo identifiableGogo)
        {
            IdentifiableGogo = identifiableGogo;

            var scriptableGogo = IdentifiableGogo.ScriptableGogo;
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Rarity).SetTier((int)scriptableGogo.RarityTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Weight).SetTier((int)scriptableGogo.WeightTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.PointValue).SetTier((int)scriptableGogo.PointValueTier);
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.GemValue).SetTier((int)scriptableGogo.GemValueTier);
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

        protected void UnparentTriggerRange()
        {
            m_TriggerRange.transform.parent = null;
        }

        protected void ReparentTriggerRange()
        {
            m_TriggerRange.transform.parent = m_TriggerRangeParent;
            m_TriggerRange.transform.localPosition = Vector3.zero;
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
