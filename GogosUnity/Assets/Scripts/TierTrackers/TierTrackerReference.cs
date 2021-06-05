using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
	public class TierTrackerReference : MonoBehaviour
	{
        public IReadOnlyList<AbstractTierTracker> TierTrackers { get; private set; }

        private Dictionary<TierVariant, AbstractTierTracker> m_TierTrackersByVariant = new Dictionary<TierVariant, AbstractTierTracker>();

        private void Awake()
        {
            TierTrackers = GetComponents<AbstractTierTracker>();
            foreach (var tierTracker in TierTrackers)
            {
                m_TierTrackersByVariant.Add(tierTracker.TierVariant, tierTracker);
            }
        }

        public AbstractTierTracker GetTierTrackerForVariant(TierVariant tierVariant)
        {
            return m_TierTrackersByVariant[tierVariant];
        }
	}
}