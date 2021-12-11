using System;
using UnityEngine;

namespace Gogos
{
    public class TierDetails : MonoBehaviour
    {
        [SerializeField]
        private TierSlot[] m_TierSlots;

        public void SetSlots<T>(T tier) where T : Enum
        {
            var slotsFilled = (int)(object)tier + 1;
            for (int i = 0; i < m_TierSlots.Length; i++)
            {
                var tierSlot = m_TierSlots[i];
                if (i < slotsFilled)
                {
                    tierSlot.Fill();
                }
                else
                {
                    tierSlot.Empty();
                }
            }
        }
    }
}
