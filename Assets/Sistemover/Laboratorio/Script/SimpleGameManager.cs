using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGameManager : MonoBehaviour 
{
	#region Singleton
	public static SimpleGameManager instance;
	void Awake ()
	{
		if (instance != null) 
		{
			Debug.Log ("Hay'más de un GameManager instanciado!!");
			return;
		}
		instance = this;
	}
	#endregion

	//EVENTOS
	public event System.Action<PlayerManager> OnLocalPlayerJoined;


	//INSTANCIADORES
	private PlayerManager m_LocalPlayer;
	public PlayerManager LocalPlayer
	{
		get{return m_LocalPlayer; }
		set
		{
			m_LocalPlayer = value;
			if (OnLocalPlayerJoined != null)
				OnLocalPlayerJoined (m_LocalPlayer);
		}
	}

	private LocalizationLocalizationManager m_LocalizationManager;
	public LocalizationLocalizationManager localizationManager
	{
		get
		{
			if (m_LocalizationManager == null)
				m_LocalizationManager = gameObject.GetComponent<LocalizationLocalizationManager> ();
			return m_LocalizationManager;
		}
	}

	private LocalizationCanvasController m_LocalizationCanvasController;
	public LocalizationCanvasController localizationCanvasController
	{
		get
		{
			if (m_LocalizationCanvasController == null)
				m_LocalizationCanvasController = gameObject.GetComponent<LocalizationCanvasController> ();
			return m_LocalizationCanvasController;
		}
	}

	//**************************************************

	void Start () 
	{
		//Debug.Log ("SimpleGameManager");
		localizationCanvasController.Init ();
		localizationManager.Init ();
	}
	
	void Update () 
	{
		if (LocalPlayer != null)
			LocalPlayer.Tick ();
	}
}
