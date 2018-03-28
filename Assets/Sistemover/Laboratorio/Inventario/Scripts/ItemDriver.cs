﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDriver : MonoBehaviour 
{
	public Image IconContainer;
	public Text AmountContainer;

	[HideInInspector] public LItem myItem;
	[HideInInspector] public SlotType mySlotType;

	public void AgregarItem(string path, int amount)
	{
		myItem = Resources.Load (path) as LItem;

		if (myItem != null) 
			AsignarIcono ();
		AsignarAmount (amount);
	}

	void AsignarIcono()
	{
		IconContainer.sprite = Resources.Load<Sprite> (myItem.IconoPequeño);
		if (IconContainer.sprite != null)
			IconContainer.enabled = true;
		else
			Debug.Log ("No se encontró Sprite en Carpeta Resources!");
	}

	void AsignarAmount(int amount)
	{
		if (amount == 0)
			AmountContainer.text = "";
		else 
			AmountContainer.text = amount.ToString ();
	}

	public void TapSeleccionarItem()
	{
		FillDescription ();
	}

	void FillDescription()
	{
		if (mySlotType.Equals(SlotType.Pocket))
			InventarioManager.instance.InventarioCanvasManager.descripcionManager [0].FillDescription (myItem);
	}
}
