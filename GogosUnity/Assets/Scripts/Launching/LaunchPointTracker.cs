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

        [SerializeField]
        private Checkpoint[] m_Checkpoints;

        private void Start()
        {
            m_PlayerGogoReturner.Returned += GetLaunchPoints;
            m_PlayerGogoReturner.Skipped += GetLaunchPoints;

            var startingCheckpoints = m_Checkpoints.Where(c => c.IsStartingCheckpoint).ToList();
            foreach (var player in PlayerTracker.Players)
            {
                var randomStartingCheckpoint = startingCheckpoints[Random.Range(0, startingCheckpoints.Count)];
                randomStartingCheckpoint.SetPlayer(player);
                randomStartingCheckpoint.SetTurnReached(TurnTracker.Turn);
                startingCheckpoints.Remove(randomStartingCheckpoint);
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

            var checkpoints = m_Checkpoints.Where(c => c.Player == PlayerTracker.Player);
            foreach (var checkpoint in checkpoints)
            {
                LaunchPoints.Add(new LaunchPoint(checkpoint.TurnReached, checkpoint.transform.position));
            }

            LaunchPoints = LaunchPoints.OrderByDescending(l => l.Turn).ToList();
        }
    }
}
