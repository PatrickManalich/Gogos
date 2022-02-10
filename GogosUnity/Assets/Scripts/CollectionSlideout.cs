using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class CollectionSlideout : MonoBehaviour
    {
        [SerializeField]
        private Button m_OpenButton;

        [SerializeField]
        private Button m_CloseButton;

        [SerializeField]
        private GameObject m_CollectionGogoDetailsPanel;

        [SerializeField]
        private GameObject m_CollectionScrollView;

        private void Start()
        {
            m_OpenButton.onClick.AddListener(OpenButton_OnClick);
            m_CloseButton.onClick.AddListener(CloseButton_OnClick);
        }

        private void OnDestroy()
        {
            m_CloseButton.onClick.RemoveListener(CloseButton_OnClick);
            m_OpenButton.onClick.RemoveListener(OpenButton_OnClick);
        }

        private void OpenButton_OnClick()
        {
            m_CloseButton.gameObject.SetActive(true);
            m_CollectionGogoDetailsPanel.SetActive(true);
            m_CollectionScrollView.SetActive(true);
        }

        private void CloseButton_OnClick()
        {
            m_CloseButton.gameObject.SetActive(false);
            m_CollectionGogoDetailsPanel.SetActive(false);
            m_CollectionScrollView.SetActive(false);
        }
    }
}
