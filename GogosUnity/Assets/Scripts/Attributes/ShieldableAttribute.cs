using UnityEngine;

namespace Gogos
{
    public class ShieldableAttribute : AbstractAttribute
    {
        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_ForcePoint;

        private const float DeflectPower = 10;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var shieldTrigger = e.OtherCollider.GetComponent<ShieldTrigger>();
            if (shieldTrigger != null)
            {
                var shieldAbility = shieldTrigger.ShieldAbility;
                if (shieldAbility.CanDeflect(Player))
                {
                    var awayFromCenterDirection = (transform.position - shieldTrigger.CenterPosition).normalized;
                    m_Rigidbody.AddForceAtPosition(awayFromCenterDirection * DeflectPower, m_ForcePoint.transform.position, ForceMode.Impulse);

                    var shieldStrengthTierTracker = shieldTrigger.ShieldStrengthTierTracker;
                    if (shieldStrengthTierTracker.LastTurnModified != TurnTracker.Turn)
                    {
                        shieldStrengthTierTracker.ModifyTier(-1);
                    }
                }
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
