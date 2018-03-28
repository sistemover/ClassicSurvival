using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationLocalizationManager : MonoBehaviour 
{
	//LLAVES
	public string StartLanguajeKey;

	//DICCIONARIOS
	private Dictionary<string, string> localizedText;
	private Dictionary<string, ItemText> localizedItem;

	//MENSAJES
	private string missingTextString = "Localized text not found.";
	private string missingItemString = "Localized Item not found.";

	//DELEGATES
	public delegate void OnlocalizacionChanged();
	public OnlocalizacionChanged onLocalizationChangedCallback;

	//**************************************************************************************************************
	
	public void Init () 
	{
		SelectLanguage (StartLanguajeKey);
		//Debug.Log ("LocalizationManager");
	}

	public void LoadLocalizedText(string fileName)
	{
		localizedText = new Dictionary<string, string> ();
		localizedItem = new	Dictionary<string, ItemText> ();

		TextAsset jsonOnTextAsset = Resources.Load ("DataBase/" + fileName) as TextAsset;
		string jsonOnString = jsonOnTextAsset.ToString ();
		LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (jsonOnString);

		for (int i = 0; i < loadedData.text.Length; i++) 
		{
			localizedText.Add (loadedData.text [i].key, loadedData.text [i].value);
		}

		for (int i = 0; i < loadedData.items.Length; i++) 
		{
			ItemText itemText = new ItemText();
		    itemText.name = loadedData.items [i].name;
			itemText.shortdescription = loadedData.items [i].shortdescription;
			itemText.longdescription = loadedData.items [i].longdescription;
			localizedItem.Add (loadedData.items [i].key, itemText);
		}
	}

	public string GetLocalizedText(string key)
	{
		string result = missingTextString;

		if (localizedText.ContainsKey(key))
		{
			result = localizedText [key];
		}
		return result;
	}

	public ItemText GetLocalizedItem(string key)
	{
		ItemText result = new ItemText ();
		result.name = missingItemString;
		if (localizedItem.ContainsKey(key)) {
			result = localizedItem [key];
		}
		return result;
	}

	//Validación e invocación del Callback
	public void UpdateText()
	{
		if (onLocalizationChangedCallback != null) {
			onLocalizationChangedCallback.Invoke ();
		}
	}

	//Botón de cambio de idioma, invoca el Callback onLocalizationChangedCallback
	public void SelectLanguage(string fileName)
	{
		LoadLocalizedText (fileName);
		UpdateText ();
	}
}
