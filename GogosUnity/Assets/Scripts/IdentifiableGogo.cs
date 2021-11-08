using System;

namespace Gogos
{
    public class IdentifiableGogo
    {
        public string Id { get; set; }
        public AbstractScriptableGogo ScriptableGogo { get; }

        public IdentifiableGogo(AbstractScriptableGogo scriptableGogo)
        {
            Id = Guid.NewGuid().ToString();
            ScriptableGogo = scriptableGogo;
        }
    }
}
