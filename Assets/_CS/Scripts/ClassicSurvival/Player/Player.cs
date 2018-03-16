using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class Player : MonoBehaviour 
	{
		public GameObject activeModel;

		public Rigidbody m_RigidBody;
		public CapsuleCollider m_CapsuleCollider;
		public Animator m_Animator;

		float a_h, a_v, t_x, t_y;
		bool m_d_x, m_d_y, m_d_a, m_d_b, m_u_x, m_u_y, m_u_a,m_u_b, h_x, h_y, h_a, h_b;

		public const string startingPositionKey = "starting position";
		public SaveData playerSaveData;

		private Interactable interactable;

		private PlayerAnimation m_PlayerAnimation;
		public PlayerAnimation PlayerAnimation
		{
			get
			{
				if (m_PlayerAnimation == null)
					m_PlayerAnimation = gameObject.GetComponent<PlayerAnimation> ();
				return m_PlayerAnimation;
			}
		}

		private PlayerMove m_PlayerMove;
		public PlayerMove PlayerMove
		{
			get
			{
				if (m_PlayerMove == null)
					m_PlayerMove = gameObject.GetComponent<PlayerMove> ();
				return m_PlayerMove;
			}
		}

		private PlayerInteractor m_PlayerInteractor;
		public PlayerInteractor PlayerInteractor
		{
			get
			{
				if (m_PlayerInteractor == null)
					m_PlayerInteractor = gameObject.GetComponent<PlayerInteractor> ();
				return m_PlayerInteractor;
			}
		}

		//**************************************************************************************************************

		void Awake()
		{
			JoinPlayer ();
		}

		void Start()
		{
			m_Animator = activeModel.GetComponent<Animator> ();
			m_CapsuleCollider = GetComponent<CapsuleCollider>();
			m_RigidBody = GetComponent<Rigidbody>();
			PlayerMove.Init (m_RigidBody, m_Animator);
			SetStartPositionPlayer ();
		}

		public void FixedTick(GameObject cam , float f_d, Vector2 axis, Vector2 touch)
		{
			Axis (axis);
			Touch (touch);

			//Llamada de métodos
			PlayerMove.FixedTick (cam, f_d, a_h, a_v);
		}

		public void Tick(float d, bool d_x, bool d_y, bool d_a, bool d_b, bool u_x, bool u_y, bool u_a, bool u_b)
		{
			XYAB (d_x, d_y, d_a, d_b, u_x , u_y, u_a, u_b);

			//Llamada de métodos
			PlayerMove.Tick (d);
			PlayerInteractor.Tick (u_a);
		}

		void Axis(Vector2 axis)
		{
			a_h = axis.x;
			a_v = axis.y;
		}

		void Touch(Vector2 touch)
		{

		}

		void XYAB (bool d_x, bool d_y, bool d_a, bool d_b, bool u_x, bool u_y, bool u_a, bool u_b)
		{
			
		}

		void SetStartPositionPlayer()
		{
			string startingPositionName = "";
			playerSaveData.Load(startingPositionKey, ref startingPositionName);

			Transform spawnPoint = SpawnPoint.FindSpawnPoints (startingPositionName);

			transform.position = spawnPoint.position;
			transform.rotation = spawnPoint.rotation;
		}

		void JoinPlayer()
		{
			GameManager.instance.LocalPlayer = this;
		}
	}
}