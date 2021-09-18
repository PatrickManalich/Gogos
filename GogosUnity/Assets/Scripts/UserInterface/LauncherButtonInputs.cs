using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class LauncherButtonInputs : MonoBehaviour
    {
        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private HoldDownButton m_MoveLeftButton;

        [SerializeField]
        private HoldDownButton m_MoveRightButton;

        [SerializeField]
        private HoldDownButton m_DecreaseLaunchForceButton;

        [SerializeField]
        private HoldDownButton m_IncreaseLaunchForceButton;

        [SerializeField]
        private Button m_LaunchButton;

        private void Start()
        {
            m_MoveLeftButton.Held += m_Launcher.MoveLeft;
            m_MoveRightButton.Held += m_Launcher.MoveRight;
            m_DecreaseLaunchForceButton.Held += m_Launcher.DecreaseLaunchForce;
            m_IncreaseLaunchForceButton.Held += m_Launcher.IncreaseLaunchForce;
            m_LaunchButton.onClick.AddListener(LaunchButton_OnClick);
        }

        private void OnDestroy()
        {
            m_LaunchButton.onClick.RemoveListener(LaunchButton_OnClick);
            m_IncreaseLaunchForceButton.Held -= m_Launcher.IncreaseLaunchForce;
            m_DecreaseLaunchForceButton.Held -= m_Launcher.DecreaseLaunchForce;
            m_MoveRightButton.Held -= m_Launcher.MoveRight;
            m_MoveLeftButton.Held -= m_Launcher.MoveLeft;
        }

        private void LaunchButton_OnClick()
        {
            if (m_Launcher.ReadyForLaunch)
            {
                m_Launcher.Launch();
            }
        }
    }
}
