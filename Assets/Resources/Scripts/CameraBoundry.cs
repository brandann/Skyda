using UnityEngine;
using System.Collections;

public class CameraBoundry : MonoBehaviour {

	MapManager map;
	HeroBehavior hero;
	
	// Use this for initialization
	void Start () {
		map = GameObject.Find ("MapManager").GetComponent<MapManager>();
		hero = GameObject.Find("triangle").GetComponent<HeroBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		print ("Collide");
		//if (tag != other.gameObject.tag && other.gameObject.tag.Contains ("Unit")) {
		string othertag = other.gameObject.tag;
		if(othertag == "hero"){
			if(this.gameObject.tag == "up"){
				map.MoveMap(0,1);
				hero.transform.position += new Vector3(0,-8.5f,0);
			}
			if(this.gameObject.tag == "right"){
				map.MoveMap(1,0);
				hero.transform.position += new Vector3(-15.5f,0,0);
			}
			if(this.gameObject.tag == "down"){
				map.MoveMap(0,-1);
				hero.transform.position += new Vector3(0,8.5f,0);
			}
			if(this.gameObject.tag == "left"){
				map.MoveMap(-1,0);
				hero.transform.position += new Vector3(15.5f,0,0);
			}
		}
	}
}
