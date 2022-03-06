using System;
using UnityEngine;

namespace Gogos
{
    public class LaunchPointTrigger : MonoBehaviour
    {
        public event EventHandler Triggered;

        public Player Player { get; private set; }

        public bool IsStartingTrigger => m_IsStartingTrigger;

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
            var renderers = m_ClaimedVisual.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
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
