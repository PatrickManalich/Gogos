using System;
using System.Collections.Generic;
using System.Linq;

namespace Gogos
{
    public class Collection
    {
        public event EventHandler<IdentifiableGogoEventArgs> GogoAdded;

        public event Action GemsAdded;

        public IReadOnlyList<IdentifiableGogo> IdentifiableGogos => m_IdentifiableGogosById.Values.ToList();

        private Dictionary<string, IdentifiableGogo> m_IdentifiableGogosById = new Dictionary<string, IdentifiableGogo>();

        private Dictionary<GogoClass, int> m_GemsByGogoClass = new Dictionary<GogoClass, int>();

        public Collection()
        {
            m_GemsByGogoClass.Add(GogoClass.Blast, 0);
            m_GemsByGogoClass.Add(GogoClass.Shield, 0);
            m_GemsByGogoClass.Add(GogoClass.Support, 0);
        }

        public void Add(IdentifiableGogo identifiableGogo)
        {
            m_IdentifiableGogosById.Add(identifiableGogo.Id, identifiableGogo);
            GogoAdded?.Invoke(this, new IdentifiableGogoEventArgs(identifiableGogo));
        }

        public void Add(GogoClass gogoClass, int gems)
        {
            m_GemsByGogoClass[gogoClass] += gems;
            GemsAdded?.Invoke();
        }

        public int GetGemsForGogoClass(GogoClass gogoClass)
        {
            return m_GemsByGogoClass[gogoClass];
        }
    }
}
