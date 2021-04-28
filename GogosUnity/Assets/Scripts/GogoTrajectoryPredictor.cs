using TrajectoryPrediction;
using UnityEngine;

namespace Gogos
{
	public class GogoTrajectoryPredictor : MonoBehaviour
	{
        [SerializeField]
        private TrajectoryPredictor m_TrajectoryPredictor;

        [SerializeField]
        private GogoLauncher m_GogoLauncher;

        private void Awake()
		{
            m_GogoLauncher.LaunchPrepared += GogoLauncher_OnLaunchPrepared;
            m_GogoLauncher.Launched += GogoLauncher_OnLaunched;
		}

        private void OnDestroy()
		{
            m_GogoLauncher.Launched -= GogoLauncher_OnLaunched;
            m_GogoLauncher.LaunchPrepared -= GogoLauncher_OnLaunchPrepared;
        }

        private void LateUpdate()
        {
            if (m_TrajectoryPredictor.gameObject.activeSelf)
            {
                m_TrajectoryPredictor.debugLineDuration = Time.unscaledDeltaTime;
                m_TrajectoryPredictor.Predict3D(m_GogoLauncher.LaunchPoint, m_GogoLauncher.LaunchVector, Physics.gravity);
            }
		}

        private void GogoLauncher_OnLaunchPrepared()
        {
            m_TrajectoryPredictor.gameObject.SetActive(true);
        }

        private void GogoLauncher_OnLaunched()
        {
            m_TrajectoryPredictor.gameObject.SetActive(false);
        }
    }
}