using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectReaction : DelayedReaction 
{
	public GameObject gameObject;

	protected override void ImmediateReaction()
	{
		Destroy (gameObject);
	}
}
