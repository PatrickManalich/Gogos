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
        private GameObject[] m_PointCubes;

        protected override void OnBlasted()
        {
            for (int i = 0; i < m_CapacityTierTracker.Capacity; i++)
            {
                var pointCube = m_PointCubes[i];
                pointCube.SetActive(true);
                pointCube.transform.parent = null;
            }
            Destroy(m_Root);
        }
    }
}
