// https://answers.unity.com/questions/1169028/unity-dropdown-doesnt-scroll-when-navigating-with.html
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gogos
{
    public class Autoscroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private ScrollRect m_ScrollRect;

        private RectTransform m_ScrollRectTransform;
        private RectTransform m_ContentPanel;
        private bool m_IsInitialized;

        private GameObject m_LastSelected;
        private bool m_IsMouseHovering;

        private IEnumerator Start()
        {
            yield return null;  // Allow scroll view to be populated before autoscrolling

            m_ScrollRectTransform = m_ScrollRect.GetComponent<RectTransform>();
            m_ContentPanel = m_ScrollRect.content;
            m_IsInitialized = true;
        }

        private void Update()
        {
            if (m_IsInitialized && !m_IsMouseHovering)
            {
                Autoscroll();
            }
        }

        public void Autoscroll()
        {
            var selected = EventSystem.current.currentSelectedGameObject;

            if (selected == null)
            {
                return;
            }
            if (selected.transform.parent != m_ContentPanel.transform)
            {
                return;
            }
            if (selected == m_LastSelected)
            {
                return;
            }

            var selectedRectTransform = (RectTransform)selected.transform;
            var anchoredPositionX = m_ContentPanel.anchoredPosition.x;
            var anchoredPositionY = -selectedRectTransform.localPosition.y - (selectedRectTransform.rect.height / 2);
            var clampedAnchoredPositionY = Mathf.Clamp(anchoredPositionY, 0, m_ContentPanel.sizeDelta.y - m_ScrollRectTransform.sizeDelta.y);
            m_ContentPanel.anchoredPosition = new Vector2(anchoredPositionX, clampedAnchoredPositionY);

            m_LastSelected = selected;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_IsMouseHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_IsMouseHovering = false;
        }
    }
}
