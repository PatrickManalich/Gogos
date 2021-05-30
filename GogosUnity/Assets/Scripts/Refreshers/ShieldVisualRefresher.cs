using Gogos.Extensions;
using UnityEngine;

namespace Gogos
{
    public class ShieldVisualRefresher : MonoBehaviour
    {
        [SerializeField]
        private ShieldStrengthTierTracker m_ShieldStrengthTierTracker;

        [SerializeField]
        private MeshRenderer m_ShieldVisualMeshRenderer;

        private void OnEnable()
        {
            m_ShieldStrengthTierTracker.CurrentTierChanged += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            m_ShieldStrengthTierTracker.CurrentTierChanged -= Refresh;
        }

        public void Refresh()
        {
            var material = m_ShieldVisualMeshRenderer.material;
            material.color = material.color.WithA(m_ShieldStrengthTierTracker.ShieldAlpha);
        }
    }
}