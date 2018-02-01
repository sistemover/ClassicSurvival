using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class GameManager : MonoBehaviour
	{
		#region Singleton
		public static GameManager instance;
		void Awake ()
		{
			if (instance != null) 
			{
				Debug.Log ("Hay más de un GameManager instanciado!!");
				return;
			}

			instance = this;
		}
		#endregion

		public event System.Action<Player> OnLocalPlayerJoined;
		public event System.Action<CameraManager> OnLocalCameraJoined;

		bool d_x, d_y, d_a, d_b, u_x, u_y, u_a, u_b, d_BODY, u_BODY;
		Vector2  h_v, x_y;

		private InputController m_InputController;
		public InputController InputController
		{
			get
			{
				if(m_InputController == null)
					m_InputController=gameObject.GetComponent<InputController>();
				return m_InputController;
			}
		}

		private CanvasController m_CanvasController;
		public CanvasController CanvasController
		{
			get
			{
				if(m_CanvasController == null)
					m_CanvasController = GameObject.Find ("CanvasController").GetComponent<CanvasController> ();
				return m_CanvasController;
			}
		}

		private CameraManager m_CameraManager;
		public CameraManager LocalCameraManager
		{
			get
			{
				return m_CameraManager;
			}
			set
			{
				m_CameraManager = value;
				if (OnLocalCameraJoined != null)
					OnLocalCameraJoined (m_CameraManager);
			}
		}

		private Player m_LocalPlayer;
		public Player LocalPlayer
		{
			get
			{
				return m_LocalPlayer;
			}
			set
			{
				m_LocalPlayer = value;
				if (OnLocalPlayerJoined != null)
					OnLocalPlayerJoined (m_LocalPlayer);
			}
		}

		//**************************************************************************************************************

		void Start()
		{
			CanvasController.Init ();
			EquipmentManager.instance.init ();
		}

		void Update()
		{
			float delta = Time.deltaTime;

			InputController.Tick ();
			PassingInputs ();
			CanvasController.Tick (d_BODY, u_BODY);

			if (LocalPlayer != null)
				LocalPlayer.Tick (delta, d_x, d_y, d_a, d_b, u_x, u_y, u_a, u_b);

			/*//Imprime el inventario directamente.
			List<PocketSlot> p = InventoryManager.instance.PocketContainer;
			for (int i = 0; i < p.Count; i++) 
			{
				Debug.Log ("Espacio: " + i + " ID: " + p[i].ID + " contiene: " + p[i].Item.name);
			}//*/
		}

		void FixedUpdate()
		{
			float fixedDelta = Time.fixedDeltaTime;

			InputController.FixedTick ();
			FixedPassingInputs();

			if (LocalPlayer != null)
				LocalPlayer.FixedTick (LocalCameraManager.Cameras[LocalCameraManager.ActiveCamera], fixedDelta, h_v, x_y);
		}

		void PassingInputs()
		{
			d_x = InputController.d_x;
			d_y = InputController.d_y;
			d_a = InputController.d_a;
			d_b = InputController.d_b;
			u_x = InputController.u_x;
			u_y = InputController.u_y;
			u_a = InputController.u_a;
			u_b = InputController.u_b;
			d_BODY = InputController.d_BODY;
			u_BODY = InputController.u_BODY;
		}

		void FixedPassingInputs()
		{
			h_v = InputController.AxisInput;
			x_y = InputController.TouchInput;
		}
	}
}