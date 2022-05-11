using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class PlayerGogoReturner : MonoBehaviour
    {
        public event Action Returned;

        public event Action Skipped;

        [SerializeField]
        private GameObject m_Announcement;

        private List<Player> m_PlayersReadyToReturn = new List<Player>();

        private void Start()
        {
            ObjectiveTracker.ObjectiveChanged += ObjectiveTracker_OnObjectiveChanged;
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
            ObjectiveTracker.ObjectiveChanged -= ObjectiveTracker_OnObjectiveChanged;
        }

        private void ObjectiveTracker_OnObjectiveChanged()
        {
            m_PlayersReadyToReturn = PlayerTracker.Players.ToList();
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.GogoReturning)
            {
                var identifiableGogos = PlayerTracker.Player.Collection.IdentifiableGogos;
                var areAnyGogosAvailable = identifiableGogos.Any(i => GogoSituationDatabase.GetSituation(i) == Situation.Available);
                if (!areAnyGogosAvailable || m_PlayersReadyToReturn.Contains(PlayerTracker.Player))
                {
                    m_PlayersReadyToReturn.Remove(PlayerTracker.Player);
                    StartCoroutine(FlashTextAndReturnRoutine());
                }
                else
                {
                    Skipped?.Invoke();
                }
            }
        }

        private IEnumerator FlashTextAndReturnRoutine()
        {
            m_Announcement.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);

            var playersGogos = FindObjectsOfType<AbstractGogo>().Where(g => g.Player == PlayerTracker.Player);
            foreach (var playerGogo in playersGogos)
            {
                Destroy(playerGogo.gameObject);
            }
            foreach (var identifiableGogo in PlayerTracker.Player.Collection.IdentifiableGogos)
            {
                GogoSituationDatabase.SetSituation(identifiableGogo, Situation.Available);
            }
            yield return new WaitForSeconds(1);

            m_Announcement.SetActive(false);
            Returned?.Invoke();
        }
    }
}
