using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class SpawnPoint : MonoBehaviour 
	{
		public string spawnPointName;

		private static List<SpawnPoint> allSpawnPoints = new List<SpawnPoint> ();

		private void OnEnable()
		{
			allSpawnPoints.Add (this);
		}

		private void OnDisable ()
		{
			allSpawnPoints.Remove (this);
		}

		public static Transform FindSpawnPoints (string pointName)
		{
			for (int i = 0; i < allSpawnPoints.Count; i++) {
				if (allSpawnPoints [i].spawnPointName == pointName)
					return allSpawnPoints [i].transform;
			}

			return null;
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireCube (Vector3.zero + Vector3.up*1, Vector3.one+Vector3.up*1);
			Gizmos.DrawLine (new Vector3( 0.5f , 0.1f , -0.5f), new Vector3(0    , 0.1f , 0.5f));
			Gizmos.DrawLine (new Vector3(-0.5f , 0.1f , -0.5f), new Vector3(0    , 0.1f , 0.5f));
		}
	}
}