using TMPro;
using UnityEngine;

namespace Gogos
{
    public class PlayerNameText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        public void SetPlayerName(Player player)
        {
            var palette = m_ScriptableColorPalette;
            var isUnclaimed = player == null;
            m_Text.text = isUnclaimed ? "Unclaimed" : player.Name;
            m_Text.color = isUnclaimed ? palette.Grey : palette.GetColorForPlayerColor(player.PlayerColor);
        }
    }
}
