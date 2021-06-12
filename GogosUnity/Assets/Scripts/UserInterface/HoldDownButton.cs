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

        public void OnPointerDown(PointerEventData eventData)
        {
            StopEndlessHold();
            StartEndlessHold();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopEndlessHold();
        }

        private void StartEndlessHold()
        {
            m_EndlessHoldCoroutine = StartCoroutine(EndlessHold());
        }

        private void StopEndlessHold()
        {
            if (m_EndlessHoldCoroutine != null)
            {
                StopCoroutine(m_EndlessHoldCoroutine);
                m_EndlessHoldCoroutine = null;
            }
        }

        private IEnumerator EndlessHold()
        {
            while (true)
            {
                Held?.Invoke();
                yield return null;
            }
        }
    }
}