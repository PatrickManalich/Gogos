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

        [SerializeField]
        private ScriptableGogoBucket m_SpawnableGogos;

        private const int TurnsToSpawn = PlayerTracker.PlayerCount * 2 + 1;
        private const int MaxSpawners = PlayerTracker.PlayerCount;
        private const int MinSpawners = 6;

        private Queue<Spawner> m_UnusedSpawners = new Queue<Spawner>();
        private Queue<Spawner> m_NextSpawners = new Queue<Spawner>();

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            foreach (var spawner in m_Spawners)
            {
                spawner.ShowSpawnMarker();
                m_NextSpawners.Enqueue(spawner);
            }

            StartCoroutine(SpawnNextSpawnersAndRefill());
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
                    StartCoroutine(SpawnNextSpawnersAndRefill());
                }
                else
                {
                    Skipped?.Invoke();
                }
            }
        }

        private IEnumerator SpawnNextSpawnersAndRefill()
        {
            while (m_NextSpawners.Count > 0)
            {
                var spawner = m_NextSpawners.Dequeue();
                yield return spawner.RandomlySpawn(m_Spawnables);
                spawner.Spawn(m_SpawnableGogos.GetRandomScriptableGogo());
                spawner.HideSpawnMarker();
            }
            yield return new WaitForSeconds(0.25f);

            var randomSpawnerCount = UnityEngine.Random.Range(MinSpawners, MaxSpawners + 1);
            for (int i = 0; i < randomSpawnerCount; i++)
            {
                if (m_UnusedSpawners.Count == 0)
                {
                    var random = new System.Random();
                    m_UnusedSpawners = new Queue<Spawner>(m_Spawners.OrderBy(s => random.Next()));
                }

                var spawner = m_UnusedSpawners.Dequeue();
                spawner.ShowSpawnMarker();
                m_NextSpawners.Enqueue(spawner);
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.25f);

            Spawned?.Invoke();
        }
    }
}
