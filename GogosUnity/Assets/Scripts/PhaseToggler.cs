﻿using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class PhaseToggler : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] m_ItemsToToggle;

        [SerializeField]
        private Phase[] m_ActiveForPhases;

        private void Start()
        {
            PhaseTracker.PhaseChanged += ToggleBasedOnPhase;

            ToggleBasedOnPhase();
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= ToggleBasedOnPhase;
        }

        private void ToggleBasedOnPhase()
        {
            var active = m_ActiveForPhases.Contains(PhaseTracker.Phase);
            foreach (var item in m_ItemsToToggle)
            {
                item.SetActive(active);
            }
        }
    }
}
