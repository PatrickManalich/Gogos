using System;
using UnityEngine;

namespace Gogos
{
    public class LaunchPointTrigger : MonoBehaviour
    {
        public event EventHandler Triggered;

        public Player Player { get; private set; }

        public bool IsStartingTrigger => m_IsStartingTrigger;

        public bool IsCollectTrigger => m_IsCollectTrigger;

        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private GameObject m_UnclaimedVisual;

        [SerializeField]
        private GameObject m_ClaimedVisual;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        [SerializeField]
        private bool m_IsStartingTrigger;

        [SerializeField]
        private bool m_IsCollectTrigger;

        private Renderer[] m_ClaimedVisualRenderers;

        private void Awake()
        {
            m_ClaimedVisualRenderers = m_ClaimedVisual.GetComponentsInChildren<Renderer>(true);
        }

        private void Start()
        {
            m_TriggerListener.Entered += TriggerListener_OnEntered;

            m_UnclaimedVisual.SetActive(true);
            m_ClaimedVisual.SetActive(false);
        }

        private void OnDestroy()
        {
            m_TriggerListener.Entered -= TriggerListener_OnEntered;
        }

        public void SetPlayer(Player player)
        {
            Player = player;
            m_TriggerListener.DisableTrigger();
            m_UnclaimedVisual.SetActive(false);

            var playerColor = m_ScriptableColorPalette.GetColorForPlayerColor(Player.PlayerColor);
            foreach (var renderer in m_ClaimedVisualRenderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.SetColor("_TintColor", playerColor);
                    material.SetColor("_Color", playerColor);
                    material.SetColor("_RimColor", playerColor);
                }
            }
            m_ClaimedVisual.SetActive(true);
        }

        private void TriggerListener_OnEntered(object sender, TriggerEventArgs e)
        {
            var gogo = e.OtherCollider.GetComponent<AbstractGogo>();
            if (gogo == null)
            {
                return;
            }

            if (Player == null && gogo.Player != null)
            {
                SetPlayer(gogo.Player);
                Triggered?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
