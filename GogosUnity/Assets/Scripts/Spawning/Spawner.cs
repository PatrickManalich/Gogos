using Gogos.Extensions;
using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private int m_MaxSpawnAmount;

        [SerializeField]
        private float m_SpawnRadius;

        public void RandomlySpawn(Spawnable[] spawnables)
        {
            StartCoroutine(RandomlySpawnRoutine(spawnables));
        }

        private IEnumerator RandomlySpawnRoutine(Spawnable[] spawnables)
        {
            for (int i = 0; i < m_MaxSpawnAmount; i++)
            {
                var randomSpawnable = spawnables[Random.Range(0, spawnables.Length)];
                var randomChance = Random.Range(0, 100 + 1);
                if (randomChance <= randomSpawnable.SpawnChance)
                {
                    var spawnableInstance = Instantiate(randomSpawnable.Prefab, transform);
                    var randomPositionInCircle = Quaternion.Euler(90, 0, 0) * Random.insideUnitCircle * m_SpawnRadius;
                    spawnableInstance.transform.position = transform.position + randomPositionInCircle;
                    spawnableInstance.transform.rotation = Random.rotation;
                    spawnableInstance.name = spawnableInstance.name.Replace("(Clone)", "");
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue.WithA(0.3f);
            Gizmos.DrawSphere(transform.position, m_SpawnRadius);
        }
    }
}
