using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class LauncherButtonInputs : MonoBehaviour
    {
        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private LaunchPointTracker m_LaunchPointTracker;

        [SerializeField]
        private HoldDownButton m_TurnLeftButton;

        [SerializeField]
        private HoldDownButton m_TurnRightButton;

        [SerializeField]
        private HoldDownButton m_DecreaseLaunchPowerButton;

        [SerializeField]
        private HoldDownButton m_IncreaseLaunchPowerButton;

        [SerializeField]
        private Button m_CyclePreviousLaunchPointButton;

        [SerializeField]
        private Button m_CycleNextLaunchPointButton;

        [SerializeField]
        private Button m_LaunchButton;

        private void Start()
        {
            m_TurnLeftButton.Held += m_Launcher.TurnLeft;
            m_TurnRightButton.Held += m_Launcher.TurnRight;
            m_DecreaseLaunchPowerButton.Held += m_Launcher.DecreaseLaunchPower;
            m_IncreaseLaunchPowerButton.Held += m_Launcher.IncreaseLaunchPower;
            m_CyclePreviousLaunchPointButton.onClick.AddListener(m_Launcher.CyclePreviousLaunchPoint);
            m_CycleNextLaunchPointButton.onClick.AddListener(m_Launcher.CycleNextLaunchPoint);
            m_LaunchButton.onClick.AddListener(LaunchButton_OnClick);
        }

        private void OnEnable()
        {
            var hasMultipleLaunchPoints = m_LaunchPointTracker.LaunchPoints.Count > 1;
            m_CyclePreviousLaunchPointButton.gameObject.SetActive(hasMultipleLaunchPoints);
            m_CycleNextLaunchPointButton.gameObject.SetActive(hasMultipleLaunchPoints);
        }

        private void OnDestroy()
        {
            m_LaunchButton.onClick.RemoveListener(LaunchButton_OnClick);
            m_CycleNextLaunchPointButton.onClick.RemoveListener(m_Launcher.CycleNextLaunchPoint);
            m_CyclePreviousLaunchPointButton.onClick.RemoveListener(m_Launcher.CyclePreviousLaunchPoint);
            m_IncreaseLaunchPowerButton.Held -= m_Launcher.IncreaseLaunchPower;
            m_DecreaseLaunchPowerButton.Held -= m_Launcher.DecreaseLaunchPower;
            m_TurnRightButton.Held -= m_Launcher.TurnRight;
            m_TurnLeftButton.Held -= m_Launcher.TurnLeft;
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
