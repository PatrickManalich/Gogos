﻿using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Gogos
{
    public class PlayerGogoReturner : MonoBehaviour
    {
        public event Action Returned;

        public event Action Skipped;

        [SerializeField]
        private TextMeshProUGUI m_ReturningText;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Returning)
            {
                var identifiableGogos = PlayerTracker.Player.Collection.IdentifiableGogos;
                if (!identifiableGogos.Any(i => GogoSituationDatabase.GetSituation(i) == Situation.Available))
                {
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
            m_ReturningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);

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

            m_ReturningText.gameObject.SetActive(false);
            Returned?.Invoke();
        }
    }
}
