using System;

namespace Gogos
{
    [Flags]
    public enum Groups { AllyGogos = 1, EnemyGogos = 2, UnclaimedGogos = 4, Gems = 8, Bombs = 16, Crates = 32 } // Index 0 is for "Nothing" in the editor

    public enum GroupTag { None, Gogo, Gem, Bomb, Crate }

    public static class GroupExtensions
    {
        public static GroupTag GetGroupTag(string tag)
        {
            switch (tag)
            {
                case "Gogo":
                    return GroupTag.Gogo;

                case "Gem":
                    return GroupTag.Gem;

                case "Bomb":
                    return GroupTag.Bomb;

                case "Crate":
                    return GroupTag.Crate;

                default:
                    return GroupTag.None;
            }
        }

        public static bool IsInGroup(this Groups groups, GroupTag groupTag, Player player, Player allyPlayer)
        {
            switch (groupTag)
            {
                case GroupTag.Gogo:
                    var isInAllyGogosGroup = groups.HasFlag(Groups.AllyGogos) && player == allyPlayer;
                    var isInEnemyGogosGroup = groups.HasFlag(Groups.EnemyGogos) && player != null && player != allyPlayer;
                    var isInUnclaimedGogosGroup = groups.HasFlag(Groups.UnclaimedGogos) && player == null;
                    return isInAllyGogosGroup || isInEnemyGogosGroup || isInUnclaimedGogosGroup;

                case GroupTag.Gem:
                    return groups.HasFlag(Groups.Gems);

                case GroupTag.Bomb:
                    return groups.HasFlag(Groups.Bombs);

                case GroupTag.Crate:
                    return groups.HasFlag(Groups.Crates);

                case GroupTag.None:
                default:
                    return false;
            }
        }
    }
}
