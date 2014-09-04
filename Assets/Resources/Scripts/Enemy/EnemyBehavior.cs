using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public Sprite[] EnemySheet;
	private SpriteRenderer MyRenderer;
	int SpritePosition = 0;
	int x = 0;
	bool change = false;
	
	// Use this for initialization
	void Start () {
		MyRenderer = this.GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(x++ >= 20){
			change = true;
			x = 0;
		}
		
		if(change){
			if(SpritePosition >= EnemySheet.GetLength(0)){
				SpritePosition = 0;
			}
			MyRenderer.sprite = EnemySheet[SpritePosition];
			SpritePosition++;
			change = false;
		}
		
	}
}
