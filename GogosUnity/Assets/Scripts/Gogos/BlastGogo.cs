using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class BlastGogo : AbstractGogo
    {
        [SerializeField]
        private TriggerRangeRefresher m_TriggerRangeRefresher;

        [SerializeField]
        private List<BlastTrigger> m_BlastTriggers;

        private Vector3 m_StartedMovingPosition;

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            m_BlastTriggers.ForEach(b => b.SetPlayer(Player));
        }

        public override void SetTiers(IdentifiableGogo identifiableGogo)
        {
            base.SetTiers(identifiableGogo);
            var blastScriptableGogo = (BlastScriptableGogo)IdentifiableGogo.ScriptableGogo;
            TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastPower).SetTier((int)blastScriptableGogo.BlastPowerTier);
        }

        protected override void OnStartedMoving()
        {
            m_StartedMovingPosition = transform.position;
        }

        protected override void OnStoppedMoving()
        {
            UnsubscribeFromMovementEvents();

            m_TriggerRangeRefresher.enabled = false;
            UnparentTriggerRange();
            TriggerRange.transform.rotation = Quaternion.LookRotation(TriggerRange.transform.position - m_StartedMovingPosition);

            var rangeTierTracker = (RangeTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range);
            var blastPowerTierTracker = (BlastPowerTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastPower);
            m_BlastTriggers.ForEach(t => t.Blast(rangeTierTracker, blastPowerTierTracker));
        }
    }
}
