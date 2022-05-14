using System;
using System.Collections.Generic;
using System.Linq;

namespace Gogos
{
    public class Collection
    {
        public event EventHandler<IdentifiableGogoEventArgs> GogoAdded;

        public IReadOnlyList<IdentifiableGogo> IdentifiableGogos => m_IdentifiableGogosById.Values.ToList();

        private Dictionary<string, IdentifiableGogo> m_IdentifiableGogosById = new Dictionary<string, IdentifiableGogo>();

        public void Add(IdentifiableGogo identifiableGogo)
        {
            m_IdentifiableGogosById.Add(identifiableGogo.Id, identifiableGogo);
            GogoAdded?.Invoke(this, new IdentifiableGogoEventArgs(identifiableGogo));
        }
    }
}
