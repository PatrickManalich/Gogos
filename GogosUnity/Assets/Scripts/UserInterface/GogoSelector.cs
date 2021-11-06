using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gogos
{
    public class GogoSelector : MonoBehaviour
    {
        public AbstractScriptableGogo SelectedScriptableGogo { get; private set; }

        [SerializeField]
        private ToggleGroup m_ToggleGroup;

        [SerializeField]
        private GameObject m_GogoSelectionTogglePrefab;

        [SerializeField]
        private GogoDetailsPanel m_GogoDetailsPanel;

        [SerializeField]
        private GogoCreator m_GogoCreator;

        private List<GogoSelectionToggle> m_GogoSelectionToggles = new List<GogoSelectionToggle>();

        private void Start()
        {
            PlayerTracker.PlayerChanged += RefreshGogoSelectionToggles;

            RefreshGogoSelectionToggles();
        }

        private void OnDestroy()
        {
            UnsubscribeFromSelectedEvents();
            PlayerTracker.PlayerChanged -= RefreshGogoSelectionToggles;
        }

        private void GogoSelectionToggle_OnGogoSelected(object sender, GogoSelectedEventArgs e)
        {
            SelectedScriptableGogo = e.ScriptableGogo;
            m_GogoDetailsPanel.SetDetails(SelectedScriptableGogo);
            m_GogoCreator.CreateGogo(SelectedScriptableGogo);
        }

        private void RefreshGogoSelectionToggles()
        {
            UnsubscribeFromSelectedEvents();
            m_GogoSelectionToggles.Clear();
            foreach (Transform child in m_ToggleGroup.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var identifiableGogo in PlayerTracker.Player.Collection.IdentifiableGogos)
            {
                var gogoSelectionToggle = Instantiate(m_GogoSelectionTogglePrefab).GetComponent<GogoSelectionToggle>();
                gogoSelectionToggle.SetToggle(identifiableGogo.ScriptableGogo);
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
