using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationCanvasController : MonoBehaviour 
{
	public GameObject MenuInicio;
	public GameObject MenuPausa;
	public GameObject IngameCommands;

	public GameObject MIMainPanel;
	public GameObject MIOptionPanel;
	public GameObject MPMainPanel;
	public GameObject MPOptionPanel;

	public void Init()
	{
		GameObject[] menus = {MenuInicio, MenuPausa, IngameCommands, MIMainPanel, MIOptionPanel, MPMainPanel, MPOptionPanel};
		for (int i = 0; i < menus.Length; i++) {
			menus [i].SetActive (true);
		}

		MenuPausa.SetActive (false);
		IngameCommands.SetActive (false);
		MIOptionPanel.SetActive (false);
		MPOptionPanel.SetActive (false);

		Debug.Log ("LocalizationCanvasController");
	}

	public void BotonIniciar()
	{
		MenuInicio.SetActive (!MenuInicio.activeInHierarchy);
		IngameCommands.SetActive (!IngameCommands.activeInHierarchy);
		SimpleGameManager.instance.localizationManager.UpdateText ();
	}
	public void BotonOpciones()
	{
		MIMainPanel.SetActive (!MIMainPanel.activeInHierarchy);
		MIOptionPanel.SetActive (!MIOptionPanel.activeInHierarchy);
	}
	public void BotonSalir()
	{
		Debug.Log ("Cerrando Juego!!");
	}
	public void BotonPausa()
	{
		IngameCommands.SetActive (!IngameCommands.activeInHierarchy);
		MenuPausa.SetActive (!MenuPausa.activeInHierarchy);
	}
	public void BotonOpcionesPausa()
	{
		MPMainPanel.SetActive (!MPMainPanel.activeInHierarchy);
		MPOptionPanel.SetActive (!MPOptionPanel.activeInHierarchy);
	}
	public void BotonMenuInicio()
	{
		MenuInicio.SetActive (!MenuInicio.activeInHierarchy);
		MenuPausa.SetActive (!MenuPausa.activeInHierarchy);
	}
	public void BotonInventario()
	{
		Debug.Log ("Inventario abierto!!");
	}
	public void BotonAtacar()
	{
		Debug.Log ("Ataque!!");
	}

	public void BotonDefender()
	{
		Debug.Log ("Defensa!!");
	}
}
