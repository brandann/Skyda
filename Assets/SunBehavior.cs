using UnityEngine;
using System.Collections;

namespace Skyda{
	public class SunBehavior : MonoBehaviour {
	
		private enum DayQuadrant{q1,q2,q3,q4};
		private DayQuadrant CurrentQuadrant;
		
		private float DayLength = 1f;
		private float DayUpdates;
		private float UpdateIncrement;
		private float DayTime;
		
		private float MaxIntensity = .5f;
		private float MinIntensity = 0f;
		private float UpdatesPerSecond = 40;
		private float SecondsPerMinute = 60;
		
		private bool pause = false;
		
		private FlashlightBehavior FlashLight;
		
		ClockBehavior clock;
		
		// Use this for initialization
		void Start () {
			FlashLight = GameObject.Find("Flashlight").GetComponent<FlashlightBehavior>();
			clock = GameObject.Find("Clock").GetComponent<ClockBehavior>();
			DayUpdates = DayLength * SecondsPerMinute * UpdatesPerSecond;
			UpdateIncrement = MaxIntensity/(DayUpdates/4);
			CurrentQuadrant = DayQuadrant.q2;
			DayTime = DayUpdates/4;
			print ("DayUpdates: " + DayUpdates);
			print ("UpdateIncrement: " + UpdateIncrement);
			print ("DayTime: " + DayTime);
		}
		
		// Update is called once per frame
		void Update () {
			if(!pause){
				switch(CurrentQuadrant){
					case(DayQuadrant.q1):
						this.light.intensity += UpdateIncrement;
						DayTime++;
						if(this.light.intensity >= MaxIntensity) {
							CurrentQuadrant = DayQuadrant.q2;
							this.light.intensity = MaxIntensity;
						}
						if(this.light.intensity >= (MaxIntensity/2)){
							FlashLight.TurnOff();
						}
						break;
					case(DayQuadrant.q2):
						this.light.intensity -= UpdateIncrement;
						DayTime++;
						if(this.light.intensity <= MinIntensity) {
							CurrentQuadrant = DayQuadrant.q3;
							this.light.intensity = MinIntensity;
							this.light.enabled = false;
						}
					if(this.light.intensity <= (MaxIntensity/2)){
						FlashLight.TurnOn();
					}
						break;
					case(DayQuadrant.q3):
						this.light.intensity += UpdateIncrement;
						DayTime++;
						if(this.light.intensity >= MaxIntensity) {
							CurrentQuadrant = DayQuadrant.q4;
							this.light.intensity = MaxIntensity;
						}
						break;
					case(DayQuadrant.q4):
					
						// testing
						CurrentQuadrant = DayQuadrant.q1;
						this.light.intensity = MinIntensity;
						this.light.enabled = true;
						DayTime = 0;;
						break;
						//end testing
						
						this.light.intensity -= UpdateIncrement;
						DayTime++;
						if(this.light.intensity <= MinIntensity) {
							CurrentQuadrant = DayQuadrant.q1;
							this.light.intensity = MinIntensity;
							this.light.enabled = true;
							DayTime = 0;;
						}
						break;
				}
				clock.SetClock(DayTime, DayUpdates);
			}
		}
	}
}
