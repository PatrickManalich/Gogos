using UnityEngine;

namespace Gogos
{
    public enum RarityTier { Common, Uncommon, Rare, Golden }

    public class RarityTierTracker : AbstractTierTracker<RarityTier>
    {
        public override TierVariant TierVariant => TierVariant.Rarity;
        public override RarityTier CurrentTier { get => m_CurrentTier; protected set => m_CurrentTier = value; }

        [SerializeField]
        private RarityTier m_CurrentTier;
    }
}