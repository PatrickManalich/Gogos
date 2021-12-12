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

        [SerializeField]
        private GameObject m_SpawnMarker;

        private void Start()
        {
            m_SpawnMarker.transform.localScale = 2 * m_SpawnRadius * Vector3.one;
        }

        public void ShowSpawnMarker()
        {
            m_SpawnMarker.SetActive(true);
        }

        public void HideSpawnMarker()
        {
            m_SpawnMarker.SetActive(false);
        }

        public void Spawn(AbstractScriptableGogo scriptableGogo)
        {
            var identifiableGogo = new IdentifiableGogo(scriptableGogo);
            var gogoInstance = Instantiate(identifiableGogo.ScriptableGogo.Prefab).GetComponent<AbstractGogo>();
            gogoInstance.SetTiers(identifiableGogo);
            gogoInstance.enabled = false;
            PlaceInsideCircle(gogoInstance.gameObject);
        }

        public Coroutine RandomlySpawn(Spawnable[] spawnables)
        {
            return StartCoroutine(RandomlySpawnRoutine(spawnables));
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
                    PlaceInsideCircle(spawnableInstance);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private void PlaceInsideCircle(GameObject instance)
        {
            instance.name = instance.name.Replace("(Clone)", "");
            var randomPositionInCircle = Quaternion.Euler(90, 0, 0) * Random.insideUnitCircle * m_SpawnRadius;
            instance.transform.SetPositionAndRotation(transform.position + randomPositionInCircle, Random.rotation);
        }
    }
}
