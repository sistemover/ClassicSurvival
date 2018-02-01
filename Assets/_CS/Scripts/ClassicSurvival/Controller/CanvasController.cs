using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClassicSurvival
{
	public class CanvasController : MonoBehaviour 
	{
		//Menus.
		public SceneController sceneController;
		public GameObject MenuInicio;
		public GameObject MenuDocumentos;
		public GameObject MenuInventario;
		public GameObject MenuOpciones;
		public GameObject MenuPickup;
		public GameObject MenuExamine;
		public GameObject MenuButton;
		public GameObject[] OpenedObjects;
		public GameObject[] MenuButtons;

		//MenuExamine
		public Text MENombre;
		public Text MEDesc;
		public Image MEIcon;

		//TouchGamePad
		private GameObject Y;
		private GameObject X;
		private GameObject B;
		private GameObject A;
		private GameObject BS;
		private Image Stick;
		private Image Pad;
		private InventoryManager invManager;

		private void InitTouchGamePad()
		{
			Y = GameObject.Find ("Y");
			X = GameObject.Find ("X");
			B = GameObject.Find ("B");
			A = GameObject.Find ("A");
			BS = GameObject.Find ("BaseStick");

			Stick = GameObject.Find ("Stick[L]").GetComponent<Image> ();
			Pad = GameObject.Find ("TouchPad[R]").GetComponent<Image> ();

			invManager = InventoryManager.instance;
		}

		private InventoryCanvasController m_InventoryCanvasController;
		public InventoryCanvasController InventoryCanvasController
		{
			get
			{
				if(m_InventoryCanvasController == null)
					m_InventoryCanvasController = GetComponent<InventoryCanvasController>();
				return m_InventoryCanvasController;
			}
		}


		//**************************************************************************************************************

		public void Init()
		{
			InitTouchGamePad ();
			MenuDocumentos.SetActive (false);
			MenuInventario.SetActive (false);
			MenuOpciones.SetActive (false);
			MenuPickup.SetActive (false);
			MenuExamine.SetActive (false);
			MenuButton.SetActive (false);
			ActiveDeactiveLeftGamePad (false);
			ActiveDeactiveRightGamePad (false);

			InventoryCanvasController.Init ();
		}

		public void Tick(bool d_BODY, bool u_BODY)
		{
			if (d_BODY)
				OpenCloseMenuButton ();
		}

		//Botón Iniciar del Menú Principal.
		public void Iniciar()
		{
			sceneController.Init ();
			MenuInicio.SetActive (false);
			ActiveDeactiveLeftGamePad (true);
			ActiveDeactiveRightGamePad (true);
		}

		public void OpenCloseInventario()
		{
			MenuInventario.SetActive (true);
			ActiveDeactiveRightGamePad (false);

			//Actualiza el Inventario al abrir.
			if (MenuInventario.activeInHierarchy) 
			{
				Debug.Log ("*********** Actualizando InventarioVisual desde OpenCloseInventario **********");
				invManager.UpdateInventory ();
				InventoryCanvasController.SelectFirstItem ();
			}
		}

		public void OpenClosePickup()
		{
			MenuPickup.SetActive (!MenuPickup.activeSelf);
			ActiveDeactiveLeftGamePad (!MenuPickup.activeInHierarchy);
		}

		public void OpenCloseMenuButton()
		{
			PressInventario ();
			MenuButton.SetActive (true);
		}

		public void PressDocumentos()
		{
			CloseAllMenus ();
			CleanAllMenuButtons ();

			MenuDocumentos.SetActive (true);
			MenuButtons [0].GetComponent<Image> ().color = Color.blue;
		}

		public void PressInventario()
		{
			CloseAllMenus ();

			CleanAllMenuButtons ();

			OpenCloseInventario ();
			MenuButtons [1].GetComponent<Image> ().color = Color.blue;

		}
			
		public void PressOpciones()
		{
			CloseAllMenus ();
			CleanAllMenuButtons ();

			MenuOpciones.SetActive (true);
			MenuButtons [2].GetComponent<Image> ().color = Color.blue;
		}

		public void PressCross()
		{
			if (MenuPickup.activeInHierarchy) 
			{
				invManager.PickupContainer.Clear ();
				//invManager.ClearInteractionPicks ();
				OpenClosePickup ();
			}				

			MenuButton.SetActive (false);
			CloseAllMenus ();
			ActiveDeactiveRightGamePad (true);
		}

		public void CloseAllMenus()
		{
			OpenedObjects = GameObject.FindGameObjectsWithTag ("Menu");
			if (OpenedObjects != null) {
				for (int i = 0; i < OpenedObjects.Length; i++) {
					OpenedObjects [i].SetActive (false);
				}
			}
		}

		public void CleanAllMenuButtons()
		{
			for (int i = 0; i < MenuButtons.Length; i++) {
				MenuButtons [i].GetComponent<Image> ().color = Color.black;
			}
		}

		public void Back()
		{
			Debug.Log ("entro a back");
			if (OpenedObjects[0] != null) 
			{
				for (int i = 0; i < OpenedObjects.Length; i++) 
				{
					OpenedObjects [i].SetActive (true);
				}
			}

			GameObject[] canvasthings = GameObject.FindGameObjectsWithTag ("Menu");
			canvasthings [0].SetActive (false);
		}

		//Gestión Gamepad.
		void ActiveDeactiveLeftGamePad(bool a)
		{
			
			/*if (a)
				Debug.Log ("Activando Left Game Pad");
			else
				Debug.Log ("Desactivando Left Game Pad");//*/
			Y.SetActive(a);
			X.SetActive (a);
			BS.SetActive (a);
			Stick.enabled = a;
		}

		void ActiveDeactiveRightGamePad(bool a)
		{
			/*if (a)
				Debug.Log ("Activando Right Game Pad");
			else
				Debug.Log ("Desactivando Right Game Pad");//*/
			B.SetActive (a);
			A.SetActive (a);
			Pad.enabled = a;
		}

	}
}