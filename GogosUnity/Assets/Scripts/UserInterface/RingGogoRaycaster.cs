using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gogos
{
    public class RingGogoRaycaster : MonoBehaviour
    {
        [SerializeField]
        private GogoDetailsPanel m_GogoDetailsPanel;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;

            m_GogoDetailsPanel.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void Update()
        {
            var selectingUserInterface = EventSystem.current.currentSelectedGameObject != null;
            if (PhaseTracker.Phase != Phase.Selecting || selectingUserInterface || !Input.GetMouseButtonDown(0))
            {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray, 100, LayerMasks.Triggerable);
            var gogoHits = hits.Where(h => h.collider.GetComponent<AbstractGogo>() != null);
            var firstGogo = gogoHits.OrderBy(h => h.distance).Select(h => h.collider.GetComponent<AbstractGogo>()).FirstOrDefault();
            if (firstGogo != null)
            {
                if (firstGogo.Player == null || GogoSituationDatabase.GetSituation(firstGogo.IdentifiableGogo) == Situation.InRing)
                {
                    m_GogoDetailsPanel.SetDetails(firstGogo);
                    m_GogoDetailsPanel.gameObject.SetActive(true);
                }
                else
                {
                    m_GogoDetailsPanel.gameObject.SetActive(false);
                }
            }
            else
            {
                m_GogoDetailsPanel.gameObject.SetActive(false);
            }
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase != Phase.Selecting)
            {
                m_GogoDetailsPanel.gameObject.SetActive(false);
            }
        }
    }
}
