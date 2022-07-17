using UnityEngine;

namespace Gogos
{
    public class SparkleVisualRefresher : MonoBehaviour
    {
        [SerializeField]
        private RarityTierTracker m_RarityTierTracker;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        [SerializeField]
        private SparkleVisual m_SparkleVisual;

        private void OnEnable()
        {
            m_RarityTierTracker.TierChanged += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            m_RarityTierTracker.TierChanged -= Refresh;
        }

        private void Refresh()
        {
            var color = m_ScriptableColorPalette.GetColorForRarityTier(m_RarityTierTracker.Tier);
            m_SparkleVisual.SetColor(color);
        }
    }
}
