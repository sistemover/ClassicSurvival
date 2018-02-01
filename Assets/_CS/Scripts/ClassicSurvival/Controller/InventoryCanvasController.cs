using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace ClassicSurvival
{
	public class InventoryCanvasController : MonoBehaviour 
	{
		public InventoryItem ActualSwitchedInventoryItem;
		public Transform pocketParents;
		public Transform pickupParents;
		public Transform equipParents;

		public DescriptionManager inventoryDescription;
		public DescriptionManager pickupDescription;

		InventoryManager invManager;
		InventoryItem[] pocketItems;
		InventoryItem[] pickupItems;
		InventoryItem[] equipItems;

		CanvasController canvasController;

		public void Init()
		{
			invManager = InventoryManager.instance;
			invManager.onItemChangedCallback += Tick;
			invManager.onPocketSelectedCallback += ClearHighlightInventorySlot;
			invManager.onPickSelectedCallback += ClearHighlightPickupSlot;

			pocketItems = pocketParents.GetComponentsInChildren<InventoryItem> ();
			pickupItems = pickupParents.GetComponentsInChildren<InventoryItem> ();
			equipItems = equipParents.GetComponentsInChildren<InventoryItem> ();

			canvasController = GetComponent<CanvasController> ();
		}

		#region Metodos delegate
		void Tick()
		{
			UpdatePocketSlot ();
			UpdatePickupSlot ();
			UpdateEquipmentSlot ();

			Debug.Log ("****************************** Final del Update ******************************");
		}

		void ClearHighlightInventorySlot()
		{
			for (int i = 0; i < pocketItems.Length; i++) 
			{
				pocketItems [i].Highlight.color = Color.black;
			}
			for (int i = 0; i < equipItems.Length; i++) {
				equipItems [i].Highlight.color = Color.black;
			}
		}

		void ClearHighlightPickupSlot()
		{
			for (int i = 0; i < pickupItems.Length; i++) 
			{
				pickupItems [i].Highlight.color = Color.black;
			}
		}
		#endregion

		public void SelectFirstItem()
		{
			pocketItems [0].SelectItem ();
			Debug.Log ("**********UpdateFirstItemSelected inv");
			if (pickupItems [0].myPocket != null) 
			{
				pickupItems [0].SelectItem ();
				Debug.Log ("**********UpdateFirstItemSelected pick");
			}
		}

		public InventoryItem FetchInventorySlotByID(int ID)
		{
			for (int i = 0; i < pocketItems.Length; i++) 
			{
				if(pocketItems[i].myPocket != null)
					if (pocketItems [i].myPocket.ID == ID)
						return pocketItems [i];
			}
			return null;
		}

		public InventoryItem LastPocketItem()
		{
			InventoryItem last = null;
			for (int i = 0; i < pocketItems.Length; i++) 
			{
				if (pocketItems [i].myPocket != null) 
					last = pocketItems [i];
			}
			return last;
		}

		#region CHECKEA SI EL INVENTORY PICK ESTÁ VACIO PARA DESTRUIRLO.
		public bool CheckPickInventoryVoid()
		{
			if (invManager.visual.Count == 0) 
			{
				StartCoroutine (ApagarMenuPickup ());
				StartCoroutine(DestruirInteraccion ());
				return true;
			}
			return false;
		}

		IEnumerator ApagarMenuPickup()
		{
			yield return new WaitForSeconds (0.01f);
			canvasController.OpenClosePickup ();
			canvasController.OpenCloseMenuButton ();
		}

		IEnumerator DestruirInteraccion()
		{
			invManager.interactable.SetActive (false);
			yield return new WaitForSeconds (0.5f);
			Destroy (invManager.interactable);
			Debug.Log ("Interacción Destruida");
		}

		#endregion

		void UpdatePocketSlot()
		{
			for (int i = 0; i < pocketItems.Length; i++) 
			{
				if (i < invManager.PocketContainer.Count) 
				{
					pocketItems [i].AddItem (invManager.PocketContainer [i], false);
				} 
				else 
				{
					pocketItems [i].ClearSlot ();
				}
			}
		}

		void UpdatePickupSlot ()
		{
			for (int i = 0; i < pickupItems.Length; i++) 
			{
				if (i < invManager.PickupContainer.Count) 
				{
					pickupItems [i].AddItem (invManager.PickupContainer [i], true);
				} 
				else
				{
					pickupItems [i].ClearSlot ();
					pickupItems [i].OffSlot();
				}			
			}
		}

		void UpdateEquipmentSlot()
		{
			for (int i = 0; i < equipItems.Length; i++) {
				if (equipItems[i].myPocket != null)
					equipItems [i].AddItem (equipItems[i].myPocket, false);
				else
					equipItems [i].ClearSlot ();
			}
		}
	}
}