using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gogos
{
    public class HoldDownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action Held;

        private Coroutine m_EndlessHoldCoroutine;

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                return;
            }

            if (InputKeys.SubmitKeyDown)
            {
                RestartEndlessHold();
            }
            else if (InputKeys.SubmitKeyUp)
            {
                StopEndlessHold();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RestartEndlessHold();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopEndlessHold();
        }

        private void RestartEndlessHold()
        {
            StopEndlessHold();
            StartEndlessHold();
        }

        private void StartEndlessHold()
        {
            m_EndlessHoldCoroutine = StartCoroutine(EndlessHoldRoutine());
        }

        private void StopEndlessHold()
        {
            if (m_EndlessHoldCoroutine != null)
            {
                StopCoroutine(m_EndlessHoldCoroutine);
                m_EndlessHoldCoroutine = null;
            }
        }

        private IEnumerator EndlessHoldRoutine()
        {
            while (true)
            {
                Held?.Invoke();
                yield return null;
            }
        }
    }
}
