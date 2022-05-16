using UnityEngine;

namespace Gogos
{
    public class GemCollectedEffect : AbstractCollectedEffect
    {
        [SerializeField]
        private GemValueTierTracker m_GemValueTierTracker;

        protected override void OnCollected()
        {
            if (PhaseTracker.Phase != Phase.Settling)
            {
                return;
            }

            PlayerTracker.Player.Collection.Add(m_GemValueTierTracker.GogoClass, m_GemValueTierTracker.GemValue);
        }
    }
}
