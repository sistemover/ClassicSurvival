using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ClassicSurvival
{
	public class InventoryItem : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler//, IPointerEnterHandler, IPointerExitHandler
	{
		//Canvas objects
		public Image icon;
		public Image Highlight;
		//public Button removeButton;
		public Text amount;

		//Object container
		public PocketSlot myPocket;

		//Private objects
		private Transform originalParent;
		private Vector3 originalPosition;
		private DescriptionManager descriptionManager;
		Text[] description;

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

		public void AddItem(PocketSlot newItem, bool b)
		{
			myPocket = newItem;
			if (b) 
				Highlight.enabled = true;

			icon.sprite = newItem.Item.icon;
			icon.enabled = true;
			//removeButton.interactable = true;
			if (newItem.Amount > 0)
				amount.text = newItem.Amount.ToString ();
			else
				amount.text = "";
		}

		public void ClearSlot()
		{
			if(myPocket != null)
				myPocket = null;
			icon.sprite = null;
			icon.enabled = false;
			amount.text = "";
		}

		public void OffSlot()
		{
			Highlight.enabled = false;
		}

		/*
		public void OnRemoveButton()
		{
			Debug.Log ("Item Removido");
			InventoryManager.instance.InventoryRemove (pocketSlot);
		}*/
			 
		public void SelectItem()
		{			
			if (this.transform.tag.Equals ("InventorySlot") || this.transform.tag.Equals ("EquipSlot")) 
			{
				InventoryManager.instance.UpdateHighlight (true);
				descriptionManager = invCanvasController.inventoryDescription;
				if (myPocket != null) 
				{
					Highlight.color = Color.blue;
					descriptionManager.FillDescription (myPocket.Item);
				} 
				else 
					descriptionManager.ClearDescription();
			}

			if (this.transform.tag.Equals ("PickUpSlot")) 
			{
				InventoryManager.instance.UpdateHighlight (false);
				descriptionManager = invCanvasController.pickupDescription;
				if (myPocket != null) 
				{
					Highlight.color = Color.red;
					descriptionManager.FillDescription (myPocket.Item);
				} 
				else 
					descriptionManager.ClearDescription();
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{			
			originalParent = this.transform.parent;
			originalPosition = this.transform.position;
			this.transform.SetParent (this.transform.parent.parent.parent.parent);
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (myPocket != null) 
			{
				this.transform.position = eventData.position;
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			this.transform.SetParent (originalParent);
			this.transform.position = originalPosition;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}

		/*
		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log ("Entrando");
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Debug.Log ("Saliendo");
		}*/
	}
}