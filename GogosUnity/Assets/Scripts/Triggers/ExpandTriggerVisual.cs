using UnityEngine;

namespace Gogos
{
    public class ExpandTriggerVisual : MonoBehaviour
    {
        [SerializeField]
        private ExpandTrigger m_ExpandTrigger;

        [SerializeField]
        private TriggerAnimationsReference m_TriggerAnimationsReference;

        [SerializeField]
        private GameObject m_Visual;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        [SerializeField]
        private float m_Intensity;

        private Renderer[] m_Renderers = new Renderer[] { };

        private void Start()
        {
            m_ExpandTrigger.PlayerChanged += Refresh;
            m_TriggerAnimationsReference.AnimationStarted += TriggerAnimationsReference_OnAnimationStarted;
            m_TriggerAnimationsReference.AnimationFinished += TriggerAnimationsReference_OnAnimationFinished;

            m_Renderers = m_Visual.GetComponentsInChildren<Renderer>(true);
            m_Visual.SetActive(false);
            Refresh();
        }

        private void OnDestroy()
        {
            m_TriggerAnimationsReference.AnimationFinished -= TriggerAnimationsReference_OnAnimationFinished;
            m_TriggerAnimationsReference.AnimationStarted -= TriggerAnimationsReference_OnAnimationStarted;
            m_ExpandTrigger.PlayerChanged -= Refresh;
        }

        private void TriggerAnimationsReference_OnAnimationStarted(object sender, TriggerAnimationEventArgs e)
        {
            if (e.TriggerAnimation == TriggerAnimation.Invisible)
            {
                m_Visual.SetActive(false);
            }
        }

        private void TriggerAnimationsReference_OnAnimationFinished(object sender, TriggerAnimationEventArgs e)
        {
            if (e.TriggerAnimation == TriggerAnimation.Expand)
            {
                m_Visual.SetActive(true);
            }
        }

        private void Refresh()
        {
            var player = m_ExpandTrigger.Player;
            if (player == null)
            {
                return;
            }

            var playerColor = m_ScriptableColorPalette.GetColorForPlayerColor(player.PlayerColor);
            foreach (var renderer in m_Renderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.SetColor("_TintColor", playerColor * m_Intensity);
                    material.SetColor("_Color", playerColor * m_Intensity);
                    material.SetColor("_RimColor", playerColor * m_Intensity);
                }
            }
        }
    }
}
