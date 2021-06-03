using UnityEngine;

namespace Gogos
{
	public abstract class AbstractGogo : MonoBehaviour
	{
        [SerializeField]
		private Accelerometer m_Accelerometer;

        protected abstract void OnStartedMoving();
        protected abstract void OnStoppedMoving();

        protected virtual void Start()
        {
            SubscribeToMovementEvents();
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeFromMovementEvents();
        }

        protected void SubscribeToMovementEvents()
        {
            m_Accelerometer.StartedMoving += Accelerometer_OnStartedMoving;
            m_Accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;
        }

        protected void UnsubscribeFromMovementEvents()
        {
            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
            m_Accelerometer.StartedMoving -= Accelerometer_OnStartedMoving;
        }

        private void Accelerometer_OnStartedMoving()
        {
            OnStartedMoving();
        }

        private void Accelerometer_OnStoppedMoving()
        {
            OnStoppedMoving();
        }
    }
}