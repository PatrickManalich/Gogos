using UnityEngine;

namespace Gogos
{
    public class Checkpoint : MonoBehaviour
    {
        public Player Player { get; private set; }

        public int TurnReached { get; private set; }

        public bool IsStartingCheckpoint => m_IsStartingCheckpoint;

        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private GameObject m_CheckpointMarker;

        [SerializeField]
        private ShieldAbility m_ShieldAbility;

        [SerializeField]
        private TierTrackerReference m_TierTrackerReference;

        [SerializeField]
        private ShieldTrigger m_ShieldTrigger;

        [SerializeField]
        private bool m_IsStartingCheckpoint;

        private void Start()
        {
            m_TriggerListener.Entered += TriggerListener_OnEntered;
        }

        private void OnDestroy()
        {
            m_TriggerListener.Entered -= TriggerListener_OnEntered;
        }

        public void SetPlayer(Player player)
        {
            Player = player;
            m_TriggerListener.DisableTrigger();
            m_CheckpointMarker.SetActive(false);

            m_ShieldAbility.SetPlayer(Player);
            m_ShieldTrigger.SetPlayer(Player);
            var rangeTierTracker = (RangeTierTracker)m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range);
            var shieldStrengthTierTracker = (ShieldStrengthTierTracker)m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength);
            m_ShieldTrigger.EnableShield(rangeTierTracker, shieldStrengthTierTracker, m_ShieldAbility);
        }

        public void SetTurnReached(int turnReached)
        {
            TurnReached = turnReached;
        }

        private void TriggerListener_OnEntered(object sender, TriggerEventArgs e)
        {
            var gogo = e.OtherCollider.GetComponent<AbstractGogo>();
            if (gogo == null)
            {
                return;
            }

            if (Player != null || gogo.Player == null)
            {
                return;
            }

            SetPlayer(gogo.Player);
            SetTurnReached(TurnTracker.Turn);
        }
    }
}
