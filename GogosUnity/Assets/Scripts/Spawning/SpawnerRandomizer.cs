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
        private Spawner m_GoldenSpawner;

        [SerializeField]
        private Spawnable[] m_Spawnables;

        [SerializeField]
        private ScriptableGogoBucket m_SpawnableGogos;

        [SerializeField]
        private ScriptableGogoBucket m_GoldenGogos;

        private bool m_ReadyToSpawn;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
            ObjectiveTracker.ObjectiveChanged += ObjectiveTracker_OnObjectiveChanged;

            m_ReadyToSpawn = true;
            m_Spawners.ToList().ForEach(s => s.HideVisual());
            StartCoroutine(SpawnRoutine());
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
            if (ObjectiveTracker.Objective == Objective.Collect)
            {
                var activeSpawners = new Queue<Spawner>(m_Spawners);
                while (activeSpawners.Count > 0)
                {
                    var activeSpawner = activeSpawners.Dequeue();
                    activeSpawner.ShowVisual();
                    yield return activeSpawner.RandomlySpawn(m_Spawnables);
                    activeSpawner.Spawn(m_SpawnableGogos.GetRandomScriptableGogo());
                    activeSpawner.HideVisual();
                }
            }
            else
            {
                var activeSpawner = m_GoldenSpawner;
                activeSpawner.ShowVisual();
                yield return activeSpawner.RandomlySpawn(m_Spawnables);
                activeSpawner.Spawn(m_GoldenGogos.GetRandomScriptableGogo());
                activeSpawner.HideVisual();
            }
            yield return new WaitForSeconds(2);

            Spawned?.Invoke();
        }
    }
}
