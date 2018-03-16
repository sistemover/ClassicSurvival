using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassicSurvival;

public class AutoDestroyReaction : DelayedReaction
{
	public Interactable sendInteraction;
	public GameObject My;

	protected override void ImmediateReaction()
	{
		My.SetActive (false);
		GameManager.instance.LocalPlayer.PlayerInteractor.GetInteraction (sendInteraction);
	}
}
