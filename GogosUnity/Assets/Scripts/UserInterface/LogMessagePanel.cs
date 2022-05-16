using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Gogos
{
    public class LogMessagePanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Root;

        [SerializeField]
        private TextMeshProUGUI m_MessageText;

        [SerializeField]
        private TextMeshProUGUI m_StackText;

        [SerializeField]
        private Button m_ContinueButton;

        [SerializeField]
        private Button m_QuitButton;

        private void Awake()
        {
            Application.logMessageReceived += Application_OnLogMessageReceived;
        }

        private void Start()
        {
            m_QuitButton.onClick.AddListener(QuitButton_OnClick);
            m_ContinueButton.onClick.AddListener(ContinueButton_OnClick);

            m_Root.SetActive(false);
        }

        private void OnDestroy()
        {
            m_ContinueButton.onClick.RemoveListener(ContinueButton_OnClick);
            m_QuitButton.onClick.RemoveListener(QuitButton_OnClick);
        }

        private void Application_OnLogMessageReceived(string message, string stackTrace, LogType type)
        {
            if (type == LogType.Log)
            {
                return;
            }

            m_Root.SetActive(true);
            m_MessageText.text = message;
            m_StackText.text = stackTrace;
        }

        private void ContinueButton_OnClick()
        {
            m_Root.SetActive(false);
        }

        private void QuitButton_OnClick()
        {
            GUIUtility.systemCopyBuffer = m_MessageText.text + Environment.NewLine + Environment.NewLine + m_StackText.text;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
