using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotDriver : MonoBehaviour 
{
	public SlotType slotType;
	public ItemDriver itemDriver;
	public Image HighlightContainer;

	public string ItemPath;
	public int Amount;


	void Start ()
	{
		itemDriver.mySlotType = slotType;
		itemDriver.AgregarItem (ItemPath, Amount);
	}
}
