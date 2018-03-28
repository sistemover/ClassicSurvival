using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour 
{
	#region Singleton
	public static InventarioManager instance;
	void Awake ()
	{
		if (instance != null) 
		{
			Debug.Log ("Hay'más de un InventarioManager instanciado!!");
			return;
		}
		instance = this;
	}
	#endregion

	//Delegates
	public delegate void OnItemChanged();
	public OnItemChanged onItemSelectedCallback;

	private InventarioCanvasManager m_inventarioCanvasManager;
	public InventarioCanvasManager InventarioCanvasManager
	{
		get
		{
			if (m_inventarioCanvasManager == null)
				m_inventarioCanvasManager = gameObject.GetComponent<InventarioCanvasManager> ();
			return m_inventarioCanvasManager;
		}
	}

	void Start()
	{
		InventarioCanvasManager.Init ();
	}

	public void ActualizarInventario()
	{
		if(onItemSelectedCallback != null)
			onItemSelectedCallback.Invoke();
	}
}
