using UnityEngine;

namespace Gogos
{
    public class GoldenSatoBlastedEffect : AbstractBlastedEffect
    {
        [SerializeField]
        private GoldenSato m_GoldenSato;

        protected override void OnBlasted(BlastTriggerEventArgs e)
        {
            m_GoldenSato.PrepareCounterBlast(e.BlastTrigger.CenterPosition);
        }
    }
}
