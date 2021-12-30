using UnityEngine;

namespace Gogos
{
    public class GogoCollectedEffect : AbstractCollectedEffect
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

        private const int EnemyGogoPointValue = 500;

        protected override void OnCollected()
        {
            if (m_Gogo.Player == null)
            {
                PlayerTracker.Player.Collection.Add(m_Gogo.IdentifiableGogo);
            }
            else if (m_Gogo.Player != PlayerTracker.Player)
            {
                PlayerTracker.Player.AddPoints(EnemyGogoPointValue);
            }
            GogoSituationDatabase.SetSituation(m_Gogo.IdentifiableGogo, Situation.OffRing);
        }
    }
}
