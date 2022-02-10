using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class OnClickToggler : MonoBehaviour
    {
        [SerializeField]
        private Button m_Button;

        [SerializeField]
        private List<GameObject> m_GameObjects;

        [SerializeField]
        private bool m_Value;

        private void Start()
        {
            m_Button.onClick.AddListener(Button_OnClick);
        }

        private void OnDestroy()
        {
            m_Button.onClick.RemoveListener(Button_OnClick);
        }

        private void Button_OnClick()
        {
            m_GameObjects.ForEach(g => g.SetActive(m_Value));
        }
    }
}
