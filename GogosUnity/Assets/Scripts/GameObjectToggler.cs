using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class GameObjectToggler : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> m_GameObjects;

        [SerializeField]
        private bool m_Value;

        [SerializeField]
        private float m_Delay;

        private void OnEnable()
        {
            StartCoroutine(ToggleAfterDelayRoutine());
        }

        private IEnumerator ToggleAfterDelayRoutine()
        {
            yield return new WaitForSeconds(m_Delay);
            m_GameObjects.ForEach(g => g.SetActive(m_Value));
        }
    }
}
