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

            if (InputKeys.ResetGogoKeyDown)
            {
                m_Launcher.PrepareForLaunch();
            }
            if (InputKeys.LaunchGogoKeyDown && m_Launcher.ReadyForLaunch)
            {
                m_Launcher.Launch();
            }
            if (InputKeys.MoveLauncherLeftKey)
            {
                m_Launcher.MoveLeft();
            }
            if (InputKeys.MoveLauncherRightKey)
            {
                m_Launcher.MoveRight();
            }
            if (InputKeys.IncreaseLaunchForceKey)
            {
                m_Launcher.IncreaseLaunchForce();
            }
            if (InputKeys.DecreaseLaunchForceKey)
            {
                m_Launcher.DecreaseLaunchForce();
            }
        }
    }
}