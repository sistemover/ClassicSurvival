using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExaminarManager : MonoBehaviour {

	public Image Icono;
	public Text Nombre;
	public Text Descripcion;

	public void AbrirCerrar()
	{
		gameObject.SetActive (!gameObject.activeInHierarchy);
		GameObject menuInventario = SimpleGameManager.instance.localizationCanvasController.MenuInventario;
		menuInventario.SetActive (!menuInventario.activeInHierarchy);
	}
}
