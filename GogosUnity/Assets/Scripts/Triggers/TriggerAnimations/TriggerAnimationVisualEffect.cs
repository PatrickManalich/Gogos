using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class TriggerAnimationVisualEffect : MonoBehaviour, ITriggerAnimationObserver
    {
        [SerializeField]
        private TriggerAnimationSubject m_TriggerAnimationSubject;

        [SerializeField]
        private GameObject m_VisualEffect;

        [SerializeField]
        private bool m_AdjustColorAndIntensity;

        [SerializeField]
        private Color m_Color;

        [SerializeField]
        private float m_Intensity;

        [SerializeField]
        private float m_ResetDelay;

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

            m_TriggerAnimationSubject.AddObserverForAnimationStarted(this, TriggerAnimation.Expand);
        }

        private void OnDestroy()
        {
            m_TriggerAnimationSubject.RemoveObserverForAnimationStarted(this, TriggerAnimation.Expand);
        }

        public void Notify()
        {
            m_VisualEffect.SetActive(true);
            StartCoroutine(ResetAfterDelayRoutine());
        }

        private IEnumerator ResetAfterDelayRoutine()
        {
            yield return new WaitForSeconds(m_ResetDelay);
            m_VisualEffect.SetActive(false);
        }
    }
}
