using UnityEngine;
using System.Collections.Generic;
using ClassicSurvival;

public class CanvasReaction : Reaction 
{
	public Pickups pickups;

	protected override void ImmediateReaction()
	{
		for (int i = 0; i < pickups.visual.Count; i++) 
		{
			PocketSlot pocketAdd = new PocketSlot(-1, pickups.amounts[i], pickups.items[i]);
			InventoryManager.instance.PickupAdd (pocketAdd);
		}

		GameManager.instance.CanvasController.OpenCloseInventario ();
		GameManager.instance.CanvasController.OpenClosePickup ();
	}
}