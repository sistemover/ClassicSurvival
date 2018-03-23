using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	[System.Serializable]
	public class Instanciador
	{
		public GameObject Point;
		public bool IsActive;
		public string Path;
	}

	private DiccionarioRecursos ins;

	public List<Instanciador> Instanciadores = new	List<Instanciador>();

	void Start()
	{
		ins = DiccionarioRecursos.instance;
		Instanciar ();
	}

	void Instanciar()
	{
		for (int i = 0; i < Instanciadores.Count; i++) 
		{
			Instanciador spawn = Instanciadores [i];

			if (Instanciadores [i].IsActive) 
			{
				GameObject obj = Instantiate (Resources.Load (spawn.Path) as GameObject);

				Debug.Log (obj.name +" "+spawn.Point.name);

				obj.transform.SetParent (spawn.Point.transform);
				obj.transform.position = spawn.Point.transform.position;
			} 
			else 
			{
				spawn.Point.SetActive (false);
			}
		}
	}
}
