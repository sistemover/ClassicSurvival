using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class CamTrigger : MonoBehaviour 
	{
		public int activeCamera;
		CameraManager cameraManager;
		InteractTrigger interactTrigger;

		//**************************************************************************************************************

		void Start()
		{
			cameraManager = GameObject.Find ("CameraManager").GetComponent<CameraManager> ();
			interactTrigger = GetComponent<InteractTrigger> ();
			if (interactTrigger == null)
				return;
		}

		void OnTriggerStay(Collider other)
		{
			if (other.CompareTag ("Player")) 
				SwitchCameras();
		}

		void SwitchCameras()
		{
			cameraManager.DeactivateCameras (activeCamera);
		}
	}
}