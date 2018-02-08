using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace ClassicSurvival
{
	public class InventorySlot : MonoBehaviour, IDropHandler
	{
		InventoryManager invManager;
		EquipmentManager eqpManager;
		InventoryCanvasController invCanvasController;

		List<PocketSlot> pocketContainer;
		List<PocketSlot> pickupContainer;

		InventoryItem droppedInventoryItem;
		InventoryItem myInventoryItem;

		public int ID;
		public bool isAdd = false;
		//public PocketSlot pocketEquipment;
		public Equipment myEquipment;

		//**************************************************************************************************************

		public void OnDrop(PointerEventData eventData)
		{
			Init (eventData);

			if (myInventoryItem == null || droppedInventoryItem == null)
				return;

			PocketSlot myPocket = myInventoryItem.myPocket;
			PocketSlot droppedPocket = droppedInventoryItem.myPocket;

			if (droppedPocket == null)
				return;

			#region Procesa de Pocket y Pickup hacia EquipSlot
			if (myInventoryItem.tag.Equals("EquipSlot"))
			{
				#region VALIDACIONES

				//Valida que el PocketSlot sea un item equipable.
				if (!droppedPocket.Item.isEquipment)
					return;

				int cndID;//candidato ID
				int antID;//Anterior ID
				PocketSlot pocketCandidato = null;
				Equipment equipmentCandidato = droppedPocket.Item.GetEquip ();

				#endregion

				//Proceso el equip si proviene de un PocketSlot
				if(droppedInventoryItem.tag.Equals("InventorySlot"))
				{
					#region ASIGNACIONES

					cndID = -1;
					antID = droppedPocket.ID;
					pocketCandidato = new PocketSlot(cndID, droppedPocket.Amount, droppedPocket.Item);

					#endregion

					if (myPocket == null) {
						if (eqpManager.CheckEquipmentIsEquip (equipmentCandidato))//Acá se chequea si puede equiparse o no.
							return;

						if (!equipmentCandidato.equipMainType.Equals (EquipmentMainType.Being))//Validar que sean solo de tipo BEING.
							return;

						AñadirCandidatoPocket (pocketCandidato);
						AsignarCandidatoEquipment (equipmentCandidato);
					} else {
						Debug.Log ("Checkando si existe en currentEquipment");
						if (eqpManager.CheckEquipmentIsEquip (equipmentCandidato)) { //Acá chequea que pueda cambiarse o no.
							Debug.Log ("Verificando si está en este my");
							if (equipmentCandidato.equipSlot.Equals (myEquipment.equipSlot)) { //Si equipSlot se encuentra en my
								Debug.Log ("Verificando si es Being");
								if (equipmentCandidato.equipMainType.Equals (EquipmentMainType.Being) && !equipmentCandidato.equipSubType.Equals(EquipmentSubType.Special)) {
									CambiarCandidatoPocket (pocketCandidato, antID, true);
									AsignarCandidatoEquipment (equipmentCandidato);
								} else {
									if (equipmentCandidato.equipSubType.Equals (myEquipment.equipSubType)) {
										Debug.Log ("Metiendo Munición");
										if(myEquipment.equipSlot.Equals(EquipmentType.Weapon) || myEquipment.equipSlot.Equals(EquipmentType.Armor))
										{
											CombinandoAmounts (true);
										}
										else if(myEquipment.equipSlot.Equals(EquipmentType.Combat))
										{
											if(droppedPocket.Amount == 0)
												return;
											droppedPocket.Amount--;
											myPocket.Amount=myPocket.Item.maxAmount;
										}


									} else {
										Debug.Log ("No es del mismo tipo de munición");
									}
								}
							} else
								return;
						} else {
							CambiarCandidatoPocket (pocketCandidato, antID, true);
							AsignarCandidatoEquipment (equipmentCandidato);
						}
					}
				}

				//Procesa el equip si proviene de un PickSlot
				if (droppedInventoryItem.tag.Equals ("PickUpSlot")) {
					#region ASIGNACIONES

					cndID = -2;
					pocketCandidato = new PocketSlot (cndID, droppedPocket.Amount, droppedPocket.Item);

					#endregion

					Debug.Log ("Vengo de un PickSlot");
				}

				ActualizandoInventario(-1, 3);

				return;
			}
			#endregion

			#region Procesa de PickupContainer hacia PocketContainer.
			if (droppedInventoryItem.tag.Equals ("PickUpSlot")) 
			{
				#region Validaciones
				//Valida que no se permita intercambio entre Pickslots.
				if (myInventoryItem.transform.tag.Equals ("PickUpSlot")) 
					return;

				//Valida que el drop no sea procesado por un slot que no sea InventorySlot.
				if(!myInventoryItem.transform.tag.Equals("InventorySlot"))
					return;
				
				#endregion

				if (myPocket == null) //Procesa el Drop cuando my no tiene contenido.
				{
					int newID = SetPickupOnVoidPocket(droppedPocket);
					ActualizandoInventario(newID, 2);
					return;
				}

				if (myPocket.Item.isStackable && droppedPocket.Item.isStackable && myPocket.Item.Equals(droppedPocket.Item)) //Procesa el drop cuando my y drop son Stackables y del mismo tipo.
				{
					//CombinePickupToPocket(myPocket, droppedPocket);
					CombinandoAmounts(false);
					ActualizandoInventario(myPocket.ID, 2);
					return;
				}

				else //Procesa el drop y my intercambiandolos de inventarios.
				{
					SwitchPickupToPocket(myPocket, droppedPocket);
					ActualizandoInventario(-1,4);
					return;
				}
			}
			#endregion

			#region Procesa de PocketContainer hacia PocketContainer.
			if (droppedInventoryItem.tag.Equals("InventorySlot")) 
			{
				if (myPocket == null)
				{
					SwitchPocketToVoid (droppedPocket);
					ActualizandoInventario(droppedPocket.ID, 1);
					return;
				}

				if (droppedPocket.Item.isStackable && myPocket.Item.isStackable && droppedPocket.Item.Equals(myPocket.Item))
				{
					CombinandoAmounts(true);
					ActualizandoInventario(myPocket.ID, 1);
					return;
				}

				else
				{
					invManager.SwitchItemByItem(myPocket, droppedPocket);
					ActualizandoInventario(droppedPocket.ID, 1);
					return;
				}
			}
			#endregion
		}

		//***************************************************************************************************************************************************************
		//***************************************************************************************************************************************************************
		//***************************************************************************************************************************************************************

		void Init(PointerEventData eventData)
		{
			invManager = InventoryManager.instance;
			eqpManager = EquipmentManager.instance;
			invCanvasController = GameManager.instance.CanvasController.InventoryCanvasController;

			pocketContainer = invManager.PocketContainer;
			pickupContainer = invManager.PickupContainer;

			droppedInventoryItem = eventData.pointerDrag.GetComponent<InventoryItem> ();
			myInventoryItem = GetComponentInChildren<InventoryItem> ();
		}

		#region Pocket a Pocket

		void SwitchPocketToVoid(PocketSlot drop)
		{
			pocketContainer.Remove (drop);
			pocketContainer.Add (drop);
		}

		#endregion

		#region Pickup a Pocket

		int SetPickupOnVoidPocket(PocketSlot a)
		{
			int ID = invManager.PocketAdd (a);
			invManager.PickupRemove (a);
			return ID;
		}

		void SwitchPickupToPocket(PocketSlot my, PocketSlot drop)
		{
			GameObject oldVisual = invManager.visual [drop.ID];
			GameObject newVisual = my.Item.gameObject;

			int myID = my.ID;
			int dropID = drop.ID;

			PocketSlot newPocket = new PocketSlot (myID, drop.Amount, drop.Item);
			PocketSlot newPickup = new PocketSlot(dropID, my.Amount, my.Item);

			pocketContainer [invManager.FetchItemIDByID (newPocket.ID)] = newPocket;
			pickupContainer [dropID] = newPickup;

			invManager.items [dropID] = newPickup.Item;
			invManager.amounts [dropID] = newPickup.Amount;

			newVisual.transform.position = oldVisual.transform.position;
			newVisual.transform.rotation = oldVisual.transform.rotation;
			invManager.visual [dropID] = newVisual;

			GameObject n = Instantiate (newVisual);
			n.transform.SetParent (invManager.interactable.transform);
			invManager.visual [dropID] = n;

			Destroy (oldVisual);
		}

		#endregion

		#region EQUIPMENT

		public void AñadirCandidatoPocket (PocketSlot candidato)
		{
			PocketSlot drop = droppedInventoryItem.myPocket;
			myInventoryItem.myPocket = candidato;
			if (candidato.ID == -1)
				invManager.PocketContainer.Remove (drop);
			else if (candidato.ID == -2)
				invManager.PickupContainer.Remove (drop);
		}

		public void AsignarCandidatoEquipment(Equipment equipCandidato)
		{
			if (myEquipment != null)
				eqpManager.RemoveEquip (myEquipment);

			myEquipment = equipCandidato;
			myEquipment.Use ();
		}

		public void CambiarCandidatoPocket(PocketSlot candidato, int ID, bool direccion)
		{
			PocketSlot my = myInventoryItem.myPocket;
			myInventoryItem.myPocket = candidato;

			Debug.Log (candidato.Item.name + " " + my.Item.name);

			if (direccion)//de inv a eqp.
				invManager.PocketContainer [invManager.FetchItemIDByID (ID)] = new PocketSlot (ID, my.Amount, my.Item);
			else//de pkp a eqp.
				invManager.PickupContainer [ID] = new PocketSlot (ID, my.Amount, my.Item);
		}

		#endregion

		#region MÉTODOS COMUNES

		void ActualizandoInventario(int ID, int type)
		{
			Debug.Log ("************* Actualizando InventarioVisual desde InventorySlot **************");
			invManager.UpdateInventory ();


			if (type == 1 || type == 2) {
				invCanvasController.FetchInventorySlotByID (ID).SelectItem();
			} else if (type == 3) {
				myInventoryItem.SelectItem ();
			}else if (type == 4) {
				myInventoryItem.SelectItem ();
				droppedInventoryItem.SelectItem ();
			}
			if (type == 2) {
				if (!invCanvasController.CheckPickInventoryVoid ()) {
					invManager.UpdateHighlight (false);
					invCanvasController.pickupDescription.ClearDescription ();
				}
			}
		}

		public void CombinandoAmounts (bool type)
		{
			Debug.Log ("Combinando Amounts...");
			PocketSlot drop = droppedInventoryItem.myPocket;
			int dropAmount = drop.Amount;

			dropAmount = Alimentando (dropAmount);
			if (dropAmount == 0) {
				if (type)
					invManager.PocketContainer.Remove (drop);
				else
					invManager.PickupRemove (drop);
			}
			else
				drop.Amount = dropAmount;
		}

		public int Alimentando(int c)
		{
			int ocupado = 0;
			PocketSlot my = myInventoryItem.myPocket;
			for (int i = 0; i < c; i++) {
				if (my.Amount == my.Item.maxAmount)
					break;
				ocupado++;
				my.Amount = my.Amount + 1;
			}
			c = c - ocupado;
			return c;
		}

		#endregion
	}
}