namespace Gogos
{
    public class SupportTrigger : AnimatedTrigger
    {
        public SupportAbility SupportAbility { get; private set; }

        public void ProvideSupport(SupportAbility supportAbility)
        {
            SupportAbility = supportAbility;
            Expand();
        }

        public void RemoveSupport()
        {
            Shrink();
        }
    }
}
