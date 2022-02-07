using TMPro;
using UnityEngine;

namespace Gogos
{
    public class TextColorSwapper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        private void Start()
        {
            PlayerTracker.PlayerChanged += Refresh;

            Refresh();
        }

        private void OnDestroy()
        {
            PlayerTracker.PlayerChanged -= Refresh;
        }

        private void Refresh()
        {
            m_Text.color = m_ScriptableColorPalette.GetColorForPlayerColor(PlayerTracker.Player.PlayerColor);
        }
    }
}
