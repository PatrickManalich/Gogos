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
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.TurnChanging)
            {
                Turn++;
                TurnChanged?.Invoke();
            }
        }
    }
}
