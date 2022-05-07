using UnityEngine;
using UnityEngine.EventSystems;

namespace Gogos
{
    public class LauncherKeyInputs : MonoBehaviour
    {
        [SerializeField]
        private Launcher m_Launcher;

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            if (InputKeys.User.TurnLauncherLeftKey)
            {
                m_Launcher.TurnLeft();
            }
            if (InputKeys.User.TurnLauncherRightKey)
            {
                m_Launcher.TurnRight();
            }
            if (InputKeys.User.IncreaseLaunchForceKey)
            {
                m_Launcher.IncreaseLaunchPower();
            }
            if (InputKeys.User.DecreaseLaunchForceKey)
            {
                m_Launcher.DecreaseLaunchPower();
            }
            if (InputKeys.User.CyclePreviousLaunchPointKey)
            {
                m_Launcher.CyclePreviousLaunchPoint();
            }
            if (InputKeys.User.CycleNextLaunchPointKey)
            {
                m_Launcher.CycleNextLaunchPoint();
            }
            if (InputKeys.User.ResetGogoKeyDown)
            {
                m_Launcher.PrepareForLaunch();
            }
            if (InputKeys.User.LaunchGogoKeyDown && m_Launcher.ReadyForLaunch)
            {
                m_Launcher.Launch();
            }
        }
    }
}
