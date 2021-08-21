using TrajectoryPrediction;
using UnityEngine;

namespace Gogos
{
	public class LauncherTrajectoryPredictor : MonoBehaviour
	{
        [SerializeField]
        private TrajectoryPredictor m_TrajectoryPredictor;

        [SerializeField]
        private Launcher m_Launcher;

        private void Awake()
		{
            m_Launcher.LaunchPrepared += Launcher_OnLaunchPrepared;
            m_Launcher.Launched += Launcher_OnLaunched;
		}

        private void OnDestroy()
		{
            m_Launcher.Launched -= Launcher_OnLaunched;
            m_Launcher.LaunchPrepared -= Launcher_OnLaunchPrepared;
        }

        private void LateUpdate()
        {
            if (m_TrajectoryPredictor.gameObject.activeSelf)
            {
                m_TrajectoryPredictor.debugLineDuration = Time.unscaledDeltaTime;
                m_TrajectoryPredictor.Predict3D(m_Launcher.LaunchPoint, m_Launcher.LaunchVector, Physics.gravity);
            }
		}

        private void Launcher_OnLaunchPrepared()
        {
            m_TrajectoryPredictor.gameObject.SetActive(true);
        }

        private void Launcher_OnLaunched()
        {
            m_TrajectoryPredictor.gameObject.SetActive(false);
        }
    }
}