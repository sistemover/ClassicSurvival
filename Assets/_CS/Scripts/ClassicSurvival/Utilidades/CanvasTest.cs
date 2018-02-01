using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTest : MonoBehaviour 
{
	SceneController sceneController;
	GameObject panel;

	void Start()
	{
		sceneController = GameObject.Find ("SceneController").GetComponent<SceneController> ();
		panel = GameObject.Find ("MenuInicio");
	}

	public void Iniciar()
	{
		sceneController.Init ();
		panel.SetActive (false);
	}
}
