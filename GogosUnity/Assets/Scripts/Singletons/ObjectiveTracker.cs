using System;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public enum Objective { Collect, KnockOut }

    public class ObjectiveTracker : AbstractSingleton<TurnTracker>
    {
        public static event Action ObjectiveChanged;

        public static Objective Objective { get; private set; }

        public static int SwitchObjectivesTurn { get; private set; }

        [SerializeField]
        private SpawnerRandomizer m_SpawnerRandomizer;

        private CollectableAttribute m_GoldenGogoCollectableAttribute;

        protected override void Awake()
        {
            base.Awake();
            Objective = Objective.Collect;
            SwitchObjectivesTurn = PlayerTracker.PlayerCount * 3 + 1;
        }

        private void Start()
        {
            TurnTracker.TurnChanged += TurnTracker_OnTurnChanged;
            m_SpawnerRandomizer.Spawned += SpawnerRandomizer_OnSpawned;
        }

        private void OnDestroy()
        {
            ClearAttributeAndUnsubscribe();
            m_SpawnerRandomizer.Spawned -= SpawnerRandomizer_OnSpawned;
            TurnTracker.TurnChanged -= TurnTracker_OnTurnChanged;
        }

        public static void OverrideObjective(Objective objective)
        {
            Objective = objective;
            ObjectiveChanged?.Invoke();
        }

        private void TurnTracker_OnTurnChanged()
        {
            if (Objective == Objective.Collect && TurnTracker.Turn == SwitchObjectivesTurn)
            {
                Objective = Objective.KnockOut;
                ObjectiveChanged?.Invoke();
            }
        }

        private void SpawnerRandomizer_OnSpawned()
        {
            if (Objective == Objective.KnockOut && m_GoldenGogoCollectableAttribute == null)
            {
                FindAttributeAndSubscribe();
            }
        }

        private void GoldenGogoCollectableAttribute_OnCollected()
        {
            ClearAttributeAndUnsubscribe();
            SwitchObjectivesTurn = TurnTracker.Turn + PlayerTracker.PlayerCount * 3 + 1;
            Objective = Objective.Collect;
            ObjectiveChanged?.Invoke();
        }

        private void FindAttributeAndSubscribe()
        {
            var goldenGogo = FindObjectsOfType<AbstractGogo>().First(g => g.IdentifiableGogo.ScriptableGogo.GogoClass == GogoClass.Golden);
            m_GoldenGogoCollectableAttribute = goldenGogo.GetComponentInChildren<CollectableAttribute>();
            m_GoldenGogoCollectableAttribute.Collected += GoldenGogoCollectableAttribute_OnCollected;
        }

        private void ClearAttributeAndUnsubscribe()
        {
            if (m_GoldenGogoCollectableAttribute != null)
            {
                m_GoldenGogoCollectableAttribute.Collected -= GoldenGogoCollectableAttribute_OnCollected;
                m_GoldenGogoCollectableAttribute = null;
            }
        }
    }
}
