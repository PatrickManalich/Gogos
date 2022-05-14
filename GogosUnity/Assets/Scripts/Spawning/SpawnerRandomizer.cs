using Gogos.Extensions;
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

        public AbstractScriptableGogo GoldenScriptableGogo { get; private set; }

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
        private List<AbstractScriptableGogo> m_UnusedGoldenScriptableGogos = new List<AbstractScriptableGogo>();

        private void Awake()
        {
            SetGoldenScriptableGogo();
        }

        private void Start()
        {
            ObjectiveTracker.ObjectiveChanged += ObjectiveTracker_OnObjectiveChanged;
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            m_ReadyToSpawn = true;
            m_Spawners.ToList().ForEach(s => s.HideVisual());
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
            ObjectiveTracker.ObjectiveChanged -= ObjectiveTracker_OnObjectiveChanged;
        }

        private void ObjectiveTracker_OnObjectiveChanged()
        {
            m_ReadyToSpawn = true;
            if (ObjectiveTracker.Objective == Objective.Collect)
            {
                SetGoldenScriptableGogo();
            }
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
                activeSpawner.Spawn(GoldenScriptableGogo);
                activeSpawner.HideVisual();
            }
            yield return new WaitForSeconds(2);

            Spawned?.Invoke();
        }

        private void SetGoldenScriptableGogo()
        {
            if (m_UnusedGoldenScriptableGogos.Count == 0)
            {
                m_UnusedGoldenScriptableGogos = m_GoldenGogos.ScriptableGogos.ToList();
            }
            GoldenScriptableGogo = m_UnusedGoldenScriptableGogos.GetRandomAndRemove();
        }
    }
}
