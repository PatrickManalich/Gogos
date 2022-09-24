using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class WeightVisualRefresher : MonoBehaviour
    {
        [SerializeField]
        private WeightTierTracker m_WeightTierTracker;

        [SerializeField]
        private BoxCollider m_BoxCollider;

        [SerializeField]
        private GameObject m_Visuals;

        private static readonly Dictionary<WeightTier, float> ScalarsByTier = new Dictionary<WeightTier, float>()
        {
            { WeightTier.Lightweight, 1 },
            { WeightTier.Middleweight, 1.25f },
            { WeightTier.Heavyweight, 1.5f },
        };

        private Vector3 m_OriginalBoxColliderSize;

        private void Awake()
        {
            m_OriginalBoxColliderSize = m_BoxCollider.size;
        }

        private void OnEnable()
        {
            m_WeightTierTracker.TierChanged += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            m_WeightTierTracker.TierChanged -= Refresh;
        }

        private void Refresh()
        {
            var scalar = ScalarsByTier[m_WeightTierTracker.Tier];
            m_BoxCollider.size = m_OriginalBoxColliderSize * scalar;
            m_Visuals.transform.localScale = Vector3.one * scalar;
        }
    }
}
