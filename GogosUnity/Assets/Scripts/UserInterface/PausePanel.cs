using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Root;

        [SerializeField]
        private Button m_ResumeButton;

        [SerializeField]
        private Button m_QuitButton;

        private float m_PreviousTimeScale;

        private void Start()
        {
            m_ResumeButton.onClick.AddListener(Unpause);
            m_QuitButton.onClick.AddListener(QuitButton_OnClick);

            m_Root.SetActive(false);
        }

        private void OnDestroy()
        {
            m_QuitButton.onClick.RemoveListener(QuitButton_OnClick);
            m_ResumeButton.onClick.RemoveListener(Unpause);
        }

        private void Update()
        {
            if (InputKeys.User.PauseKeyDown)
            {
                if (Time.timeScale != 0)
                {
                    Pause();
                }
                else
                {
                    Unpause();
                }
            }
        }

        private void QuitButton_OnClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void Pause()
        {
            m_PreviousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            m_Root.SetActive(true);
        }

        private void Unpause()
        {
            Time.timeScale = m_PreviousTimeScale;
            m_Root.SetActive(false);
        }
    }
}
