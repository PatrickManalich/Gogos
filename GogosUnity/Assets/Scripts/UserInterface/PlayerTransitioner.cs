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
            if (PhaseTracker.Phase == Phase.Transitioning)
            {
                StartCoroutine(FlashTextAndTransitionRoutine());
            }
        }

        private IEnumerator FlashTextAndTransitionRoutine()
        {
            PlayerTracker.Instance.TransitionToNextPlayer();
            m_TransitioningText.text = $"{PlayerTracker.Player.Name}'s Turn!";
            m_TransitioningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);

            m_TransitioningText.gameObject.SetActive(false);
            Transitioned?.Invoke();
        }
    }
}
