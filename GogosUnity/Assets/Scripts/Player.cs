using System.Collections.Generic;

namespace Gogos
{
	public enum PlayerColor { Red, Green, Blue }

	public class Player
	{
		public string Name { get; }

		public PlayerColor PlayerColor { get; }

		public List<AbstractScriptableGogo> Collection { get; private set; } = new List<AbstractScriptableGogo>();

		public Player(string name, PlayerColor playerColor)
        {
			Name = name;
			PlayerColor = playerColor;
        }

		public void AddToCollection(IEnumerable<AbstractScriptableGogo> scriptableGogos)
        {
			Collection.AddRange(scriptableGogos);
        }
	}
}