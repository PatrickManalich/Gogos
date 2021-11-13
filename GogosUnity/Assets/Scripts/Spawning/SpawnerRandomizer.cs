using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class SpawnerRandomizer : MonoBehaviour
    {
        [SerializeField]
        private Spawner[] m_Spawners;

        [SerializeField]
        private Spawnable[] m_Spawnables;

        private const int TurnsToSpawn = PlayerTracker.PlayerCount + 1;
        private const int MaxSpawners = PlayerTracker.PlayerCount;

        private Queue<Spawner> m_RandomRemainingSpawners = new Queue<Spawner>();

        private void Start()
        {
            TurnTracker.TurnChanged += TurnTracker_OnTurnChanged;

            RandomizeAndSpawn();
        }

        private void OnDestroy()
        {
            TurnTracker.TurnChanged -= TurnTracker_OnTurnChanged;
        }

        private void TurnTracker_OnTurnChanged()
        {
            if (TurnTracker.Turn % TurnsToSpawn == 0)
            {
                RandomizeAndSpawn();
            }
        }

        private void RandomizeAndSpawn()
        {
            var randomSpawnerCount = Random.Range(1, MaxSpawners + 1);
            for (int i = 0; i < randomSpawnerCount; i++)
            {
                if (m_RandomRemainingSpawners.Count == 0)
                {
                    var random = new System.Random();
                    m_RandomRemainingSpawners = new Queue<Spawner>(m_Spawners.OrderBy(s => random.Next()));
                }
                var randomSpawner = m_RandomRemainingSpawners.Dequeue();
                randomSpawner.RandomlySpawn(m_Spawnables);
            }
        }
    }
}
