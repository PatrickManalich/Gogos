using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class SpawnerRandomizer : MonoBehaviour
    {
        public event Action Spawned;

        public event Action Skipped;

        [SerializeField]
        private Spawner[] m_Spawners;

        [SerializeField]
        private Spawnable[] m_Spawnables;

        private const int TurnsToSpawn = PlayerTracker.PlayerCount + 1;
        private const int MaxSpawners = PlayerTracker.PlayerCount;

        private Queue<Spawner> m_RandomRemainingSpawners = new Queue<Spawner>();

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            StartCoroutine(RandomizeAndSpawn());
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Spawning)
            {
                if (TurnTracker.Turn % TurnsToSpawn == 0)
                {
                    StartCoroutine(RandomizeAndSpawn());
                }
                else
                {
                    Skipped?.Invoke();
                }
            }
        }

        private IEnumerator RandomizeAndSpawn()
        {
            var randomSpawnerCount = UnityEngine.Random.Range(1, MaxSpawners + 1);
            for (int i = 0; i < randomSpawnerCount; i++)
            {
                if (m_RandomRemainingSpawners.Count == 0)
                {
                    var random = new System.Random();
                    m_RandomRemainingSpawners = new Queue<Spawner>(m_Spawners.OrderBy(s => random.Next()));
                }
                var randomSpawner = m_RandomRemainingSpawners.Dequeue();
                yield return randomSpawner.RandomlySpawn(m_Spawnables);
            }

            yield return new WaitForSeconds(1);
            Spawned?.Invoke();
        }
    }
}
