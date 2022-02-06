using System;
using UnityEngine;

namespace Gogos
{
    public class ExpandTrigger : MonoBehaviour
    {
        public event Action PlayerChanged;

        public Player Player { get; private set; }

        public Vector3 CenterPosition => m_CenterPoint.transform.position;

        [SerializeField]
        private Collider m_Collider;

        [SerializeField]
        private Animator m_TriggerAnimator;

        [SerializeField]
        private GameObject m_CenterPoint;

        private const string ExpandName = "Expand";

        public void SetPlayer(Player player)
        {
            Player = player;
            PlayerChanged?.Invoke();
        }

        public void Expand()
        {
            m_Collider.enabled = true;
            m_TriggerAnimator.SetBool(ExpandName, true);
        }

        public void Shrink()
        {
            m_Collider.enabled = false;
            m_TriggerAnimator.SetBool(ExpandName, false);
        }
    }
}
