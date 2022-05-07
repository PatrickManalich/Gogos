using UnityEngine;

namespace Gogos
{
    public class DevKeyInputs : MonoBehaviour
    {
        private void Update()
        {
            if (InputKeys.Dev.SwitchObjectivesKeyDown)
            {
                var nextObjective = ObjectiveTracker.Objective == Objective.Collect ? Objective.Defeat : Objective.Collect;
                ObjectiveTracker.OverrideObjective(nextObjective);
            }
        }
    }
}
