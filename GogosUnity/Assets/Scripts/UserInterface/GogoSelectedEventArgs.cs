using System;

namespace Gogos
{
    public class GogoSelectedEventArgs : EventArgs
    {
        public AbstractScriptableGogo ScriptableGogo { get; }

        public GogoSelectedEventArgs(AbstractScriptableGogo scriptableGogo)
        {
            ScriptableGogo = scriptableGogo;
        }
    }
}
