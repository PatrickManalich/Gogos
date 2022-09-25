﻿using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class RockBlastedEffect : AbstractBlastedEffect
    {
        [SerializeField]
        private GameObject m_Root;

        [SerializeField]
        private GemSlot[] m_GemSlots;

        [SerializeField]
        private GameObject m_Explosion;

        [SerializeField]
        private float m_ExplosionScalar;

        private const float ExplosionDestroyDelay = 2;

        private void Awake()
        {
            foreach (var gemSlot in m_GemSlots)
            {
                gemSlot.ReleaseParent = m_Root.transform.parent;
            }
        }

        protected override void OnBlasted(BlastTriggerEventArgs e)
        {
            foreach (var gemSlot in m_GemSlots.Where(g => !g.HasReleased))
            {
                gemSlot.ApplyForce(e.Force);
            }

            var explosionInstance = Instantiate(m_Explosion, transform.position, transform.rotation);
            explosionInstance.transform.localScale = explosionInstance.transform.localScale * m_ExplosionScalar;
            Destroy(explosionInstance, ExplosionDestroyDelay);

            if (m_GemSlots.All(g => g.HasReleased))
            {
                Destroy(m_Root);
            }
        }
    }
}
