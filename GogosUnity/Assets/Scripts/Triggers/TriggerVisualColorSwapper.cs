using Gogos.Extensions;
using UnityEngine;

namespace Gogos
{
    public class TriggerVisualColorSwapper : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer m_MeshRenderer;

        [SerializeField]
        private ExpandTrigger m_ExpandTrigger;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        private void Start()
        {
            m_ExpandTrigger.PlayerChanged += Refresh;

            Refresh();
        }

        private void OnDestroy()
        {
            m_ExpandTrigger.PlayerChanged -= Refresh;
        }

        private void Refresh()
        {
            var player = m_ExpandTrigger.Player;
            if (player == null)
            {
                return;
            }

            var colorAlpha = m_MeshRenderer.material.color.a;
            var color = m_ScriptableColorPalette.GetColorForPlayerColor(player.PlayerColor);
            m_MeshRenderer.material.color = color.WithA(colorAlpha);
        }
    }
}
