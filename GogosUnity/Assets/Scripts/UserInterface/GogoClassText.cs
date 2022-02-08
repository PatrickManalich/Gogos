using TMPro;
using UnityEngine;

namespace Gogos
{
    public class GogoClassText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        public void SetText(GogoClass gogoClass)
        {
            m_Text.text = gogoClass.ToString().Replace("Gogo", string.Empty);
        }
    }
}
