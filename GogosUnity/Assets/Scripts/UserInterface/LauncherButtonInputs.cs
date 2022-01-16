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
        private HoldDownButton m_DecreaseLaunchPowerButton;

        [SerializeField]
        private HoldDownButton m_IncreaseLaunchPowerButton;

        [SerializeField]
        private Button m_LaunchButton;

        private void Start()
        {
            m_MoveLeftButton.Held += m_Launcher.MoveLeft;
            m_MoveRightButton.Held += m_Launcher.MoveRight;
            m_DecreaseLaunchPowerButton.Held += m_Launcher.DecreaseLaunchPower;
            m_IncreaseLaunchPowerButton.Held += m_Launcher.IncreaseLaunchPower;
            m_LaunchButton.onClick.AddListener(LaunchButton_OnClick);
        }

        private void OnDestroy()
        {
            m_LaunchButton.onClick.RemoveListener(LaunchButton_OnClick);
            m_IncreaseLaunchPowerButton.Held -= m_Launcher.IncreaseLaunchPower;
            m_DecreaseLaunchPowerButton.Held -= m_Launcher.DecreaseLaunchPower;
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
