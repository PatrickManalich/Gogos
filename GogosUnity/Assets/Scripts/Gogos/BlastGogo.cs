using UnityEngine;

namespace Gogos
{
    public class BlastGogo : AbstractGogo
    {
        [SerializeField]
        private TriggerRangeRefresher m_TriggerRangeRefresher;

        [SerializeField]
        private RotationAligner m_TriggerRangeRotationAligner;

        [SerializeField]
        private BlastTrigger m_BlastTrigger;

        private Launcher m_Launcher;
        private Quaternion m_LauncherAlignedRotation;

        protected override void Start()
        {
            base.Start();
            m_Launcher = FindObjectOfType<Launcher>();
        }

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            m_BlastTrigger.Player = Player;
        }

        public override void SetTiers(IdentifiableGogo identifiableGogo)
        {
            base.SetTiers(identifiableGogo);
            var blastScriptableGogo = (BlastScriptableGogo)IdentifiableGogo.ScriptableGogo;
            TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastForce).SetTier((int)blastScriptableGogo.BlastForceTier);
        }

        protected override void OnStartedMoving()
        {
            m_LauncherAlignedRotation = Quaternion.Euler(new Vector3(0, m_Launcher.transform.rotation.eulerAngles.y, 0));
        }

        protected override void OnStoppedMoving()
        {
            m_TriggerRangeRefresher.enabled = false;
            m_TriggerRangeRotationAligner.AlignWithRotation(m_LauncherAlignedRotation);
            var rangeTierTracker = (RangeTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range);
            var blastForceTierTracker = (BlastForceTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastForce);
            m_BlastTrigger.Blast(rangeTierTracker, blastForceTierTracker);

            UnsubscribeFromMovementEvents();
        }
    }
}
