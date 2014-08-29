using UnityEngine;
using System.Collections;

namespace Skyda{
	public class FlashlightBehavior : MonoBehaviour {
	
		// Use this for initialization
		void Start () {
			TurnOff();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		
		public void TurnOn(){
			this.light.enabled = true;
		}
		
		public void TurnOff(){
			this.light.enabled = false;
		}
	}
}