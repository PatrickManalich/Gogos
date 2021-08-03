using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
	public class TierDetails : MonoBehaviour
	{
        [SerializeField]
        private Image[] m_TierSlots;

        [SerializeField]
        private Color m_FilledColor;

        [SerializeField]
        private Color m_EmptyColor;

		public void SetSlots<T>(T tier) where T : Enum
        {
            var slotsFilled = (int)(object)tier + 1;
            for (int i = 0; i < m_TierSlots.Length; i++)
            {
                var tierSlot = m_TierSlots[i];
                tierSlot.color = i < slotsFilled ? m_FilledColor : m_EmptyColor;
            }
        }
	}
}