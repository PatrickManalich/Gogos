using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public enum RangeTier { Small, Medium, Large }

    public class RangeTierTracker : AbstractTierTracker<RangeTier>
    {
        public override TierVariant TierVariant => TierVariant.Range;
        public override RangeTier CurrentTier { get => m_CurrentTier; protected set => m_CurrentTier = value; }

        public float Range => RangeByTier[CurrentTier];

        private static readonly Dictionary<RangeTier, float> RangeByTier = new Dictionary<RangeTier, float>()
        {
            { RangeTier.Small, 10 },
            { RangeTier.Medium, 15 },
            { RangeTier.Large, 20 },
        };

        [SerializeField]
        private RangeTier m_CurrentTier;

    }
}