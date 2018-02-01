using UnityEngine;
using System.Collections;

namespace ClassicSurvival
{
	public class Debugging : MonoBehaviour
	{
		InventoryManager invManager;

		public Item[] item;
		public int[] amount;

		void Start ()
		{
			invManager = InventoryManager.instance;
			for (int i = 0; i < item.Length; i++) 
			{

				PocketSlot pocketToAdd = new PocketSlot(-1, amount[i], item[i]);
				invManager.PocketAdd (pocketToAdd);
			}

			/*//codigo que permite identificar los objetos activos que respondan a un TAG específico
			GameObject[] canvasthings = GameObject.FindGameObjectsWithTag ("Menu");
			for (int i = 0; i < canvasthings.Length; i++) {
				Debug.Log(canvasthings [i].name);//*/
		}
	}
}