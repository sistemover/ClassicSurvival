using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class SpawnPoint : MonoBehaviour 
	{
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