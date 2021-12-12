using System;
using UnityEngine;

namespace Gogos
{
    public class TierDetails : MonoBehaviour
    {
        [SerializeField]
        private TierSlot[] m_TierSlots;

        public void SetSlots<T>(T currentTier, T baseTier) where T : Enum
        {
            var currentIndex = (int)(object)currentTier;
            var baseIndex = (int)(object)baseTier;
            var slotsFilled = currentIndex + 1;

            for (int i = 0; i < m_TierSlots.Length; i++)
            {
                var tierSlot = m_TierSlots[i];
                tierSlot.HideIcons();

                if (i < slotsFilled)
                {
                    tierSlot.Fill();
                    if (i > baseIndex)
                    {
                        tierSlot.ShowPlusIcon();
                    }
                }
                else
                {
                    tierSlot.Empty();
                    if (i <= baseIndex)
                    {
                        tierSlot.ShowMinusIcon();
                    }
                }
            }
        }
    }
}
