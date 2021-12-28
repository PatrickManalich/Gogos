using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class AttributesReference : MonoBehaviour
    {
        public IReadOnlyList<AbstractAttribute> Attributes { get; private set; }

        private void Awake()
        {
            Attributes = GetComponents<AbstractAttribute>();
        }
    }
}
