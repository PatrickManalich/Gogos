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
            color = (int)m_Gogo.Player.PlayerColor;
        }
    }
}
