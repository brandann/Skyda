using UnityEngine;
using System.Collections;

namespace Skyda{
	public class SunBehavior : MonoBehaviour {
	
		private enum DayQuadrant{q1,q2,q3,q4};
		private DayQuadrant CurrentQuadrant;
		
		public float DayLength;
		private float DayUpdates;
		private float UpdateIncrement;
		private float DayTime;
		
		private float MaxIntensity = .5f;
		private float MinIntensity = 0f;
		private float UpdatesPerSecond = 40;
		private float SecondsPerMinute = 60;
		
		private bool pause = false;
		
		ClockBehavior clock;
		
		// Use this for initialization
		void Start () {
			clock = GameObject.Find("Clock").GetComponent<ClockBehavior>();
			DayUpdates = DayLength * SecondsPerMinute * UpdatesPerSecond;
			UpdateIncrement = MaxIntensity/(DayUpdates/4);
			CurrentQuadrant = DayQuadrant.q2;
			DayTime = DayUpdates * .25f;
			print ("DayUpdates: " + DayUpdates);
			print ("UpdateIncrement: " + UpdateIncrement);
			print ("DayTime: " + DayTime);
		}
		
		// Update is called once per frame
		void Update () {
			if(!pause){
				switch(CurrentQuadrant){
				
				//increment sun brighter
				case(DayQuadrant.q1):
					this.light.intensity += UpdateIncrement;
					DayTime++;
					if(this.light.intensity >= MaxIntensity) {
						CurrentQuadrant = DayQuadrant.q2;
						this.light.intensity = MaxIntensity;
					}
					break;
				
				//leave sun bright
				case(DayQuadrant.q2):
					DayTime++;
					if(DayTime >= (DayUpdates/2)) {
						CurrentQuadrant = DayQuadrant.q3;
						this.light.intensity = MaxIntensity;
					}
					break;
					
				//decrement sun dimmer
				case(DayQuadrant.q3):
					this.light.intensity -= UpdateIncrement;
					DayTime++;
					if(this.light.intensity <= MinIntensity) {
						CurrentQuadrant = DayQuadrant.q4;
						this.light.intensity = MinIntensity;
					}
					break;
					
				//leave sun dark
				case(DayQuadrant.q4):
					DayTime++;
					if(DayTime >= (DayUpdates)) {
						CurrentQuadrant = DayQuadrant.q1;
						this.light.intensity = MinIntensity;
						DayTime = 0;;
					}
					break;
				}
				clock.SetClock(DayTime, DayUpdates);
			}
		}
	}
}
