using Gogos.Extensions;
using System;
using UnityEngine;

namespace Gogos
{
    public class BlastableAttribute : AbstractAttribute
    {
        public event EventHandler<BlastTriggerEventArgs> Blasted;

        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_ForcePoint;

        private const float MinBlastOutwardPower = 8;
        private const float MinBlastUpwardPower = 10;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var blastTrigger = e.OtherCollider.GetComponent<BlastTrigger>();
            if (blastTrigger == null)
            {
                return;
            }

            if (blastTrigger.Player != null && blastTrigger.Player == Player)
            {
                return;
            }

            var centerPosition = blastTrigger.CenterPosition;
            var distance = Vector3.Distance(transform.position, centerPosition);
            var radius = blastTrigger.RangeTierTracker.Range / 2;

            var blastOutwardPower = distance.ConvertValueToDifferentRange(0, radius, blastTrigger.BlastPowerTierTracker.BlastOutwardPower, MinBlastOutwardPower);
            var outwardDirection = (transform.position - centerPosition).normalized;
            outwardDirection = outwardDirection.WithY(Mathf.Abs(outwardDirection.y));   // Prevent downward blasts
            var outwardForce = outwardDirection * blastOutwardPower;

            var blastUpwardPower = distance.ConvertValueToDifferentRange(0, radius, blastTrigger.BlastPowerTierTracker.BlastUpwardPower, MinBlastUpwardPower);
            var upwardForce = Vector3.up * blastUpwardPower;

            var force = outwardForce + upwardForce;
            m_Rigidbody.AddForceAtPosition(force, m_ForcePoint.transform.position, ForceMode.Impulse);
            Blasted?.Invoke(this, new BlastTriggerEventArgs(blastTrigger, force));
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
