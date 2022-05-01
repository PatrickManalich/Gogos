using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "GoldenGogo", menuName = "Gogos/Scriptable Gogo/Golden")]
    public class GoldenScriptableGogo : AbstractScriptableGogo
    {
        public override GogoClass GogoClass => GogoClass.Golden;
    }
}
