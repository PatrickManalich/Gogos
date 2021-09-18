using UnityEngine;

namespace Gogos
{
	public class PlayerDetailsCreator : MonoBehaviour
	{
        [SerializeField]
        private GameObject m_PlayerDetailsPrefab;

		[SerializeField]
		private PlayerTracker m_PlayerTracker;

        private void Start()
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}

			foreach (var player in m_PlayerTracker.Players)
            {
				var playerDetails = Instantiate(m_PlayerDetailsPrefab, transform).GetComponent<PlayerDetails>();
				playerDetails.SetDetails(player);
				playerDetails.name = playerDetails.name.Replace("(Clone)", player.Name);
			}
		}
	}
}