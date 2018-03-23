using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour 
{
	public string key;

	void Start()
	{
		Debug.Log ("LocalizationText");
	}

	void Update()
	{
		CargarTexto ();
	}

	void CargarTexto()
	{
		Text t = GetComponent<Text> ();
		t.text = SimpleGameManager.instance.localizationManager.GetLocalizedText (key);
	}
}
