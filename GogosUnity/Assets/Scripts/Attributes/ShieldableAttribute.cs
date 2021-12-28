using UnityEngine;

namespace Gogos
{
    public class ShieldableAttribute : AbstractAttribute
    {
        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_ForcePoint;

        private const float DeflectForce = 10;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var shieldTrigger = e.OtherCollider.GetComponent<ShieldTrigger>();
            if (shieldTrigger != null && Player != null && shieldTrigger.Player != Player)
            {
                var shieldPosition = e.OtherCollider.transform.position;
                var oppositeDirection = (transform.position - shieldPosition).normalized;

                m_Rigidbody.AddForceAtPosition(oppositeDirection * DeflectForce, m_ForcePoint.transform.position, ForceMode.Impulse);
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
