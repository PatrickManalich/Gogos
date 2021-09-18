using UnityEngine;

namespace Gogos
{
    public class ShieldableAttribute : MonoBehaviour
    {
        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_ForcePoint;

        private const float DeflectForce = 10;

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
            var shieldTrigger = e.OtherCollider.GetComponent<ShieldTrigger>();
            if (shieldTrigger)
            {
                var shieldPosition = e.OtherCollider.transform.position;
                var oppositeDirection = (transform.position - shieldPosition).normalized;

                m_Rigidbody.AddForceAtPosition(oppositeDirection * DeflectForce, m_ForcePoint.transform.position, ForceMode.Impulse);
            }
        }
    }
}
