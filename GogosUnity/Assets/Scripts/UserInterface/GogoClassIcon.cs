using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;

namespace Gogos
{
    public class GogoClassIcon : MonoBehaviour
    {
        [System.Serializable]
        private class IconsByClass : SerializableDictionary<GogoClass, GameObject> { }

        [SerializeField]
        private IconsByClass m_IconsByClass;

        public void SetIcon(GogoClass gogoVariant)
        {
            foreach (var icon in m_IconsByClass.Values)
            {
                icon.SetActive(false);
            }
            m_IconsByClass[gogoVariant].SetActive(true);
        }
    }
}
