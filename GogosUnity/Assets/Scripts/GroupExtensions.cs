using System;

namespace Gogos
{
    [Flags]
    public enum Groups { AllyGogos = 1, EnemyGogos = 2, UnclaimedGogos = 4 } // Index 0 is for "Nothing" in the editor

    public static class GroupExtensions
    {
        public static bool IsInGroup(this Groups groups, Player player, Player allyPlayer)
        {
            var isInAllyGogosGroup = groups.HasFlag(Groups.AllyGogos) && player == allyPlayer;
            var isInEnemyGogosGroup = groups.HasFlag(Groups.EnemyGogos) && player != null && player != allyPlayer;
            var isInUnclaimedGogosGroup = groups.HasFlag(Groups.UnclaimedGogos) && player == null;
            return isInAllyGogosGroup || isInEnemyGogosGroup || isInUnclaimedGogosGroup;
        }
    }
}
