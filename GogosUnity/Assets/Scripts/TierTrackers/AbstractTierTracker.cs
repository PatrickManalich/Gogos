using System;
using UnityEngine;

namespace Gogos
{
    public enum TierVariant { Rarity, Weight, PointValue, Range, BlastForce, ShieldStrength }

    public abstract class AbstractTierTracker : MonoBehaviour
    {
        public abstract TierVariant TierVariant { get; }

        public abstract void Modify(int modifier);
    }

    public abstract class AbstractTierTracker<T> : AbstractTierTracker where T : Enum
    {
        public event Action TierChanged;

        public T Tier => m_Tier;

        [SerializeField]
        private T m_Tier;

        public override void Modify(int modifier)
        {
            var values = Enum.GetValues(typeof(T));
            var currentIndex = Array.IndexOf(values, Tier);
            var max = values.Length - 1;
            var min = 0;
            var newIndex = Mathf.Min(Mathf.Max(currentIndex + modifier, min), max);
            var newTier = (T)values.GetValue(newIndex);
            if(!newTier.Equals(Tier))
            {
                m_Tier = newTier;
                TierChanged?.Invoke();
            }
        }
    }
}