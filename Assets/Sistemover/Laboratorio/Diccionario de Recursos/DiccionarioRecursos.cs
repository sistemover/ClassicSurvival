using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiccionarioRecursos : MonoBehaviour 
{
	[System.Serializable]
	public class Recurso
	{
		public string Nombre = "NOMBRE";
		public string Path = "PATH";
	}

	public List<Recurso> Diccionario = new List<Recurso>();

	void Añadir()
	{
		Diccionario.Add (new Recurso());
	}

	void Quitar(int i)
	{
		Diccionario.RemoveAt (i);
	}
}

