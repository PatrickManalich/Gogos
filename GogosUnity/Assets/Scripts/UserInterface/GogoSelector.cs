using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gogos
{
    public class GogoSelector : MonoBehaviour
    {
        [SerializeField]
        private ToggleGroup m_ToggleGroup;

        [SerializeField]
        private GameObject m_GogoSelectionTogglePrefab;

        [SerializeField]
        private GogoDetailsPanel m_GogoDetailsPanel;

        [SerializeField]
        private GogoCreator m_GogoCreator;

        private List<GogoSelectionToggle> m_GogoSelectionToggles = new List<GogoSelectionToggle>();
        private IdentifiableGogo m_SelectedIdentifiableGogo;

        private IEnumerator Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
            yield return null;  // Allow GogoSituationDatabase to initialize

            RefreshGogoSelectionToggles();
        }

        private void OnDestroy()
        {
            UnsubscribeFromSelectedEvents();
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Selecting)
            {
                RefreshGogoSelectionToggles();
            }
        }

        private void GogoSelectionToggle_OnGogoSelected(object sender, IdentifiableGogoEventArgs e)
        {
            m_SelectedIdentifiableGogo = e.IdentifiableGogo;

            m_GogoDetailsPanel.SetDetails(m_SelectedIdentifiableGogo.ScriptableGogo, PlayerTracker.Player);
            m_GogoCreator.CreateGogo(m_SelectedIdentifiableGogo);
        }

        private void RefreshGogoSelectionToggles()
        {
            UnsubscribeFromSelectedEvents();
            m_GogoSelectionToggles.Clear();
            foreach (Transform child in m_ToggleGroup.transform)
            {
                Destroy(child.gameObject);
            }

            var identifiableGogos = PlayerTracker.Player.Collection.IdentifiableGogos;
            var orderedIdentifiableGogos = identifiableGogos.OrderBy(i => GogoSituationDatabase.GetSituation(i));
            foreach (var identifiableGogo in orderedIdentifiableGogos)
            {
                var gogoSelectionToggle = Instantiate(m_GogoSelectionTogglePrefab).GetComponent<GogoSelectionToggle>();
                gogoSelectionToggle.SetToggle(identifiableGogo);
                gogoSelectionToggle.transform.SetParent(m_ToggleGroup.transform);
                gogoSelectionToggle.Toggle.group = m_ToggleGroup;

                gogoSelectionToggle.GogoSelected += GogoSelectionToggle_OnGogoSelected;
                m_GogoSelectionToggles.Add(gogoSelectionToggle);
            }

            EventSystem.current.SetSelectedGameObject(m_GogoSelectionToggles[0].gameObject);
        }

        private void UnsubscribeFromSelectedEvents()
        {
            foreach (var gogoSelectionToggle in m_GogoSelectionToggles)
            {
                gogoSelectionToggle.GogoSelected -= GogoSelectionToggle_OnGogoSelected;
            }
        }
    }
}
