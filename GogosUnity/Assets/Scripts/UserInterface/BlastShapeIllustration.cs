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

        public void SetIllustration(Sprite[] sprites)
        {
            if (m_IllustrationCoroutine != null)
            {
                StopCoroutine(m_IllustrationCoroutine);
                m_IllustrationCoroutine = null;
            }

            m_IllustrationCoroutine = StartCoroutine(IllustrationRoutine(sprites));
        }

        private IEnumerator IllustrationRoutine(Sprite[] sprites)
        {
            while (true)
            {
                m_Image.sprite = m_EmptySprite;
                yield return new WaitForSeconds(1);

                foreach (var sprite in sprites)
                {
                    m_Image.sprite = sprite;
                    yield return new WaitForSeconds(SecondsBetweenSprites);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
