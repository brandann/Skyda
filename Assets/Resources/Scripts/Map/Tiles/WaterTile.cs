using UnityEngine;
using System.Collections;

public class WaterTile : MonoBehaviour {

	public Sprite[] MapTiles;
	
	// Use this for initialization
	void Start () {
		
		SpriteRenderer MyRenderer;
		
		// Load SpriteRenderer
		MyRenderer = this.GetComponent<SpriteRenderer>();
		
		// set Sprite to Random Tile
		int RandomAssignment = Random.Range(0,6);
		switch(RandomAssignment){
		case(0):
			MyRenderer.sprite = MapTiles[1];
			break;
		case(1):
			MyRenderer.sprite = MapTiles[2];
			break;
		case(2):
			MyRenderer.sprite = MapTiles[3];
			break;
		default:
			MyRenderer.sprite = MapTiles[0];
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
