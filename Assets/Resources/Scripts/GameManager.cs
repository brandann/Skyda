using UnityEngine;
using System.Collections;

namespace Skyda{
	public class GameManager : MonoBehaviour {
		
		private enum GameState{Play, Paused, Menu};

		GameState CurrentGameState;
		
		HeroBehavior hero;
		
		// Use this for initialization
		void Start () {
			DontDestroyOnLoad(this);
			hero = GameObject.Find("Hero").GetComponent<HeroBehavior>();
			CurrentGameState = GameState.Play;
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		
		public bool isPlayGameState() {return CurrentGameState == GameState.Play;}
		public bool isPausedGameState() {return CurrentGameState == GameState.Paused;}
		public bool isMenuGameState() {return CurrentGameState == GameState.Menu;}
		
		public void Menu(){
			hero.gameObject.SetActive(false);
			Application.LoadLevel("Menu");
		}
		
		public void Pause(){
			hero.gameObject.SetActive(false);
			Application.LoadLevel("Pause");
		}
		
		public void Play(){
			hero.gameObject.SetActive(true);
			Application.LoadLevel("Skyda");
            CurrentGameState = GameState.Play;
		}
		
	}
}