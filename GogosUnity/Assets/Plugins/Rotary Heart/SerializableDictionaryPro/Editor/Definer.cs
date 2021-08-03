using System.Collections.Generic;
using UnityEditor;

namespace RotaryHeart.Lib.SerializableDictionaryPro
{
    [InitializeOnLoad]
    public class Definer
    {
        static Definer()
        {
            List<string> defines = new List<string>(1)
            {
                "RH_SerializedDictionary"
            };
            RotaryHeart.Lib.Definer.ApplyDefines(defines);
        }
    }
}