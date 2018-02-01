using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class PlayerInteractor : MonoBehaviour 
	{
		Interactable interactable;
		public Pickups reactionObjects;

		private FieldOfVision m_FieldOfVision;
		public FieldOfVision FieldOfVision
		{
			get
			{
				if (m_FieldOfVision == null)
					m_FieldOfVision = gameObject.GetComponent<FieldOfVision> ();
				return m_FieldOfVision;
			}
		}


		//**************************************************************************************************************

		public void Tick(bool u_a)
		{
			DoInteraction (u_a);
		}

		public void GetInteraction(Interactable i)
		{
			interactable = i;
		}

		void DoInteraction(bool u_a)
		{
			string targetName;
			string interactName;
			if (FieldOfVision.vTargets.Count != 0) 
			{
				if (FieldOfVision.vTargets [0].Target.GetComponentInParent<Parent> () != null)
					targetName = FieldOfVision.vTargets [0].Target.GetComponentInParent<Parent> ().transform.name;
				else
					targetName = FieldOfVision.vTargets [0].Target.name;
				
				if (interactable != null) 
				{
					interactName = interactable.name;

					if (targetName.Equals (interactName)) 
					{
						if (u_a) 
						{
							
							if (interactable.ReactionObjects != null) 
							{
								reactionObjects = interactable.ReactionObjects;
								InventoryManager.instance.items = reactionObjects.items;
								InventoryManager.instance.amounts = reactionObjects.amounts;
								InventoryManager.instance.visual = reactionObjects.visual;
								InventoryManager.instance.interactable = reactionObjects.transform.parent.gameObject;
							}
							interactable.Interact ();
						}							
					}
				} 
			} 
		}
	}
}