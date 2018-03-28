using UnityEngine;

[CreateAssetMenu(fileName ="New LItem", menuName = "Inventory/LItem")]
public class LItem : ScriptableObject 
{
	public string name_key;
	[HideInInspector]	public string Nombre;
	[HideInInspector] public string DescripcionCorta;
	[HideInInspector] public string DescripcionLarga;

	public int MaxAmount;

	public string IconoPequeño = "Sprites/Simples/";
	public string IconoGrande = "Sprites/Amplios/";
	public string Modelo = "Primitivas/";

	public virtual void Use()
	{
		Debug.Log ("Item usado: " + Nombre);
	}
}
