﻿using UnityEngine;

namespace Gogos
{
    public class TriggerRangeRefresher : MonoBehaviour
    {
        [SerializeField]
        private RangeTierTracker m_RangeTierTracker;

        [SerializeField]
        private GameObject m_TriggerRange;

        private void Start()
        {
            m_RangeTierTracker.CurrentTierChanged += Refresh;

            Refresh();
        }

        private void OnDestroy()
        {
            m_RangeTierTracker.CurrentTierChanged -= Refresh;
        }

        public void Refresh()
        {
            m_TriggerRange.transform.localScale = transform.localScale * m_RangeTierTracker.Range;
        }
    }
}