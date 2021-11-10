using System;

namespace Gogos
{
    public class IdentifiableGogoEventArgs : EventArgs
    {
        public IdentifiableGogo IdentifiableGogo { get; }

        public IdentifiableGogoEventArgs(IdentifiableGogo identifiableGogo)
        {
            IdentifiableGogo = identifiableGogo;
        }
    }
}
