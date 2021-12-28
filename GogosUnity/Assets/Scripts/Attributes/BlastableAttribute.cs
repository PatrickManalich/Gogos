using UnityEngine;

namespace Gogos
{
    public class BlastableAttribute : AbstractAttribute
    {
        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_ForcePoint;

        private const float FalloffRate = 0.4f;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var blastTrigger = e.OtherCollider.GetComponent<BlastTrigger>();
            if (blastTrigger != null)
            {
                var blastCenterPosition = e.OtherCollider.transform.position;
                var oppositeDirection = (transform.position - blastCenterPosition).normalized;
                var upwardsDirection = Vector3.up * blastTrigger.BlastForceTierTracker.BlastUpwardsModifier;
                var forceDirection = oppositeDirection + upwardsDirection;

                var distance = Vector3.Distance(transform.position, blastCenterPosition);
                var distanceMultiplier = Mathf.Max(0, blastTrigger.BlastForceTierTracker.BlastForce - FalloffRate * distance);

                m_Rigidbody.AddForceAtPosition(forceDirection * distanceMultiplier, m_ForcePoint.transform.position, ForceMode.Impulse);
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
