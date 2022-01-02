using RotaryHeart.Lib.SerializableDictionaryPro;
using System;
using UnityEngine;

namespace Gogos
{
    public enum ShieldResponse { Deflect }

    [Serializable]
    public class GroupsByShieldResponse : SerializableDictionary<ShieldResponse, Groups> { }

    public class ShieldAbility : MonoBehaviour
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

        [SerializeField]
        private GroupsByShieldResponse m_GroupsByShieldResponse;

        public void SetAbility(GroupsByShieldResponse groupsByShieldResponse)
        {
            m_GroupsByShieldResponse = groupsByShieldResponse;
        }

        public bool CanDeflect(Player player)
        {
            var hasDeflectGroups = m_GroupsByShieldResponse.ContainsKey(ShieldResponse.Deflect);
            return hasDeflectGroups && m_GroupsByShieldResponse[ShieldResponse.Deflect].IsInGroup(player, m_Gogo.Player);
        }
    }
}
