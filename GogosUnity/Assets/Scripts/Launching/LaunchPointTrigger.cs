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
        private GameObject m_LaunchPointMarker;

        [SerializeField]
        private bool m_IsStartingTrigger;

        private void Start()
        {
            m_TriggerListener.Entered += TriggerListener_OnEntered;
        }

        private void OnDestroy()
        {
            m_TriggerListener.Entered -= TriggerListener_OnEntered;
        }

        public void SetPlayer(Player player)
        {
            Player = player;
            m_TriggerListener.DisableTrigger();
            m_LaunchPointMarker.SetActive(false);
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
