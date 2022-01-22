using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class LaunchPointTracker : MonoBehaviour
    {
        public List<Vector3> LaunchPoints { get; private set; } = new List<Vector3>();

        [SerializeField]
        private Checkpoint[] m_Checkpoints;

        private void Start()
        {
            PlayerTracker.PlayerChanged += PlayerTracker_OnPlayerChanged;

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
            PlayerTracker.PlayerChanged -= PlayerTracker_OnPlayerChanged;
        }

        private void PlayerTracker_OnPlayerChanged()
        {
            GetLaunchPoints();
        }

        private void GetLaunchPoints()
        {
            LaunchPoints.Clear();

            var checkpoints = m_Checkpoints.Where(c => c.Player == PlayerTracker.Player);
            LaunchPoints.AddRange(checkpoints.Select(c => c.transform.position));

            var playersGogos = FindObjectsOfType<AbstractGogo>().Where(g => g.Player == PlayerTracker.Player);
            var launchedPlayerGogos = playersGogos.Where(p => GogoSituationDatabase.GetSituation(p.IdentifiableGogo) == Situation.Launched);
            LaunchPoints.AddRange(launchedPlayerGogos.Select(l => l.transform.position));
        }
    }
}
