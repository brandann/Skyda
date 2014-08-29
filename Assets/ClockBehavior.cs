using UnityEngine;
using System.Collections;

namespace Skyda{
	public class ClockBehavior : MonoBehaviour {
	
		Vector3 origin1 = new Vector3(0,.125f,0);
		Vector3 origin2 = new Vector3(0,-.125f,0);
		
		// Use this for initialization
		void Start () {
			origin1 = new Vector3(this.transform.position.x, this.transform.position.y + .125f, 0);
			origin2 = new Vector3(this.transform.position.x, this.transform.position.y - .125f, 0);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		
		public void SetClock(float DayTime, float DayUpdates){
		
			float theta = Mathf.Deg2Rad * ((DayTime / DayUpdates) * 360);
			theta *= -1;
			Vector3 point = new Vector3(
				this.transform.position.x + (.5f * Mathf.Cos (theta)), 
				this.transform.position.y + (.5f * Mathf.Sin (theta)), 
				0);
			LineRenderer line = this.gameObject.GetComponent<LineRenderer>();
			line.SetPosition(0,origin1);
			line.SetPosition(1,point);
			line.SetPosition(2,origin2);			
			line.SetWidth(.01f,.01f);
		}
	}
}