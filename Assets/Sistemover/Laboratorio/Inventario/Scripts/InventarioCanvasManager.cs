using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioCanvasManager : MonoBehaviour 
{
	public DescripcionManager[] descripcionManager;
	public Transform PocketItemDriverParent;

	[HideInInspector] public ItemDriver[] PocketItemDrivers;

	public void Init()
	{
		PocketItemDrivers = PocketItemDriverParent.GetComponentsInChildren<ItemDriver> ();//Obteniendo los ItemDrivers del PocketItemDriverParent
		InventarioManager.instance.onItemSelectedCallback += Tick;
	}

	public void Tick()
	{
		SeleccionarPrimerSlot ();
	}

	void SeleccionarPrimerSlot()
	{
		PocketItemDrivers [0].TapSeleccionarItem ();
	}
}
