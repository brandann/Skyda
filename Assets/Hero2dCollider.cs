using UnityEngine;
using System.Collections;

public class Hero2dCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		print ("Collided With Hero");
		if(other.gameObject.tag == "bush"){
			print ("\tCollided With Hero: Bush");
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
		}
	}
}
