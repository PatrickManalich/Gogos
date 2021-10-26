using cakeslice;

namespace Gogos
{
    public class GogoOutline : Outline
    {
        private void Start()
        {
            color = (int)PlayerTracker.Player.PlayerColor;
        }
    }
}
