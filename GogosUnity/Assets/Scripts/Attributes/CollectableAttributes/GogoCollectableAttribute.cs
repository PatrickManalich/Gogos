using UnityEngine;

namespace Gogos
{
    public class GogoCollectableAttribute : AbstractCollectableAttribute
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

        private const int EnemyGogoPointValue = 500;

        protected override void Collect()
        {
            if (m_Gogo.Player != PlayerTracker.Player)
            {
                PlayerTracker.Player.AddPoints(EnemyGogoPointValue);
            }
            GogoSituationDatabase.Instance.SetSituation(m_Gogo.IdentifiableGogo, Situation.OffRing);
        }
    }
}
