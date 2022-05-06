﻿using System;
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

        [SerializeField]
        private ScriptableGogoBucket m_GoldenGogos;

        private const int MaxSpawners = PlayerTracker.PlayerCount;
        private const int MinSpawners = 6;

        private bool m_IsFirstSpawn;
        private bool m_ReadyToSpawn;
        private Queue<Spawner> m_UnusedSpawners = new Queue<Spawner>();

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
            ObjectiveTracker.ObjectiveChanged += ObjectiveTracker_OnObjectiveChanged;

            m_IsFirstSpawn = true;
            m_ReadyToSpawn = true;
            m_Spawners.ToList().ForEach(s => s.HideVisual());
        }

        private void OnDestroy()
        {
            ObjectiveTracker.ObjectiveChanged -= ObjectiveTracker_OnObjectiveChanged;
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void ObjectiveTracker_OnObjectiveChanged()
        {
            m_ReadyToSpawn = true;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Spawning)
            {
                if (m_ReadyToSpawn)
                {
                    m_ReadyToSpawn = false;
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
                activeSpawner.ShowVisual();
                yield return activeSpawner.RandomlySpawn(m_Spawnables);
                activeSpawner.Spawn(m_SpawnableGogos.GetRandomScriptableGogo());
                if (activeSpawners.Count == 0)
                {
                    activeSpawner.Spawn(m_GoldenGogos.GetRandomScriptableGogo());
                }
                activeSpawner.HideVisual();
            }
            yield return new WaitForSeconds(2);

            Spawned?.Invoke();
        }
    }
}
