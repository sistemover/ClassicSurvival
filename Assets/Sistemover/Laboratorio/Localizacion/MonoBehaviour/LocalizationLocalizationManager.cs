using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationLocalizationManager : MonoBehaviour 
{
	public string StartLanguajeKey;

	private Dictionary<string, string> localizedText;
	private Dictionary<string, ItemText> localizedItem;
	private string missingTextString = "Localized text not found";
	
	public void Init () 
	{
		LoadLocalizedText (StartLanguajeKey);
		Debug.Log ("LocalizationManager");
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
}
