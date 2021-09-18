using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;

namespace Gogos
{
    public class GogoVariantIcon : MonoBehaviour
    {
        [System.Serializable]
        private class IconsByVariant : SerializableDictionary<GogoVariant, GameObject> { }

        [SerializeField]
        private IconsByVariant m_IconsByVariant;

        public void SetIcon(GogoVariant gogoVariant)
        {
            foreach (var icon in m_IconsByVariant.Values)
            {
                icon.SetActive(false);
            }
            m_IconsByVariant[gogoVariant].SetActive(true);
        }
    }
}
