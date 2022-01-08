using RotaryHeart.Lib.SerializableDictionaryPro;
using System;
using UnityEngine;

namespace Gogos
{
    public enum ShieldResponse { Deflect, Attract }

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

        public bool CanDeflect(GroupTag groupTag, Player player)
        {
            var hasDeflectGroups = m_GroupsByShieldResponse.ContainsKey(ShieldResponse.Deflect);
            return hasDeflectGroups && m_GroupsByShieldResponse[ShieldResponse.Deflect].IsInGroup(groupTag, player, m_Gogo.Player);
        }

        public bool CanAttract(GroupTag groupTag, Player player)
        {
            var hasAttractGroups = m_GroupsByShieldResponse.ContainsKey(ShieldResponse.Attract);
            return hasAttractGroups && m_GroupsByShieldResponse[ShieldResponse.Attract].IsInGroup(groupTag, player, m_Gogo.Player);
        }
    }
}
