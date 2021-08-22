using UnityEngine;

namespace Gogos.Managers
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static PlayerManager PlayerManager => Instance.m_PlayerManager;

        [SerializeField]
        private PlayerManager m_PlayerManager;

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