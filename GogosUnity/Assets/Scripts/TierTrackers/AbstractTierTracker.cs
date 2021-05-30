using System;
using UnityEngine;

namespace Gogos
{
    public enum TierVariant { Weight, Rarity, Range, BlastForce, ShieldStrength }

    public abstract class AbstractTierTracker : MonoBehaviour
    {
        public abstract TierVariant TierVariant { get; }

        public abstract void Modify(int modifier);
    }

    public abstract class AbstractTierTracker<T> : AbstractTierTracker where T : Enum
    {
        public event Action CurrentTierChanged;

        public abstract T CurrentTier { get; protected set; }

        public override void Modify(int modifier)
        {
            var values = Enum.GetValues(typeof(T));
            var currentIndex = Array.IndexOf(values, CurrentTier);
            var max = values.Length - 1;
            var min = 0;
            var newIndex = Mathf.Min(Mathf.Max(currentIndex + modifier, min), max);
            var newTier = (T)values.GetValue(newIndex);
            if(!newTier.Equals(CurrentTier))
            {
                CurrentTier = newTier;
                CurrentTierChanged?.Invoke();
            }
        }
    }
}