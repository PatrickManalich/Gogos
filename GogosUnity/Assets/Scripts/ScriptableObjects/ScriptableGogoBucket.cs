using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "ScriptableGogoBucket", menuName = "Gogos/Scriptable Gogo Bucket")]
    public class ScriptableGogoBucket : ScriptableObject
	{
        [SerializeField]
        private ScriptableGogo[] m_ScriptableGogos;
        public ScriptableGogo[] ScriptableGogos => m_ScriptableGogos;
    }
}