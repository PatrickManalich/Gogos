using Gogos.Extensions;
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
        private const float MinAttractPower = 0;
        private const float MaxAttractPower = 10;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var shieldTrigger = e.OtherCollider.GetComponent<ShieldTrigger>();
            if (shieldTrigger == null)
            {
                return;
            }

            var shieldStrengthTierTracker = shieldTrigger.ShieldStrengthTierTracker;
            if (shieldStrengthTierTracker.IsShieldBroken)
            {
                return;
            }

            var shieldAbility = shieldTrigger.ShieldAbility;
            if (shieldAbility.CanDeflect(GroupTag, Player))
            {
                var outwardDirection = (transform.position - shieldTrigger.CenterPosition).normalized;
                var outwardForce = outwardDirection * DeflectPower;
                m_Rigidbody.AddForceAtPosition(outwardForce, m_ForcePoint.transform.position, ForceMode.Impulse);

                var modifiedThisTurn = shieldStrengthTierTracker.LastTurnModified != TurnTracker.Turn;
                if (!shieldStrengthTierTracker.IsShieldUnbreakable && GroupTag == GroupTag.Gogo && !modifiedThisTurn)
                {
                    shieldStrengthTierTracker.ModifyTier(-1);
                }
            }
            else if (shieldAbility.CanAttract(GroupTag, Player))
            {
                var centerPosition = shieldTrigger.CenterPosition;
                var radius = shieldTrigger.RangeTierTracker.Range / 2;
                var collisionHeight = Mathf.Clamp(transform.position.y - centerPosition.y, 0, radius);
                var attractPower = collisionHeight.ConvertValueToDifferentRange(0, radius, MaxAttractPower, MinAttractPower);
                var inwardDirection = (centerPosition - transform.position).normalized;
                var inwardForce = inwardDirection * attractPower;
                m_Rigidbody.velocity = m_Rigidbody.velocity.WithXZ(0, 0);
                m_Rigidbody.AddForceAtPosition(inwardForce, m_ForcePoint.transform.position, ForceMode.Impulse);
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
