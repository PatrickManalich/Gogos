using Gogos.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gogos
{
	public class GogoSelector: MonoBehaviour
	{
        [SerializeField]
        private ToggleGroup m_ToggleGroup;

        [SerializeField]
        private GameObject m_GogoSelectionTogglePrefab;

        [SerializeField]
        private GogoDetailsPanel m_GogoDetailsPanel;

        private List<GogoSelectionToggle> m_GogoSelectionToggles = new List<GogoSelectionToggle>();

        private void Start()
		{
            foreach (Transform child in m_ToggleGroup.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var scriptableGogo in GameManager.PlayerManager.CurrentPlayer.Collection)
            {
				var gogoSelectionToggle = Instantiate(m_GogoSelectionTogglePrefab).GetComponent<GogoSelectionToggle>();
				gogoSelectionToggle.SetToggle(scriptableGogo);
				gogoSelectionToggle.transform.SetParent(m_ToggleGroup.transform);
                gogoSelectionToggle.Toggle.group = m_ToggleGroup;

                gogoSelectionToggle.GogoSelected += GogoSelectionToggle_OnGogoSelected;
                m_GogoSelectionToggles.Add(gogoSelectionToggle);
            }

            EventSystem.current.SetSelectedGameObject(m_GogoSelectionToggles[0].gameObject);
		}

        private void OnDestroy()
        {
            foreach (var gogoSelectionToggle in m_GogoSelectionToggles)
            {
                gogoSelectionToggle.GogoSelected -= GogoSelectionToggle_OnGogoSelected;
            }
        }

        private void GogoSelectionToggle_OnGogoSelected(object sender, GogoSelectedEventArgs e)
        {
            var selectedScriptableGogo = e.ScriptableGogo;
            m_GogoDetailsPanel.SetDetails(selectedScriptableGogo);
        }
    }
}