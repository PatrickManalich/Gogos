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

            if (InputKeys.TurnLauncherLeftKey)
            {
                m_Launcher.TurnLeft();
            }
            if (InputKeys.TurnLauncherRightKey)
            {
                m_Launcher.TurnRight();
            }
            if (InputKeys.IncreaseLaunchForceKey)
            {
                m_Launcher.IncreaseLaunchPower();
            }
            if (InputKeys.DecreaseLaunchForceKey)
            {
                m_Launcher.DecreaseLaunchPower();
            }
            if (InputKeys.ResetGogoKeyDown)
            {
                m_Launcher.PrepareForLaunch();
            }
            if (InputKeys.LaunchGogoKeyDown && m_Launcher.ReadyForLaunch)
            {
                m_Launcher.Launch();
            }
        }
    }
}
