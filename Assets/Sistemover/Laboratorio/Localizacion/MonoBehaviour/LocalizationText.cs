using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour 
{
	public string key;
	private LocalizationLocalizationManager localizationManager;

	void Awake()
	{
		localizationManager = SimpleGameManager.instance.localizationManager;
		localizationManager.onLocalizationChangedCallback += CargarTexto;
		Debug.Log ("LocalizationText");
	}

	void CargarTexto()
	{
		Text t = GetComponent<Text> ();
		t.text = localizationManager.GetLocalizedText (key);
	}
}
