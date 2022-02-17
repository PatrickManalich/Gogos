using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class LaunchPointTracker : MonoBehaviour
    {
        public List<LaunchPoint> LaunchPoints { get; private set; } = new List<LaunchPoint>();

        [SerializeField]
        private PlayerGogoReturner m_PlayerGogoReturner;

        private LaunchPointTrigger[] m_LaunchPointTriggers;

        private void Start()
        {
            m_PlayerGogoReturner.Returned += GetLaunchPoints;
            m_PlayerGogoReturner.Skipped += GetLaunchPoints;

            m_LaunchPointTriggers = FindObjectsOfType<LaunchPointTrigger>();
            var startingTriggers = m_LaunchPointTriggers.Where(c => c.IsStartingTrigger).ToList();
            foreach (var player in PlayerTracker.Players)
            {
                var randomStartingTrigger = startingTriggers[Random.Range(0, startingTriggers.Count)];
                randomStartingTrigger.SetPlayer(player);
                randomStartingTrigger.SetTurnReached(0);
                startingTriggers.Remove(randomStartingTrigger);
            }

            GetLaunchPoints();
        }

        private void OnDestroy()
        {
            m_PlayerGogoReturner.Skipped -= GetLaunchPoints;
            m_PlayerGogoReturner.Returned -= GetLaunchPoints;
        }

        private void GetLaunchPoints()
        {
            LaunchPoints.Clear();

            var playersGogos = FindObjectsOfType<AbstractGogo>().Where(g => g.Player == PlayerTracker.Player);
            var launchedPlayerGogos = playersGogos.Where(p => GogoSituationDatabase.GetSituation(p.IdentifiableGogo) == Situation.Launched);
            foreach (var launchedPlayerGogo in launchedPlayerGogos)
            {
                LaunchPoints.Add(new LaunchPoint(launchedPlayerGogo.TurnLaunched, launchedPlayerGogo.transform.position));
            }

            var playerTriggers = m_LaunchPointTriggers.Where(c => c.Player == PlayerTracker.Player);
            foreach (var playerTrigger in playerTriggers)
            {
                LaunchPoints.Add(new LaunchPoint(playerTrigger.TurnReached, playerTrigger.transform.position));
            }

            LaunchPoints = LaunchPoints.OrderByDescending(l => l.Turn).ToList();
        }
    }
}
