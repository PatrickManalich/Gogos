using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class LaunchPointTracker : MonoBehaviour
    {
        public LaunchPoint LaunchPoint => LaunchPoints[m_LaunchPointIndicesByPlayer[PlayerTracker.Player]];

        public List<LaunchPoint> LaunchPoints => m_LaunchPointsByPlayer[PlayerTracker.Player];

        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private GameObject m_EnvironmentCenter;

        private Dictionary<Player, int> m_LaunchPointIndicesByPlayer = new Dictionary<Player, int>();
        private Dictionary<Player, List<LaunchPoint>> m_LaunchPointsByPlayer = new Dictionary<Player, List<LaunchPoint>>();
        private LaunchPointTrigger[] m_LaunchPointTriggers;

        private void Awake()
        {
            foreach (var player in PlayerTracker.Players)
            {
                m_LaunchPointIndicesByPlayer.Add(player, 0);
                m_LaunchPointsByPlayer.Add(player, new List<LaunchPoint>());
            }
        }

        private void Start()
        {
            m_LaunchPointTriggers = FindObjectsOfType<LaunchPointTrigger>();
            var startingTriggers = m_LaunchPointTriggers.Where(c => c.IsStartingTrigger).ToList();
            foreach (var player in PlayerTracker.Players)
            {
                var randomStartingTrigger = startingTriggers[Random.Range(0, startingTriggers.Count)];
                startingTriggers.Remove(randomStartingTrigger);
                randomStartingTrigger.SetPlayer(player);
                CreateLaunchPoint(randomStartingTrigger);
            }

            m_Launcher.Launched += Launcher_OnLaunched;
            foreach (var launchPointTrigger in m_LaunchPointTriggers)
            {
                launchPointTrigger.Triggered += LaunchPointTrigger_OnTriggered;
            }
        }

        private void OnDestroy()
        {
            foreach (var launchPointTrigger in m_LaunchPointTriggers)
            {
                launchPointTrigger.Triggered -= LaunchPointTrigger_OnTriggered;
            }
            m_Launcher.Launched -= Launcher_OnLaunched;
        }

        public void CycleLaunchPoint(int direction)
        {
            var launchPointIndex = m_LaunchPointIndicesByPlayer[PlayerTracker.Player];
            var launchPoints = m_LaunchPointsByPlayer[PlayerTracker.Player];

            launchPointIndex += direction;
            if (launchPointIndex > launchPoints.Count - 1)
            {
                launchPointIndex = 0;
            }
            else if (launchPointIndex < 0)
            {
                launchPointIndex = launchPoints.Count - 1;
            }
            m_LaunchPointIndicesByPlayer[PlayerTracker.Player] = launchPointIndex;
        }

        private void Launcher_OnLaunched()
        {
            LaunchPoint.TurnAngle = m_Launcher.transform.rotation.eulerAngles.y;
        }

        private void LaunchPointTrigger_OnTriggered(object sender, System.EventArgs e)
        {
            CreateLaunchPoint((LaunchPointTrigger)sender);
        }

        private void CreateLaunchPoint(LaunchPointTrigger trigger)
        {
            var lookAtEnvironmentCenterRotation = Quaternion.LookRotation(m_EnvironmentCenter.transform.position - trigger.transform.position);
            var turnAngle = lookAtEnvironmentCenterRotation.eulerAngles.y;
            var launchPoint = new LaunchPoint(trigger.transform.position, turnAngle);
            m_LaunchPointsByPlayer[trigger.Player].Add(launchPoint);
        }
    }
}
