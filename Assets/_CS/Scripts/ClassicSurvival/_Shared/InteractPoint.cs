using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class InteractPoint : MonoBehaviour 
	{
		void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireCube (Vector3.zero+Vector3.up*4.5f, new Vector3(5,10,5));
		}
	}
}