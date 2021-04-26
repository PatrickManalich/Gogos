using UnityEngine;

namespace Gogos
{
	public class BlastableAttribute : MonoBehaviour
	{
        private const float FalloffRate = 1.5f;

        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_ForcePoint;

        private void Awake()
		{
            m_TriggerListener.Entered += TriggerListener_OnEntered;
		}

        private void OnDestroy()
		{
			m_TriggerListener.Entered -= TriggerListener_OnEntered;
		}

		private void TriggerListener_OnEntered(object sender, TriggerEventArgs e)
		{
            var blastTrigger = e.OtherCollider.GetComponent<BlastTrigger>();
            if (blastTrigger)
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
    }
}