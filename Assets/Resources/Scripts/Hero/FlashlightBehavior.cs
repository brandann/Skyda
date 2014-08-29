using UnityEngine;
using System.Collections;

namespace Skyda{
	public class FlashlightBehavior : MonoBehaviour {
	
		float Interval = 0;
		
		// Use this for initialization
		void Start () {
			TurnOff();
		}
		
		// Update is called once per frame
		void Update () {
		
			if (Input.GetAxis ("Jump") > 0f) {	
				if ((Time.realtimeSinceStartup - Interval) > .125f) {
					if(this.light.enabled){
						TurnOff();
					} else {
						TurnOn();
					}
					Interval = Time.realtimeSinceStartup;
				}
			}
		}
		
		public void TurnOn(){
			this.light.enabled = true;
		}
		
		public void TurnOff(){
			this.light.enabled = false;
		}
	}
}