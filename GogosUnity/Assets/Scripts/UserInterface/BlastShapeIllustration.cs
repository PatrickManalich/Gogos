using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class BlastShapeIllustration : MonoBehaviour
    {
        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private Sprite m_EmptySprite;

        private const float SecondsBetweenSprites = 0.05f;

        private Coroutine m_IllustrationCoroutine;

        private Sprite[] m_Sprites;

        private void OnEnable()
        {
            if (m_Sprites != null)
            {
                m_IllustrationCoroutine = StartCoroutine(IllustrationRoutine());
            }
        }

        private void OnDisable()
        {
            if (m_IllustrationCoroutine != null)
            {
                StopCoroutine(m_IllustrationCoroutine);
                m_IllustrationCoroutine = null;
            }
        }

        public void SetIllustration(Sprite[] sprites)
        {
            m_Sprites = sprites;

            if (m_IllustrationCoroutine != null)
            {
                StopCoroutine(m_IllustrationCoroutine);
                m_IllustrationCoroutine = null;
            }

            m_IllustrationCoroutine = StartCoroutine(IllustrationRoutine());
        }

        private IEnumerator IllustrationRoutine()
        {
            while (true)
            {
                m_Image.sprite = m_EmptySprite;
                yield return new WaitForSeconds(1);

                foreach (var sprite in m_Sprites)
                {
                    m_Image.sprite = sprite;
                    yield return new WaitForSeconds(SecondsBetweenSprites);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
