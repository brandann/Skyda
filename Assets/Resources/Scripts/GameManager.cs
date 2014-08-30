using UnityEngine;
using System.Collections;

namespace Skyda{
	public class GameManager : MonoBehaviour {
		
		private enum GameState{Play, Paused, Menu};
		GameState CurrentGameState;
		
		// Use this for initialization
		void Start () {
			CurrentGameState = GameState.Play;	
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		
		public bool isPlayGameState() {return CurrentGameState == GameState.Play;}
		public bool isPausedGameState() {return CurrentGameState == GameState.Paused;}
		public bool isMenuGameState() {return CurrentGameState == GameState.Menu;}
		
	}
}