using UnityEngine;

namespace Gogos
{
    public class CrateBlastedEffect : AbstractBlastedEffect
    {
        [SerializeField]
        private GameObject m_Root;

        [SerializeField]
        private CapacityTierTracker m_CapacityTierTracker;

        [SerializeField]
        private GameObject[] m_Gems;

        protected override void OnBlasted(BlastTriggerEventArgs e)
        {
            for (int i = 0; i < m_CapacityTierTracker.Capacity; i++)
            {
                var gem = m_Gems[i];
                gem.SetActive(true);
                gem.transform.parent = m_Root.transform.parent;
            }
            Destroy(m_Root);
        }
    }
}
