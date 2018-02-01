using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject 
{
	new public string name = "New Item";
	[TextArea(1,3)]
	public string shortDescription = "New Description for..";
	[TextArea(1,21)]
	public string LongDescription = "New Description more detailed for...";

	public int maxAmount;

	public bool isStackable = false;
	public bool isUsable = false;
	public bool isEquipment = false;

	public Sprite icon = null;
	public GameObject gameObject = null;

	public virtual void Use()
	{
		//Se usa el item, por lo que algo debería pasar.
		Debug.Log("Usado el item: "+ name);
	}

	public virtual Equipment GetEquip()
	{
		return null;
	}
}