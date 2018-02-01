using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sistemover.TouchGamePad;

namespace ClassicSurvival
{
	public class InputTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{	
		[HideInInspector]
		public bool d_x, d_y, d_a, d_b, u_x, u_y, u_a, u_b;
		[HideInInspector]
		public Vector2 AxisInput, TouchInput;

		//SCRIPTABLE DATA
		public TestStats ts;

		//**************************************************************************************************************

		void FixedUpdate()
		{
			FixedGetInput ();
		}

		void Update()
		{
			GetInput ();
			PressA ();
		}

		void FixedGetInput () 
		{
			//Floats
			AxisInput = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"),  CrossPlatformInputManager.GetAxis ("Vertical"));
			TouchInput = new Vector2 (CrossPlatformInputManager.GetAxisRaw("Mouse X"), CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

		}

		void GetInput()
		{
			//Bools
			d_x = CrossPlatformInputManager.GetButtonDown ("X");
			u_x = CrossPlatformInputManager.GetButtonUp ("X");

			d_y = CrossPlatformInputManager.GetButtonDown ("Y");
			u_y = CrossPlatformInputManager.GetButtonUp ("Y");

			d_a = CrossPlatformInputManager.GetButtonDown ("A");
			u_a = CrossPlatformInputManager.GetButtonUp ("A");

			d_b = CrossPlatformInputManager.GetButtonDown ("B");
			u_b = CrossPlatformInputManager.GetButtonUp ("B");
		}

		void PressA()
		{
			if (d_a)
				ts.i++;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{

		}

		public void OnPointerExit(PointerEventData eventData)
		{

		}
	}
}