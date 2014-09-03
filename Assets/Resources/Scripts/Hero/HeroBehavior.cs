using UnityEngine;
using System.Collections;

namespace Skyda{
	public class HeroBehavior : MonoBehaviour {
	
		private float Speed = 3f;
		Vector3 dir = Vector3.zero;
		
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			
			// move hero
			float H = Input.GetAxis ("Horizontal");
			float V = Input.GetAxis ("Vertical");
			
			bool pressed = false; 
			if(Input.GetKey("up"))
				pressed = true;
			else if(Input.GetKey("down"))
				pressed = true;
			else if(Input.GetKey("left"))
				pressed = true;
			else if(Input.GetKey("right"))
				pressed = true;
			
			Vector3 movement = new Vector3(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
			
			//if(movement.magnitude >= .1f){
			if(pressed){
				movement *= Speed;
				this.transform.up = movement.normalized;
				this.transform.position += this.transform.up * (Speed * Time.smoothDeltaTime);
				if(H == 0 && V < 0){
					this.transform.Rotate(new Vector3(0,180,0));
				}
			}		
		}
		
		// hero collider
		void OnTriggerEnter2D(Collider2D other){
//			if(other.gameObject.tag == "water"){
//				this.transform.Translate(new Vector3(-dir.x * Speed, -dir.y * Speed, 0));
//			}
			print ("Collided With Hero");
			if(other.gameObject.tag == "bush"){
				print ("\tCollided With Hero: Bush");
				Destroy(other.gameObject);
			}
		}
	}
}