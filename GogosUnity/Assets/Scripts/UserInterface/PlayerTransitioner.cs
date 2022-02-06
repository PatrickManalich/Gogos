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
        private TextMeshProUGUI m_TransitioningText;

        private bool m_IsFirstTransition;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            m_IsFirstTransition = true;
            StartCoroutine(FlashTextAndTransitionRoutine());
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Transitioning)
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
                PlayerTracker.Instance.TransitionToNextPlayer();
            }

            m_TransitioningText.text = $"{PlayerTracker.Player.Name}'s Turn!";
            m_TransitioningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);

            m_TransitioningText.gameObject.SetActive(false);
            Transitioned?.Invoke();
        }
    }
}
