using UnityEngine;

namespace Gogos
{
    public class DevKeyInputs : MonoBehaviour
    {
        private void Update()
        {
            if (InputKeys.Dev.SwitchObjectivesKeyDown)
            {
                var nextObjective = ObjectiveTracker.Objective == Objective.Collect ? Objective.KnockOut : Objective.Collect;
                ObjectiveTracker.OverrideObjective(nextObjective);
            }
        }
    }
}
