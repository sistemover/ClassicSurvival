using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ClassicSurvival
{
	public class InventoryManager : MonoBehaviour 
	{
		#region Singleton
		public static InventoryManager instance;
		void Awake ()
		{
			if (instance != null) 
			{
				Debug.Log ("Hay más de un inventario instanciado!!");
				return;
			}

			instance = this;
		}
		#endregion

		//Delegates
		public delegate void OnItemChanged();
		public OnItemChanged onItemChangedCallback;
		public delegate void OnPickSelected();
		public OnPickSelected onPickSelectedCallback;
		public delegate void OnPocketSelected();
		public OnPocketSelected onPocketSelectedCallback;


		public int space = 6;

		public List<PocketSlot> PocketContainer = new List<PocketSlot> ();
		public List<PocketSlot> PickupContainer = new List<PocketSlot> ();

		//Interactions Picks.
		public List<Item> items = new List<Item>();
		public List<int> amounts = new List<int>();
		public List<GameObject> visual = new List<GameObject>();

		public GameObject interactable;

		//**************************************************************************************************************

		public int PocketAdd(PocketSlot p)
		{
			int ID = GetPocketID ();
			p = new PocketSlot (ID, p.Amount, p.Item);
			PocketContainer.Add (p);
			return ID;
		}

		public void PickupAdd(PocketSlot p)
		{
			PickupContainer.Add (p);
			SetPickupID ();
		}

		public void PickupRemove(PocketSlot p)
		{ 
			GameObject vp = null;

			vp = visual [p.ID];

			PickupContainer.Remove (p);

			items.Remove (p.Item);
			amounts.Remove (p.Amount);
			visual.Remove (vp);

			SetPickupID ();

			Destroy (vp);
		}

		public int FetchItemIDByID(int ID)
		{
			for (int i = 0; i < PocketContainer.Count; i++)
				if (PocketContainer [i].ID == ID)
					return i;
			return -1;
		}

		public void SwitchItemByItem(PocketSlot a, PocketSlot b)
		{
			int IDa = FetchItemIDByID (a.ID);
			int IDb = FetchItemIDByID (b.ID);

			if (IDa < IDb) 
			{
				PocketContainer [IDb] = PocketContainer [IDa];
				PocketContainer [IDa] = b;
			} 
			else 
			{
				PocketContainer [IDa] = PocketContainer [IDb];
				PocketContainer [IDb] = a;
			}
		}

		public void ClearInteractionPicks()
		{
			for (int i = 0; i < items.Count; i++) 
			{
				items [i] = null;
				amounts [i] = 0;
				visual [i] = null;
			}

		}

		int GetPocketID()
		{
			if (PocketContainer.Count == 0) 
				return 0;
			else if (PocketContainer.Count == 1) 
				return PocketContainer [0].ID + 1;
			else if (PocketContainer.Count > 1) 
			{			
				int actID = 0;
				for (int i = 0; i < PocketContainer.Count; i++) 
				{
					if (i == 0) 
						actID = PocketContainer [i].ID;
					else 
						if (actID < PocketContainer [i].ID)
							actID = PocketContainer [i].ID;
				}
				return actID + 1;
			}
			return -1;
		}

		void SetPickupID()
		{
			for (int i = 0; i < PickupContainer.Count; i++) 
			{
				PickupContainer [i].ID = i;
			}
		}

		//Método que actualiza el inventario visual.
		public void UpdateInventory()
		{
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}

		public void UpdateHighlight(bool b)
		{
			if (b) 
			{
				if (onPocketSelectedCallback != null)
					onPocketSelectedCallback.Invoke ();
			}
			else 
			{
				if (onPickSelectedCallback != null)
					onPickSelectedCallback.Invoke ();
			}
		}

		/*
		public int FetchItemIDByID(int ID)
		{
			Debug.Log ("Fetch");
			for (int i = 0; i < inventoryContainer.Count; i++)
				if (inventoryContainer [i].ID == ID)
					return i;
			return -1;
		}

		/*
		public void SwitchItemByItem(PocketSlot a, PocketSlot b)
		{
			int IDa = FetchItemIDByID (a.ID);
			int IDb = FetchItemIDByID (b.ID);

			if (IDa < IDb) 
			{
				inventoryContainer [IDb] = inventoryContainer [IDa];
				inventoryContainer [IDa] = b;
			} 
			else 
			{
				inventoryContainer [IDa] = inventoryContainer [IDb];
				inventoryContainer [IDb] = a;
			}
		}

		public void SwitchEquipmentOnInventory (PocketSlot a, PocketSlot b)
		{
			if (FetchItemIDByID (a.ID) == -1) 
			{
				Debug.Log (a.Item.name);
				PocketContainer [FetchItemIDByID (b.ID)] = a;
			} 
			else 
			{
				PocketContainer [FetchItemIDByID (a.ID)] = b;
			}
		}*/
	}

	public class PocketSlot
	{
		public int ID{ get; set;}
		public int Amount { get; set; }
		public Item Item { get; set;}

		public PocketSlot (int i, int a, Item c)
		{
			this.ID = i;
			this.Amount = a;
			this.Item = c;
		}
	}
}