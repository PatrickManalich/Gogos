﻿using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "ScriptableGogoBucket", menuName = "Gogos/Scriptable Gogo Bucket")]
    public class ScriptableGogoBucket : ScriptableObject
    {
        [SerializeField]
        private AbstractScriptableGogo[] m_ScriptableGogos;
        public AbstractScriptableGogo[] ScriptableGogos => m_ScriptableGogos;

        public AbstractScriptableGogo GetRandomScriptableGogo()
        {
            return ScriptableGogos[Random.Range(0, ScriptableGogos.Length)];
        }
    }
}
