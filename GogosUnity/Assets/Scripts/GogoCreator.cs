using UnityEngine;

namespace Gogos
{
    public class GogoCreator : MonoBehaviour
    {
        [SerializeField]
        private Launcher m_Launcher;

        private AbstractGogo m_CreatedGogo;

        private void Start()
        {
            m_Launcher.Launched += Launcher_OnLaunched;
        }

        private void OnDestroy()
        {
            m_Launcher.Launched -= Launcher_OnLaunched;
        }

        public void CreateGogo(AbstractScriptableGogo scriptableGogo)
        {
            if (m_CreatedGogo != null)
            {
                Destroy(m_CreatedGogo.gameObject);
            }

            m_CreatedGogo = Instantiate(scriptableGogo.Prefab).GetComponent<AbstractGogo>();
            m_CreatedGogo.SetTiers(scriptableGogo);
            m_CreatedGogo.name = m_CreatedGogo.name.Replace("(Clone)", "");

            m_Launcher.LoadProjectile(m_CreatedGogo.gameObject);
        }

        private void Launcher_OnLaunched()
        {
            m_CreatedGogo = null;
        }
    }
}
