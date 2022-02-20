using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class LaunchPointTracker : MonoBehaviour
    {
        public LaunchPoint LaunchPoint => LaunchPoints[m_LaunchPointIndicesByPlayer[PlayerTracker.Player]];

        public List<LaunchPoint> LaunchPoints => m_LaunchPointsByPlayer[PlayerTracker.Player];

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

                var launchPoint = new LaunchPoint(randomStartingTrigger.transform.position);
                m_LaunchPointsByPlayer[randomStartingTrigger.Player].Add(launchPoint);
            }

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

        private void LaunchPointTrigger_OnTriggered(object sender, System.EventArgs e)
        {
            var launchPointTrigger = (LaunchPointTrigger)sender;
            var launchPoint = new LaunchPoint(launchPointTrigger.transform.position);
            m_LaunchPointsByPlayer[launchPointTrigger.Player].Add(launchPoint);
        }
    }
}
