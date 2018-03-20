using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConditions : MonoBehaviour {

	public AllConditions condiciones;

	void Update ()
	{
		for (int i = 0; i < condiciones.conditions.Length; i++) {
			Debug.Log (condiciones.conditions [i].description + " " +condiciones.conditions[i].satisfied);
		}
	}

	public void ResetearCondiciones()
	{
		condiciones.Reset ();
	}
}
