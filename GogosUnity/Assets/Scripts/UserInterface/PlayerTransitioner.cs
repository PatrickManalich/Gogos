using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gogos
{
    public class PlayerTransitioner : MonoBehaviour
    {
        public event Action Transitioned;

        [SerializeField]
        private GameObject m_Announcement;

        [SerializeField]
        private TextMeshProUGUI m_AnnouncementText;

        private bool m_IsFirstTransition;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            m_IsFirstTransition = true;
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
            // Skip transitioning for the first transition since first player is already set
            if (m_IsFirstTransition)
            {
                m_IsFirstTransition = false;
            }
            else
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
