using Gogos.Managers;
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

            if (InputManager.ResetGogoKeyDown)
            {
                m_Launcher.PrepareForLaunch();
            }
            if (InputManager.LaunchGogoKeyDown && m_Launcher.ReadyForLaunch)
            {
                m_Launcher.Launch();
            }
            if (InputManager.MoveLauncherLeftKey)
            {
                m_Launcher.MoveLeft();
            }
            if (InputManager.MoveLauncherRightKey)
            {
                m_Launcher.MoveRight();
            }
            if (InputManager.IncreaseLaunchForceKey)
            {
                m_Launcher.IncreaseLaunchForce();
            }
            if (InputManager.DecreaseLaunchForceKey)
            {
                m_Launcher.DecreaseLaunchForce();
            }
        }
    }
}