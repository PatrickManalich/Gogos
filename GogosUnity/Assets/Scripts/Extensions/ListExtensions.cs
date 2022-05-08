using System.Collections.Generic;
using UnityEngine;

namespace Gogos.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns a random element from this list.
        /// </summary>
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Returns a random element from this list and removes the first occurrence of the random element from this list.
        /// </summary>
        public static T GetRandomAndRemove<T>(this List<T> list)
        {
            T item = list.GetRandom();
            list.Remove(item);
            return item;
        }

        /// <summary>
        /// Returns a random element from this list and removes the random element from this list.
        /// </summary>
        public static T GetRandomAndRemoveAt<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            T randomItem = list[randomIndex];
            list.RemoveAt(randomIndex);
            return randomItem;
        }

        /// <summary>
        /// Returns a random element from this list and that is not the active element unless there is only 1 element remaining.
        /// </summary>
        public static T GetDistinctRandom<T>(this List<T> list, T active)
        {
            List<T> choices = new List<T>(list);
            if (choices.Count > 1)
            {
                choices.Remove(active);
            }
            return choices.GetRandom();
        }

        /// <summary>
        /// Returns this list with all its items shuffled randomly.
        /// </summary>
        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> remaining = new List<T>(list);
            list.Clear();
            while (remaining.Count > 0)
            {
                list.Add(remaining.GetRandomAndRemove());
            }
            return list;
        }
    }
}
