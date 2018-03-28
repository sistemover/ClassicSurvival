using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescripcionManager : MonoBehaviour 
{
	public Text nombre;
	public Text descripcion;

	LItem myItem;
	ItemText itemText;

	public void FillDescription(LItem item)
	{
		myItem = item;
		itemText = SimpleGameManager.instance.localizationManager.GetLocalizedItem (myItem.name_key);
		nombre.text = itemText.name;
		descripcion.text = itemText.shortdescription;
	}

	public void BotonExaminar()
	{
		if (myItem == null)
			return;
		GameObject menuExaminar = SimpleGameManager.instance.localizationCanvasController.MenuExaminar;
		ExaminarManager examinarManager = menuExaminar.GetComponent<ExaminarManager> ();

		AsignarIcono (examinarManager.Icono);
		AsignarTextos (examinarManager.Nombre, examinarManager.Descripcion);

		examinarManager.AbrirCerrar ();
	}

	void AsignarIcono(Image icono)
	{
		icono.sprite = Resources.Load<Sprite> (myItem.IconoGrande);
		if (icono.sprite != null)
			icono.enabled = true;
		else
			Debug.Log ("No se encontró Sprite en Carpeta Resources!");
	}
	void AsignarTextos(Text nombre, Text descripcion)
	{
		nombre.text = itemText.name;
		descripcion.text = itemText.longdescription;
	}
}
