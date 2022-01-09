﻿using Gogos.Extensions;
using System;
using UnityEngine;

namespace Gogos
{
    public class BlastableAttribute : AbstractAttribute
    {
        public event Action Blasted;

        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_ForcePoint;

        private const float MinBlastOutwardPower = 8;
        private const float MinBlastUpwardPower = 10;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var blastTrigger = e.OtherCollider.GetComponent<BlastTrigger>();
            if (blastTrigger != null && (blastTrigger.Player == null || blastTrigger.Player != Player))
            {
                Blasted?.Invoke();
                var centerPosition = blastTrigger.CenterPosition;
                var distance = Vector3.Distance(transform.position, centerPosition);
                var radius = blastTrigger.RangeTierTracker.Range / 2;

                var blastOutwardPower = distance.ConvertValueToDifferentRange(0, radius, blastTrigger.BlastPowerTierTracker.BlastOutwardPower, MinBlastOutwardPower);
                var outwardDirection = (transform.position - centerPosition).normalized;
                var outwardForce = outwardDirection * blastOutwardPower;

                var blastUpwardPower = distance.ConvertValueToDifferentRange(0, radius, blastTrigger.BlastPowerTierTracker.BlastUpwardPower, MinBlastUpwardPower);
                var upwardForce = Vector3.up * blastUpwardPower;

                var force = outwardForce + upwardForce;
                m_Rigidbody.AddForceAtPosition(force, m_ForcePoint.transform.position, ForceMode.Impulse);
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
