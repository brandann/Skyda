using UnityEngine;
using System.Collections;

namespace Skyda{
	public class HeroBehavior : MonoBehaviour {
	
		private float Speed = .05f;
		Vector3 dir = Vector3.zero;
		
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			
			// move hero
			float H = Input.GetAxis ("Horizontal");
			float V = Input.GetAxis ("Vertical");
			this.transform.Translate(new Vector3(H * Speed, V * Speed, 0));
		}
		
		// hero collider
		void OnTriggerEnter2D(Collider2D other){
//			if(other.gameObject.tag == "water"){
//				this.transform.Translate(new Vector3(-dir.x * Speed, -dir.y * Speed, 0));
//			}
		}
	}
}