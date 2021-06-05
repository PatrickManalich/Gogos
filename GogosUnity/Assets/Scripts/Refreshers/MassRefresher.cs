using UnityEngine;

namespace Gogos
{
    public class MassRefresher : MonoBehaviour
    {
        [SerializeField]
        private WeightTierTracker m_WeightTierTracker;

        [SerializeField]
        private Rigidbody m_Rigidbody;

        private void OnEnable()
        {
            m_WeightTierTracker.TierChanged += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            m_WeightTierTracker.TierChanged -= Refresh;
        }

        public void Refresh()
        {
            m_Rigidbody.mass = m_WeightTierTracker.Weight;
        }
    }
}