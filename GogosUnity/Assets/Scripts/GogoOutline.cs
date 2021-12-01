using cakeslice;
using UnityEngine;

namespace Gogos
{
    public class GogoOutline : Outline
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

        private void Start()
        {
            if (m_Gogo.Player != null)
            {
                color = (int)m_Gogo.Player.PlayerColor;
            }
            else
            {
                enabled = false;
            }
        }
    }
}
