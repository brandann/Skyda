using UnityEngine;
using System.Collections;

public class TreeTile : MonoBehaviour {

	public Sprite rock;
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer>();
		int r = Random.Range(0,3);
		switch(r){
			case(0):
				sr.sprite = rock;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
