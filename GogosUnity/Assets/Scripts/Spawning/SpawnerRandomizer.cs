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

        private const int TurnsToSpawn = PlayerTracker.PlayerCount * 3 + 1;
        private const int MaxSpawners = PlayerTracker.PlayerCount;
        private const int MinSpawners = 6;

        private bool m_IsFirstSpawn;
        private Queue<Spawner> m_UnusedSpawners = new Queue<Spawner>();

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            m_IsFirstSpawn = true;
            m_Spawners.ToList().ForEach(s => s.HideSpawnMarker());
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Spawning)
            {
                if ((TurnTracker.Turn % TurnsToSpawn) - 1 == 0)
                {
                    StartCoroutine(SpawnRoutine());
                }
                else
                {
                    Skipped?.Invoke();
                }
            }
        }

        private IEnumerator SpawnRoutine()
        {
            var activeSpawners = new Queue<Spawner>();
            if (m_IsFirstSpawn)
            {
                m_Spawners.ToList().ForEach(s => activeSpawners.Enqueue(s));
                m_IsFirstSpawn = false;
            }
            else
            {
                var randomSpawnerCount = UnityEngine.Random.Range(MinSpawners, MaxSpawners + 1);
                for (int i = 0; i < randomSpawnerCount; i++)
                {
                    if (m_UnusedSpawners.Count == 0)
                    {
                        var random = new System.Random();
                        m_UnusedSpawners = new Queue<Spawner>(m_Spawners.OrderBy(s => random.Next()));
                    }

                    var activeSpawner = m_UnusedSpawners.Dequeue();
                    activeSpawners.Enqueue(activeSpawner);
                }
            }

            while (activeSpawners.Count > 0)
            {
                var activeSpawner = activeSpawners.Dequeue();
                activeSpawner.ShowSpawnMarker();
                yield return activeSpawner.RandomlySpawn(m_Spawnables);
                activeSpawner.Spawn(m_SpawnableGogos.GetRandomScriptableGogo());
                activeSpawner.HideSpawnMarker();
            }

            Spawned?.Invoke();
        }
    }
}
