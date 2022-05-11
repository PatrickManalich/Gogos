using System;
using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class PlatformToggler : MonoBehaviour
    {
        public event Action Toggling;

        public event Action Toggled;

        public event Action Skipped;

        [SerializeField]
        private GameObject m_CollectPlatformContainer;

        [SerializeField]
        private GameObject m_DefeatPlatformContainer;

        private bool m_ReadyToToggle;

        private void Start()
        {
            ObjectiveTracker.ObjectiveChanged += ObjectiveTracker_OnObjectiveChanged;
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            m_CollectPlatformContainer.SetActive(false);
            m_DefeatPlatformContainer.SetActive(false);
            StartCoroutine(ToggleRoutine());
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
            ObjectiveTracker.ObjectiveChanged -= ObjectiveTracker_OnObjectiveChanged;
        }

        private void ObjectiveTracker_OnObjectiveChanged()
        {
            m_ReadyToToggle = true;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.PlatformToggling)
            {
                if (m_ReadyToToggle)
                {
                    m_ReadyToToggle = false;
                    StartCoroutine(ToggleRoutine());
                }
                else
                {
                    Skipped?.Invoke();
                }
            }
        }

        private IEnumerator ToggleRoutine()
        {
            Toggling?.Invoke();
            yield return new WaitForSeconds(2);

            if (ObjectiveTracker.Objective == Objective.Collect)
            {
                m_CollectPlatformContainer.SetActive(true);
                m_DefeatPlatformContainer.SetActive(false);
            }
            else
            {
                m_DefeatPlatformContainer.SetActive(true);
                m_CollectPlatformContainer.SetActive(false);
            }
            yield return new WaitForSeconds(2);

            Toggled?.Invoke();
        }
    }
}
