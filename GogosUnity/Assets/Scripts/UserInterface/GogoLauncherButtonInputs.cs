using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
	public class GogoLauncherButtonInputs : MonoBehaviour
	{
		[SerializeField]
		private GogoLauncher m_GogoLauncher;

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
			m_MoveLeftButton.Held += m_GogoLauncher.MoveLeft;
			m_MoveRightButton.Held += m_GogoLauncher.MoveRight;
			m_DecreaseLaunchForceButton.Held += m_GogoLauncher.DecreaseLaunchForce;
			m_IncreaseLaunchForceButton.Held += m_GogoLauncher.IncreaseLaunchForce;
			m_LaunchButton.onClick.AddListener(LaunchButton_OnClick);
		}

        private void OnDestroy()
		{
			m_LaunchButton.onClick.RemoveListener(LaunchButton_OnClick);
			m_IncreaseLaunchForceButton.Held -= m_GogoLauncher.IncreaseLaunchForce;
			m_DecreaseLaunchForceButton.Held -= m_GogoLauncher.DecreaseLaunchForce;
			m_MoveRightButton.Held -= m_GogoLauncher.MoveRight;
			m_MoveLeftButton.Held -= m_GogoLauncher.MoveLeft;
		}

		private void LaunchButton_OnClick()
        {
            if (m_GogoLauncher.ReadyForLaunch)
            {
				m_GogoLauncher.Launch();
			}
        }
	}
}