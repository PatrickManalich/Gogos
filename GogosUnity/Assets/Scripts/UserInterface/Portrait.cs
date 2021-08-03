using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
	public class Portrait : MonoBehaviour
	{
        [SerializeField]
        private Image m_Image;

        public void SetPortrait(Sprite sprite)
        {
            m_Image.sprite = sprite;
            m_Image.color = Color.white;
        }
    }
}