using UnityEngine;

namespace Gogos
{
    public class Checkpoint : MonoBehaviour
    {
        public Player Player { get; private set; }

        public bool IsStartingCheckpoint => m_IsStartingCheckpoint;

        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private GameObject m_CheckpointMarker;

        [SerializeField]
        private bool m_IsStartingCheckpoint;

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
            m_CheckpointMarker.SetActive(false);
        }

        private void TriggerListener_OnEntered(object sender, TriggerEventArgs e)
        {
            var gogo = e.OtherCollider.GetComponent<AbstractGogo>();
            if (gogo == null)
            {
                return;
            }

            if (Player != null || gogo.Player == null)
            {
                return;
            }

            SetPlayer(gogo.Player);
        }
    }
}
