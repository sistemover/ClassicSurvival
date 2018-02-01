using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class PlayerAnimation : MonoBehaviour 
	{
		Animator animator;

		//****************************************************************************************************		
		public void AwakeInit(Animator a)
		{
			animator = a;
		}
		public void Tick (float d, float h, float v) 
		{
			animator.SetFloat ("Forward", v);
		}
	}
}