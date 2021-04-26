using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public enum WeightTier { Lightweight, Middleweight, Heavyweight }

    public class WeightTierTracker : AbstractTierTracker<WeightTier>
    {
        public override TierVariant TierVariant => TierVariant.Weight;
        public override WeightTier CurrentTier { get => m_CurrentTier; protected set => m_CurrentTier = value; }

        public float Weight => WeightByTier[CurrentTier];

        private static readonly Dictionary<WeightTier, float> WeightByTier = new Dictionary<WeightTier, float>()
        {
            { WeightTier.Lightweight, 0.75f },
            { WeightTier.Middleweight, 1 },
            { WeightTier.Heavyweight, 1.25f },
        };

        [SerializeField]
        private WeightTier m_CurrentTier;
    }
}