using System.Collections.Generic;

namespace Gogos
{
    public enum Situation { Available, Launched, Collected }

    public class GogoSituationDatabase : AbstractSingleton<GogoSituationDatabase>
    {
        private static Dictionary<string, Situation> s_SituationsById = new Dictionary<string, Situation>();

        protected override void Awake()
        {
            base.Awake();
            s_SituationsById.Clear();
        }

        private void Start()
        {
            foreach (var player in PlayerTracker.Players)
            {
                foreach (var identifiableGogo in player.Collection.IdentifiableGogos)
                {
                    s_SituationsById.Add(identifiableGogo.Id, Situation.Available);
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

        public static Situation GetSituation(IdentifiableGogo identifiableGogo)
        {
            return s_SituationsById[identifiableGogo.Id];
        }

        public static void SetSituation(IdentifiableGogo identifiableGogo, Situation situation)
        {
            s_SituationsById[identifiableGogo.Id] = situation;
        }

        private void Collection_OnGogoAdded(object sender, IdentifiableGogoEventArgs e)
        {
            s_SituationsById.Add(e.IdentifiableGogo.Id, Situation.Available);
        }
    }
}
