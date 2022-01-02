using UnityEngine;

namespace Gogos
{
    public class SupportAbility : MonoBehaviour
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

        [SerializeField]
        private Groups m_Groups;

        [SerializeField]
        private TierVariant m_TierVariant;

        [Range(MinSupport, MaxSupport)]
        [SerializeField]
        private int m_TierModifier;

        public const int MinSupport = -3;
        public const int MaxSupport = 3;

        public void SetAbility(Groups groups, TierVariant tierVariant, int tierModifier)
        {
            m_Groups = groups;
            m_TierVariant = tierVariant;
            m_TierModifier = Mathf.Min(Mathf.Max(tierModifier, MinSupport), MaxSupport);
        }

        public bool CanSupport(Player player, AbstractTierTracker tierTracker)
        {
            return m_Groups.IsInGroup(player, m_Gogo.Player) && tierTracker.TierVariant == m_TierVariant;
        }

        public void ProvideSupport(AbstractTierTracker tierTracker)
        {
            tierTracker.ModifyTier(m_TierModifier);
        }

        public void RemoveSupport(AbstractTierTracker tierTracker)
        {
            tierTracker.ModifyTier(-m_TierModifier);
        }
    }
}
