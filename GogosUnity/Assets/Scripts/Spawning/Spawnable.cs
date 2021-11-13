using UnityEngine;

namespace Gogos
{
    [System.Serializable]
    public class Spawnable
    {
        [SerializeField]
        private GameObject m_Prefab;
        public GameObject Prefab => m_Prefab;

        [Range(0, 100)]
        [SerializeField]
        private int m_SpawnChance;
        public int SpawnChance => m_SpawnChance;
    }
}
