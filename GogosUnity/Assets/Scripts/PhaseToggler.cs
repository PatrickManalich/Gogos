using System.Linq;
using UnityEngine;

namespace Gogos
{
	public class PhaseToggler : MonoBehaviour
	{
        [SerializeField]
        private PhaseTracker m_PhaseTracker;

        [SerializeField]
        private GameObject[] m_ItemsToToggle;

        [SerializeField]
        private Phase[] m_ActiveForPhases;

        private void Start()
		{
            m_PhaseTracker.PhaseChanged += ToggleBasedOnPhase;

            ToggleBasedOnPhase();
		}

        private void OnDestroy()
		{
            m_PhaseTracker.PhaseChanged -= ToggleBasedOnPhase;
        }

        private void ToggleBasedOnPhase()
        {
            var active = m_ActiveForPhases.Contains(m_PhaseTracker.Phase);
            foreach (var item in m_ItemsToToggle)
            {
                item.SetActive(active);
            }
        }
    }
}