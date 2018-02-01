using UnityEngine;
using ClassicSurvival;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item 
{
	public EquipmentType equipSlot;
	public EquipmentMainType equipMainType;
	public EquipmentSubType equipSubType;

	public override void Use()
	{
		base.Use ();
		EquipmentManager.instance.Equip (this);
	}

	public override Equipment GetEquip ()
	{
		//base.GetEquip ();
		return this;
	}
}

public enum EquipmentType {Armor, Combat, Weapon}
public enum EquipmentMainType {Being, Food}
public enum EquipmentSubType {Special, Pistola, Metralleta, Escopeta, Cuchillo, Espada, Fierro, PapelRevista, PapelNormal, CartónReforzado}
