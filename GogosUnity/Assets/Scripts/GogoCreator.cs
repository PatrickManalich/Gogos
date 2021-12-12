using UnityEngine;

namespace Gogos
{
    public class GogoCreator : MonoBehaviour
    {
        public AbstractGogo CreatedGogo { get; private set; }

        [SerializeField]
        private Launcher m_Launcher;

        private void Start()
        {
            m_Launcher.Launched += Launcher_OnLaunched;
        }

        private void OnDestroy()
        {
            m_Launcher.Launched -= Launcher_OnLaunched;
        }

        public void CreateGogo(IdentifiableGogo identifiableGogo)
        {
            if (CreatedGogo != null)
            {
                Destroy(CreatedGogo.gameObject);
            }

            CreatedGogo = Instantiate(identifiableGogo.ScriptableGogo.Prefab).GetComponent<AbstractGogo>();
            CreatedGogo.SetTiers(identifiableGogo);
            CreatedGogo.name = CreatedGogo.name.Replace("(Clone)", "");

            m_Launcher.LoadProjectile(CreatedGogo.gameObject);
        }

        private void Launcher_OnLaunched()
        {
            GogoSituationDatabase.SetSituation(CreatedGogo.IdentifiableGogo, Situation.InRing);
            CreatedGogo = null;
        }
    }
}
