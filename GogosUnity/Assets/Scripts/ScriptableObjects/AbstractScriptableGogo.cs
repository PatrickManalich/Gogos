using UnityEngine;

namespace Gogos
{
    public enum GogoClass { Blast, Shield, Support, Golden }

    public abstract class AbstractScriptableGogo : ScriptableObject
    {
        public abstract GogoClass GogoClass { get; }

        [SerializeField]
        private GameObject m_Prefab;
        public GameObject Prefab => m_Prefab;

        [SerializeField]
        private string m_Nickname;
        public string Nickname => m_Nickname;

        [SerializeField]
        private Sprite m_Portrait;
        public Sprite Portrait => m_Portrait;

        [SerializeField]
        private int m_Number;
        public int Number => m_Number;

        [SerializeField]
        private RarityTier m_RarityTier;
        public RarityTier RarityTier => m_RarityTier;

        [SerializeField]
        private WeightTier m_WeightTier;
        public WeightTier WeightTier => m_WeightTier;

        public PointValueTier PointValueTier => (PointValueTier)WeightTier;

        public GemValueTier GemValueTier => (GemValueTier)WeightTier;

        [SerializeField]
        private RangeTier m_RangeTier;
        public RangeTier RangeTier => m_RangeTier;
    }
}
