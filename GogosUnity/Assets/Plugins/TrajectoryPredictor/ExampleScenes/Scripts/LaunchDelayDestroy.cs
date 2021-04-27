using UnityEngine;
using System.Collections;

namespace TrajectoryPrediction
{
	public class LaunchDelayDestroy : MonoBehaviour
	{

		void Start()
		{
			StartCoroutine(DestroyCo());
		}

		IEnumerator DestroyCo()
		{
			yield return new WaitForSeconds(7.5f);
			Destroy(gameObject);
		}
	}

}