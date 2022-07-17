using UnityEngine;

namespace Gogos
{
    public class SparkleVisual : MonoBehaviour
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

        [SerializeField]
        private GameObject m_Visual;

        private const float DestroyDelay = 2;

        private ParticleSystem[] m_ParticleSystems;

        private void Update()
        {
            m_Visual.SetActive(m_Gogo.Player == null);
        }

        public void SetColor(Color color)
        {
            foreach (var particleSystem in GetParticleSystems())
            {
                var main = particleSystem.main;
                main.startColor = color;
            }
        }

        public void StopAndDestroy()
        {
            foreach (var particleSystem in GetParticleSystems())
            {
                particleSystem.Stop();
            }
            transform.parent = null;
            Destroy(gameObject, DestroyDelay);
        }

        private ParticleSystem[] GetParticleSystems()
        {
            if (m_ParticleSystems == null)
            {
                m_ParticleSystems = m_Visual.GetComponentsInChildren<ParticleSystem>(true);
            }
            return m_ParticleSystems;
        }
    }
}
