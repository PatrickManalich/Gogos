using System;

namespace Gogos
{
    public class GogoSelectedEventArgs : EventArgs
    {
        public IdentifiableGogo IdentifiableGogo { get; }

        public GogoSelectedEventArgs(IdentifiableGogo identifiableGogo)
        {
            IdentifiableGogo = identifiableGogo;
        }
    }
}
