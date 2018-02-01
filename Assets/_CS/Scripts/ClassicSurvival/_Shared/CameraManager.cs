using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class CameraManager : MonoBehaviour 
	{
		public GameObject[] Cameras;
		public int ActiveCamera;

		//**************************************************************************************************************

		void Awake()
		{
			JoinCamera ();
			//Cameras = GameObject.FindGameObjectsWithTag ("MainCamera");
		}

		void Start()
		{
			DeactivateCameras (ActiveCamera);
		}

		public void Tick()
		{
			
		}

		public void DeactivateCameras(int a)
		{
			ActiveCamera = a;
			for (int i = 0; i < Cameras.Length; i++) {
				Cameras [i].SetActive (false);
			}
			//a = a - 1;
			Cameras[ActiveCamera].SetActive (true);
		}

		void JoinCamera()
		{
			GameManager.instance.LocalCameraManager = this;
		}
	}
}