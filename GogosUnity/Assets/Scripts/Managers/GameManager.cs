using UnityEngine;

namespace Gogos.Managers
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static InputManager InputKeyManager => Instance.m_InputManager;

        [SerializeField]
        private InputManager m_InputManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}