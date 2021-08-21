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

        private GogoLauncher m_GogoLauncher;
        private Quaternion m_GogoLauncherAlignedRotation; 

        protected override void Start()
        {
            base.Start();
            m_GogoLauncher = FindObjectOfType<GogoLauncher>();
        }

        public override void SetTiers(AbstractScriptableGogo scriptableGogo)
        {
            base.SetTiers(scriptableGogo);
            var blastScriptableGogo = (BlastScriptableGogo)scriptableGogo;
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastForce).SetTier((int)blastScriptableGogo.BlastForceTier);
        }

        protected override void OnStartedMoving()
        {
            m_GogoLauncherAlignedRotation = Quaternion.Euler(new Vector3(0, m_GogoLauncher.transform.rotation.eulerAngles.y, 0));
        }

        protected override void OnStoppedMoving()
        {
            m_TriggerRangeRefresher.gameObject.SetActive(false);
            m_TriggerRangeRotationAligner.AlignWithRotation(m_GogoLauncherAlignedRotation);
            m_BlastTrigger.Blast();

            UnsubscribeFromMovementEvents();
        }
    }
}