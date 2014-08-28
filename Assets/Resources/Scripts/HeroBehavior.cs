using UnityEngine;
using System.Collections;

namespace Skyda{
	public class HeroBehavior : MonoBehaviour {
	
		private float Speed = .05f;
		
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
			
		}
	}
}