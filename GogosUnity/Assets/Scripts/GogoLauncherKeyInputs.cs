using Gogos.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gogos
{
	public class GogoLauncherKeyInputs : MonoBehaviour
	{
        [SerializeField]
        private GogoLauncher m_GogoLauncher;

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            if (InputManager.LaunchGogoKeyDown && m_GogoLauncher.ReadyForLaunch)
            {
                m_GogoLauncher.Launch();
            }
            if (InputManager.ResetGogoKeyDown)
            {
                m_GogoLauncher.PrepareForLaunch();
            }
            if (InputManager.MoveLauncherLeftKey)
            {
                m_GogoLauncher.MoveLeft();
            }
            if (InputManager.MoveLauncherRightKey)
            {
                m_GogoLauncher.MoveRight();
            }
            if (InputManager.IncreaseLaunchForceKey)
            {
                m_GogoLauncher.IncreaseLaunchForce();
            }
            if (InputManager.DecreaseLaunchForceKey)
            {
                m_GogoLauncher.DecreaseLaunchForce();
            }
        }
    }
}