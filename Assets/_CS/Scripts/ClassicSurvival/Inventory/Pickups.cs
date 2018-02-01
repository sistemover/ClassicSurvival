using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassicSurvival
{
	public class Pickups : MonoBehaviour 
	{
		public List<Item> items = new List<Item>();
		public List<int> amounts = new List<int>();
		public List<GameObject> visual = new List<GameObject>();
	}
}