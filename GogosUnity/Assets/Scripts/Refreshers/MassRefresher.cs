using UnityEngine;

namespace Gogos
{
    public class MassRefresher : MonoBehaviour
    {
        [SerializeField]
        private WeightTierTracker m_WeightTierTracker;

        [SerializeField]
        private Rigidbody m_Rigidbody;

        private void Start()
        {
            m_WeightTierTracker.CurrentTierChanged += Refresh;

            Refresh();
        }

        private void OnDestroy()
        {
            m_WeightTierTracker.CurrentTierChanged -= Refresh;
        }

        public void Refresh()
        {
            m_Rigidbody.mass = m_WeightTierTracker.Weight;
        }
    }
}