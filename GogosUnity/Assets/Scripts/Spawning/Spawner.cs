﻿using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private int m_MaxSpawnAmount;

        [SerializeField]
        private float m_SpawnRadius;

        [SerializeField]
        private GameObject m_SpawnArea;

        [SerializeField]
        private Animator m_VisualAnimator;

        private const string SpawnName = "Spawn";

        private void Awake()
        {
            m_SpawnArea.transform.localScale = 2 * m_SpawnRadius * Vector3.one;
        }

        public void ShowVisual()
        {
            m_VisualAnimator.SetBool(SpawnName, true);
        }

        public void HideVisual()
        {
            m_VisualAnimator.SetBool(SpawnName, false);
        }

        public void Spawn(AbstractScriptableGogo scriptableGogo)
        {
            var identifiableGogo = new IdentifiableGogo(scriptableGogo);
            var gogo = Spawn(identifiableGogo.ScriptableGogo.Prefab).GetComponent<AbstractGogo>();
            gogo.SetTiers(identifiableGogo);
            gogo.enabled = identifiableGogo.ScriptableGogo.GogoClass == GogoClass.Golden;
        }

        public Coroutine RandomlySpawn(Spawnable[] spawnables)
        {
            return StartCoroutine(RandomlySpawnRoutine(spawnables));
        }

        private GameObject Spawn(GameObject prefab)
        {
            var instance = Instantiate(prefab, transform);
            instance.name = instance.name.Replace("(Clone)", "");
            var randomPositionInCircle = Quaternion.Euler(90, 0, 0) * Random.insideUnitCircle * m_SpawnRadius;
            instance.transform.SetPositionAndRotation(transform.position + randomPositionInCircle, Random.rotation);
            return instance;
        }

        private IEnumerator RandomlySpawnRoutine(Spawnable[] spawnables)
        {
            for (int i = 0; i < m_MaxSpawnAmount; i++)
            {
                var randomSpawnable = spawnables[Random.Range(0, spawnables.Length)];
                var randomChance = Random.Range(0, 100 + 1);
                if (randomChance <= randomSpawnable.SpawnChance)
                {
                    Spawn(randomSpawnable.Prefab);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
}
