using UnityEngine;

namespace Gogos
{
    public class TriggerAnimationVisualEffect : MonoBehaviour
    {
        [SerializeField]
        private TriggerAnimationsReference m_TriggerAnimationsReference;

        [SerializeField]
        private GameObject m_VisualEffect;

        [SerializeField]
        private bool m_AdjustColorAndIntensity;

        [SerializeField]
        private Color m_Color;

        [SerializeField]
        private float m_Intensity;

        private void Start()
        {
            m_VisualEffect.SetActive(false);

            if (m_AdjustColorAndIntensity)
            {
                var renderers = m_VisualEffect.GetComponentsInChildren<Renderer>(true);
                foreach (var renderer in renderers)
                {
                    foreach (var material in renderer.materials)
                    {
                        material.SetColor("_TintColor", m_Color * m_Intensity);
                        material.SetColor("_Color", m_Color * m_Intensity);
                        material.SetColor("_RimColor", m_Color * m_Intensity);
                    }
                }
            }

            m_TriggerAnimationsReference.AnimationStarted += TriggerAnimationsReference_OnAnimationStarted;
        }

        private void OnDestroy()
        {
            m_TriggerAnimationsReference.AnimationStarted -= TriggerAnimationsReference_OnAnimationStarted;
        }

        private void TriggerAnimationsReference_OnAnimationStarted(object sender, TriggerAnimationEventArgs e)
        {
            if (e.TriggerAnimation == TriggerAnimation.Expand)
            {
                m_VisualEffect.SetActive(false);
                m_VisualEffect.SetActive(true);
            }
        }
    }
}
