using System.Collections.Generic;

namespace Gogos
{
    public enum Situation { Available, InRing, OffRing }

    public class GogoSituationDatabase : AbstractSingleton<GogoSituationDatabase>
    {
        private Dictionary<string, Situation> m_SituationsById = new Dictionary<string, Situation>();

        private void Start()
        {
            foreach (var player in PlayerTracker.Players)
            {
                foreach (var identifiableGogo in player.Collection.IdentifiableGogos)
                {
                    m_SituationsById.Add(identifiableGogo.Id, Situation.Available);
                }
                player.Collection.GogoAdded += Collection_OnGogoAdded;
            }
        }

        private void OnDestroy()
        {
            foreach (var player in PlayerTracker.Players)
            {
                player.Collection.GogoAdded -= Collection_OnGogoAdded;
            }
        }

        public Situation GetSituation(IdentifiableGogo identifiableGogo)
        {
            return m_SituationsById[identifiableGogo.Id];
        }

        public void SetSituation(IdentifiableGogo identifiableGogo, Situation situation)
        {
            m_SituationsById[identifiableGogo.Id] = situation;
        }

        private void Collection_OnGogoAdded(object sender, IdentifiableGogoEventArgs e)
        {
            m_SituationsById.Add(e.IdentifiableGogo.Id, Situation.Available);
        }
    }
}
