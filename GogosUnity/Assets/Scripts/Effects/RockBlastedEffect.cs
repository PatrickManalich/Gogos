using UnityEngine;

namespace Gogos
{
    public class RockBlastedEffect : AbstractBlastedEffect
    {
        [SerializeField]
        private GameObject m_Root;

        [SerializeField]
        private ChildRigidbodyConstrainer m_ChildRigidbodyConstrainer;

        [SerializeField]
        private GameObject[] m_GemPoints;

        [SerializeField]
        private GameObject m_Explosion;

        [SerializeField]
        private float m_ExplosionScalar;

        private const float ExplosionDestroyDelay = 2;

        protected override void OnBlasted(BlastTriggerEventArgs e)
        {
            m_ChildRigidbodyConstrainer.Release();
            foreach (var gemPoint in m_GemPoints)
            {
                var gem = gemPoint.transform.GetChild(0);
                gem.transform.parent = m_Root.transform.parent;
            }
            var explosionInstance = Instantiate(m_Explosion, transform.position, transform.rotation);
            explosionInstance.transform.localScale = explosionInstance.transform.localScale * m_ExplosionScalar;
            Destroy(explosionInstance, ExplosionDestroyDelay);
            Destroy(m_Root);
        }
    }
}
