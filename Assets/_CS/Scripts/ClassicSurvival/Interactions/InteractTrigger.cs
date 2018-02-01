using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class InteractTrigger : MonoBehaviour 
	{
		//GameManager gameManager;
		Player p;
		Interactable interactable;

		//**************************************************************************************************************

		void OnTriggerEnter(Collider c)
		{
			if (c.CompareTag ("Player")) 
			{
				interactable = GetComponent<Interactable> ();
				c.transform.GetComponent<Player> ().PlayerInteractor.GetInteraction (interactable);
			}						
		}

		void OnTriggerExit(Collider c)
		{
			if (c.CompareTag ("Player")) 
			{
				c.transform.GetComponent<Player> ().PlayerInteractor.GetInteraction (null);
			}	
		}
	}
}