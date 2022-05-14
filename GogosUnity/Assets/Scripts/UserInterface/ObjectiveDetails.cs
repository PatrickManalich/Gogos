using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class ObjectiveDetails : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_ObjectiveText;

        [SerializeField]
        private Image m_GoldenGogoPortrait;

        [SerializeField]
        private SpawnerRandomizer m_SpawnerRandomizer;

        private const string CollectObjectiveText = "Turns Until";
        private const string KnockOutObjectiveText = "Knock Out";

        private void Start()
        {
            TurnTracker.TurnChanged += RefreshDetails;
            ObjectiveTracker.ObjectiveChanged += RefreshDetails;

            RefreshDetails();
        }

        private void OnDestroy()
        {
            ObjectiveTracker.ObjectiveChanged -= RefreshDetails;
            TurnTracker.TurnChanged -= RefreshDetails;
        }

        private void RefreshDetails()
        {
            if (ObjectiveTracker.Objective == Objective.Collect)
            {
                var turnsUntil = ObjectiveTracker.SwitchObjectivesTurn - TurnTracker.Turn;
                m_ObjectiveText.text = $"{turnsUntil} {CollectObjectiveText}";
            }
            else
            {
                m_ObjectiveText.text = KnockOutObjectiveText;
            }
            m_GoldenGogoPortrait.sprite = m_SpawnerRandomizer.GoldenScriptableGogo.Portrait;
        }
    }
}
