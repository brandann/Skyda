using UnityEngine;
using System.Collections;

public class GrassTile : MonoBehaviour {

	public Sprite grass1;
	public Sprite grass2;
	public Sprite grass3;
	public Sprite grass4;
	
	private SpriteRenderer sr;
	
	// Use this for initialization
	void Start () {
	
		sr = this.GetComponent<SpriteRenderer>();
		int r = Random.Range(0,5);
		switch (r){
			case(0):
				sr.sprite = grass1;
				break;
			case(1):
				sr.sprite = grass2;
				break;
			case(2):
				sr.sprite = grass3;
				break;
			case(3):
				sr.sprite = grass4;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
