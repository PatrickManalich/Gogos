using Gogos.Extensions;
using TrajectoryPrediction;
using UnityEngine;

namespace Gogos
{
    public class LauncherTrajectoryPredictor : MonoBehaviour
    {
        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private TrajectoryPredictor m_TrajectoryPredictor;

        [SerializeField]
        private GameObject m_HitPoint;

        [SerializeField]
        private GameObject m_EnvironmentCenter;

        [SerializeField]
        private float m_MaxDistanceFromCenter;

        [SerializeField]
        private float m_MaxHitPointHeight;

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
                m_TrajectoryPredictor.Predict3D(m_Launcher.LaunchPoint, m_Launcher.LaunchForce, Physics.gravity);

                if (m_TrajectoryPredictor.hitInfo3D.collider != null)
                {
                    var trajectoryPoint = m_TrajectoryPredictor.hitInfo3D.point;
                    var distanceFromCenter = Vector3.Distance(trajectoryPoint.WithY(0), m_EnvironmentCenter.transform.position.WithY(0));
                    var hitPointHeight = distanceFromCenter.ConvertValueToDifferentRange(0, m_MaxDistanceFromCenter, m_MaxHitPointHeight, 0);
                    m_HitPoint.transform.position = trajectoryPoint.WithY(hitPointHeight);
                }
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
