using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ClassicSurvival
{
	public class UseSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
	{
		public Image slot;

		Item item;
		InventoryItem droppedInventoryItem;
		bool isPocketSlot;

		InventoryManager m_InvManager;
		InventoryManager invManager
		{
			get
			{
				if (m_InvManager == null)
					m_InvManager = InventoryManager.instance;
				return m_InvManager;
			}
		}

		InventoryCanvasController m_InvCanvasController;
		InventoryCanvasController invCanvasController
		{
			get
			{
				if(m_InvCanvasController == null)
					m_InvCanvasController = GameManager.instance.CanvasController.InventoryCanvasController;
				return m_InvCanvasController;
			} 
		}

		//**************************************************************************************************************

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData.pointerDrag == null)
				return;
			
			droppedInventoryItem = eventData.pointerDrag.GetComponent<InventoryItem> ();

			if (!CheckDragState ())
				return;

			GetItem ();

			if (!item.isUsable)
				slot.color = Color.red;
			else
				slot.color = Color.blue;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			slot.color = Color.black;
		}

		public void OnDrop(PointerEventData eventData)
		{
			droppedInventoryItem = eventData.pointerDrag.GetComponent<InventoryItem> ();

			if (!CheckDragState ())
				return;
			
			GetItem ();

			if (!item.isUsable)
				return;
			
			if (isPocketSlot) 
			{
				#region VALIDAR CONDICIÓN

				//VALIDAR LA CONDICION DE USO DEL ITEM.

				//USAR EL ITEM.
				item.Use();

				#endregion

				if (droppedInventoryItem.myPocket.Amount == 0 || droppedInventoryItem.myPocket.Amount == 1) 
				{
					invManager.PocketContainer.Remove(droppedInventoryItem.myPocket);
					invCanvasController.SelectFirstItem ();
				} 
				else 
				{
					droppedInventoryItem.myPocket.Amount --;
					invCanvasController.FetchInventorySlotByID (droppedInventoryItem.myPocket.ID).SelectItem ();

				}
			} 
			else 
			{
				#region VALIDAR CONDICIÓN

				//VALIDAR LA CONDICION DE USO DEL ITEM.

				//USAR EL ITEM.
				item.Use();

				#endregion

				if (droppedInventoryItem.myPocket.Amount == 0 || droppedInventoryItem.myPocket.Amount == 1) 
				{
					invManager.PickupRemove(droppedInventoryItem.myPocket);

					if (!invCanvasController.CheckPickInventoryVoid ()) 
					{
						invManager.UpdateHighlight (false);
						invCanvasController.pickupDescription.ClearDescription ();
					}
				} 
				else 
				{
					droppedInventoryItem.myPocket.Amount --;
					droppedInventoryItem.SelectItem ();
				}
			}

			Debug.Log ("************* Actualizando InventarioVisual desde UseSlot  *******************");
			invManager.UpdateInventory ();
			
		}

		void GetItem()
		{
			if (droppedInventoryItem.transform.tag.Equals ("InventorySlot")) 
			{
				item = droppedInventoryItem.myPocket.Item;
				isPocketSlot = true;
			}

			if (droppedInventoryItem.transform.tag.Equals ("PickUpSlot")) 
			{
				item = droppedInventoryItem.myPocket.Item;
				isPocketSlot = false;
			}
		}

		bool CheckDragState()
		{
			if (droppedInventoryItem.transform.tag.Equals ("InventorySlot")) 
			{
				if (droppedInventoryItem.myPocket != null)
					return true;
			}

			if (droppedInventoryItem.transform.tag.Equals ("PickUpSlot")) 
			{
				if (droppedInventoryItem.myPocket != null)
					return true;
			}
			return false;
		}
	}
}

