using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class EquipmentManager : MonoBehaviour 
	{
		#region Singleton
		public static EquipmentManager instance;
		void Awake()
		{
			instance = this;
		}
		#endregion

		Equipment[] currentEquipment;

		public void init()
		{
			int numSlots = System.Enum.GetNames (typeof(EquipmentType)).Length;
			currentEquipment = new Equipment[numSlots];
		}

		public void Equip(Equipment newItem)
		{
			int slotIndex = (int)newItem.equipSlot;

			currentEquipment[slotIndex] = newItem;
		}

		public void RemoveEquip (Equipment newItem)
		{
			int slotIndex = (int)newItem.equipSlot;

			currentEquipment [slotIndex] = null;
		}

		public bool CheckEquipmentIsEquip(Equipment candidato)
		{
			InventorySlot[] eSlots = GameManager.instance.CanvasController.InventoryCanvasController.equipParents.GetComponentsInChildren<InventorySlot>();

			for (int i = 0; i < eSlots.Length; i++) 
			{
				if (eSlots [i].myEquipment != null) 
				{
					if (eSlots [i].myEquipment.equipSlot.Equals (candidato.equipSlot)) {
						Debug.Log ("Objeto ya en equipmentslot");
						return true;
					} else {
						Debug.Log ("Objeto permitido");
					}
				}
			}
			return false;
		}
	}
}
