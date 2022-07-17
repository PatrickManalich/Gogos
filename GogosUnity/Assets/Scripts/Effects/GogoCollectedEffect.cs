using UnityEngine;

namespace Gogos
{
    public class GogoCollectedEffect : AbstractCollectedEffect
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

        [SerializeField]
        private SparkleVisual m_SparkleVisual;

        private const int EnemyGogoGemMultiplier = 3;

        protected override void OnCollected()
        {
            if (PhaseTracker.Phase != Phase.Settling)
            {
                return;
            }

            if (m_Gogo.Player == null)
            {
                PlayerTracker.Player.Collection.Add(m_Gogo.IdentifiableGogo);
            }
            else if (m_Gogo.Player != PlayerTracker.Player)
            {
                var gogoClass = m_Gogo.IdentifiableGogo.ScriptableGogo.GogoClass;
                var gemValueTierTracker = (GemValueTierTracker)m_Gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.GemValue);
                var gems = gemValueTierTracker.GemValue * EnemyGogoGemMultiplier;
                PlayerTracker.Player.Collection.Add(gogoClass, gems);
            }
            GogoSituationDatabase.SetSituation(m_Gogo.IdentifiableGogo, Situation.Available);
            m_SparkleVisual.StopAndDestroy();
        }
    }
}
