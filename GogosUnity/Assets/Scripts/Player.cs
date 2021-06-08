using System.Collections.Generic;

namespace Gogos
{
	public enum PlayerColor { Red, Green, Blue }

	public class Player
	{
		public string Name { get; }

		public PlayerColor PlayerColor { get; }

		public List<ScriptableGogo> Collection { get; private set; } = new List<ScriptableGogo>();

		public Player(string name, PlayerColor playerColor)
        {
			Name = name;
			PlayerColor = playerColor;
        }

		public void AddToCollection(IEnumerable<ScriptableGogo> scriptableGogos)
        {
			Collection.AddRange(scriptableGogos);
        }
	}
}