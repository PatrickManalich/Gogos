using System;

namespace Gogos
{
    public class TurnTracker : AbstractSingleton<TurnTracker>
    {
        public static event Action TurnChanged;

        public static int Turn { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Turn = 1;
        }

        private void Start()
        {
            PlayerTracker.PlayerChanged += PlayerTracker_OnPlayerChanged;
        }

        private void OnDestroy()
        {
            PlayerTracker.PlayerChanged -= PlayerTracker_OnPlayerChanged;
        }

        private void PlayerTracker_OnPlayerChanged()
        {
            Turn++;
            TurnChanged?.Invoke();
        }
    }
}
