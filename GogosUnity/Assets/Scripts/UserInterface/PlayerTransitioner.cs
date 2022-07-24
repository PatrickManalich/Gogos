using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gogos
{
    public class PlayerTransitioner : MonoBehaviour
    {
        public event Action Transitioned;

        public event Action Skipped;

        [SerializeField]
        private GameObject m_Announcement;

        [SerializeField]
        private TextMeshProUGUI m_AnnouncementText;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.PlayerTransitioning)
            {
                StartCoroutine(FlashTextAndTransitionRoutine());
            }
        }

        private IEnumerator FlashTextAndTransitionRoutine()
        {
            if (TurnTracker.Turn % 2 == 0)
            {
                Skipped?.Invoke();
            }
            else
            {
                if (TurnTracker.Turn != 1)  // On first turn, correct player is already set
                {
                    PlayerTracker.TransitionToNextPlayer();
                }

                m_AnnouncementText.text = $"{PlayerTracker.Player.Name}'s Turn!";
                m_Announcement.SetActive(true);
                yield return new WaitForSeconds(2);

                m_Announcement.gameObject.SetActive(false);
                Transitioned?.Invoke();
            }
        }
    }
}
