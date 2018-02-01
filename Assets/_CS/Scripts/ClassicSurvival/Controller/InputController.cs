using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sistemover.TouchGamePad;

namespace ClassicSurvival
{
	public class InputController : MonoBehaviour 
	{
		[HideInInspector]
		public bool d_x, d_y, d_a, d_b, u_x, u_y, u_a, u_b;
		public bool d_BODY, u_BODY;
		[HideInInspector]
		public Vector2 AxisInput, TouchInput;

		//**************************************************************************************************************

		public void FixedTick()
		{
			FixedGetInput ();
		}

		public void Tick()
		{
			GetInput ();
		}

		void FixedGetInput () 
		{
			//floats
			AxisInput = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"),  CrossPlatformInputManager.GetAxis ("Vertical"));
			TouchInput = new Vector2 (CrossPlatformInputManager.GetAxisRaw("Mouse X"), CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

		}

		void GetInput()
		{
			//bools
			d_x = CrossPlatformInputManager.GetButtonDown ("X");
			u_x = CrossPlatformInputManager.GetButtonUp ("X");

			d_y = CrossPlatformInputManager.GetButtonDown ("Y");
			u_y = CrossPlatformInputManager.GetButtonUp ("Y");

			d_a = CrossPlatformInputManager.GetButtonDown ("A");
			u_a = CrossPlatformInputManager.GetButtonUp ("A");

			d_b = CrossPlatformInputManager.GetButtonDown ("B");
			u_b = CrossPlatformInputManager.GetButtonUp ("B");

			d_BODY = CrossPlatformInputManager.GetButtonDown ("BODY");
			u_BODY = CrossPlatformInputManager.GetButtonUp ("BODY");
		}
	}
}