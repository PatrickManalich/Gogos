namespace Gogos
{
    public class GoldenGogoCollectedEffect : AbstractCollectedEffect
    {
        private const int GoldenGogoPointValue = 3000;

        protected override void OnCollected()
        {
            if (PhaseTracker.Phase == Phase.Spawning)
            {
                return;
            }

            PlayerTracker.Player.AddPoints(GoldenGogoPointValue);
        }
    }
}
