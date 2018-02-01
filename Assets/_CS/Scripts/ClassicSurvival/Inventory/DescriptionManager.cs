using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ClassicSurvival
{
	public class DescriptionManager : MonoBehaviour
	{
		private Item item;

		private Text[] m_ShortDesc;
		public Text[] shortDesc
		{
			get
			{
				if (m_ShortDesc == null)
					m_ShortDesc = GetComponentsInChildren<Text> ();
				return m_ShortDesc;
			}
		}

		//**************************************************************************************************************

		public void FillDescription(Item i)
		{
			item = i;
			shortDesc [0].text = item.name;
			shortDesc [1].text = item.shortDescription;
		}

		public void ClearDescription()
		{
			shortDesc [0].text = "";
			shortDesc [1].text = "";
		}

		public void TapExamine()
		{
			if (item != null) 
			{
				GameObject menuExamine = GameManager.instance.CanvasController.MenuExamine;
				Text name = GameManager.instance.CanvasController.MENombre;
				Text longDesc = GameManager.instance.CanvasController.MEDesc;
				Image icon = GameManager.instance.CanvasController.MEIcon;

				name.text = item.name;
				longDesc.text = item.LongDescription;
				icon.sprite = item.icon;

				GameObject[] openedObjects = GameObject.FindGameObjectsWithTag ("Menu");

				for (int i = 0; i < openedObjects.Length; i++) {
					openedObjects [i].SetActive (false);
				}

				GameManager.instance.CanvasController.OpenedObjects = openedObjects;
				menuExamine.SetActive (true);
			}
			else
				Debug.Log ("No item");

		}
	}
}