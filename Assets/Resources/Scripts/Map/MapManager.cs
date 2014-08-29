using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Skyda{
	public class MapManager : MonoBehaviour {
		
		#region MapMembers
		private char[,] map;
		ArrayList mapobjects;
		ArrayList desobjects;
		private const int XDIR = 0;
		private const int YDIR = 1;
		#endregion
		
		#region PositionMemebers
		private int xpos = 0;
		private int ypos = 0;
		#endregion
		
		#region SlideMembers
		Vector3 slidedir = Vector3.zero;
		int slidecount = 0;
		float slidespeed = 1f;
		#endregion
		
		#region ScreenSizeMemebers
		private int SCREEN_HEIGHT = 10;
		private int SCREEN_WIDTH = 17;
		#endregion
		
		#region GameObjects
		private CameraBehavior cam;
		private HeroBehavior hero;
		private GameObject waterObject;
		private GameObject grassObject;
		private GameObject sandObject;
		private GameObject treeObject;
		private GameObject rockObject;
		#endregion
		
		#region GameMethods
		
		// Use this for initialization
		void Start () {
		
			// find and load objects
			cam = GameObject.Find("Main Camera").GetComponent<CameraBehavior>();
			hero = GameObject.Find("triangle").GetComponent<HeroBehavior>();
			waterObject = Resources.Load("Prefabs/watertile") as GameObject;
			grassObject = Resources.Load("Prefabs/grasstile") as GameObject;
			sandObject = Resources.Load("Prefabs/sandtile") as GameObject;
			treeObject = Resources.Load("Prefabs/treetile") as GameObject;
			rockObject = Resources.Load ("Prefabs/rocktile") as GameObject;
			
			// compute camera aspect ratio and size
			if (cam != null){
				float camHeight = cam.camera.orthographicSize;
				float asp = cam.camera.aspect;
				SCREEN_HEIGHT = (int) ((camHeight * 2) + .5f);
				SCREEN_WIDTH = (int) ((SCREEN_HEIGHT * asp) + .5f);
				SCREEN_HEIGHT++;
				SCREEN_WIDTH++;
			}
			
			// initilize Arraylists
			mapobjects = new ArrayList();
			desobjects = new ArrayList();
			
			// generate Map
			generateMap();
			
			// set current x and y positions
			// load first map screen
			xpos = map.GetLength(XDIR) / 2;
			ypos = map.GetLength(YDIR) / 2;
			
			// testing
//			xpos = 0; // start at right side
//			ypos = 0; // start at bottom
//			xpos = map.GetLength(XDIR) - SCREEN_WIDTH; // start at left side
//			ypos = map.GetLength(YDIR) - SCREEN_HEIGHT; // start at top side
			
			LoadMapScreen(xpos * SCREEN_WIDTH, ypos * SCREEN_HEIGHT, Vector3.zero);
			print ("Starting Location: (" + xpos + ", " + ypos + ", 0)");
		}
		
		// Update is called once per frame
		void Update () {
			
			// create a sliding animation for tiles
			if(slidecount > 0){
				// move new tiles
				foreach ( GameObject obj in mapobjects){
					obj.transform.position -= (slidedir * (float)((1/slidespeed)));
				}
				// move old tiles
				foreach ( GameObject obj in desobjects){
					obj.transform.position -= (slidedir * (float)((1/slidespeed)));
				}
				// decrement counter
				slidecount--;
			}
			else {
				// destroy the old tiles when finished sliding
				destroytiles(desobjects);
			}
		}
		
		#endregion
		
		// instantiate MapGenerator and create new map
		public void generateMap(){
			MapGenerator m = new MapGenerator();
			map = m.getMap();	
		}
		
		#region MapTraversal
		// destroy all tiles in a list
		private void destroytiles(ArrayList a){
			foreach ( GameObject obj in a){
				Destroy(obj.gameObject);
			}
			a.Clear();
		}
		
		/**
			move the screen tiles by a dx,dy direction
			does not account for map bounds right now!
		*/
		public void MoveMap(int dx, int dy){
			
			int txpos = xpos + dx * (SCREEN_WIDTH-1);
			int typos = ypos + dy * (SCREEN_HEIGHT-1);
			
			// check bounds before doing anything!
			bool outofbounds = false;
			if(txpos < 0) outofbounds = true;
			else if(txpos >= map.GetLength(XDIR) - SCREEN_WIDTH) outofbounds = true;
			else if(typos < 0) outofbounds = true;
			else if(typos >= map.GetLength(YDIR) - SCREEN_HEIGHT) outofbounds = true;
			
			if(outofbounds){
				return;
			}
			
			// set new current location
			ypos += dy * (SCREEN_HEIGHT-1);
			xpos += dx * (SCREEN_WIDTH-1);
			
			// move current game tiles to the temperaty list
			foreach ( GameObject obj in mapobjects){
				desobjects.Add(obj.gameObject);
			}			
			
			// clear current tile list to populate new tiles
			mapobjects.Clear();
			
			// create direction vector for slideing the map
			slidedir = new Vector3(dx,dy,0);
			
			// determine the distance tiles must move, dependent on direction
			if(dx != 0) slidecount = (int) (SCREEN_WIDTH * slidespeed);
			else if (dy != 0) slidecount = (int) (SCREEN_HEIGHT * slidespeed);
			
			// load the new map tiles into mapobjects
			LoadMapScreen(xpos, ypos, slidedir);
			
			// make a temperary vector to move the current tiles into their initial sliding location
			Vector3 dir = new Vector3(SCREEN_WIDTH, SCREEN_HEIGHT, 0);
			dir.x *= slidedir.x;
			dir.y *= slidedir.y;
			
			// move the new tiles to their initial sliding location
			foreach ( GameObject obj in mapobjects){
				obj.transform.position += dir;
			}
			
			// move hero
			hero.transform.position += new Vector3(
				(SCREEN_WIDTH - 1.5f) * -slidedir.x , //x direction
				(SCREEN_HEIGHT - 1.5f) * -slidedir.y, //y direction
				0);									  //z direction
				
			print ("Current Location: (" + xpos + ", " + ypos + ", 0)");
		}
		
		/**
			loads the map tiles based on the map encoding
		*/
		private void LoadMapScreen(int x, int y, Vector3 dir){
			
			// loop through the area of the map to be loaded
			// x direction
			for(int i = 0; i < SCREEN_WIDTH; i++){
			
				// y direction
				for(int j = 0; j < SCREEN_HEIGHT; j++){
				
					// get char code for map tile
					char tile = map[xpos+i,ypos+j];
					
					// put tile at (i,j) + (1,1)
					int lx = i+1;
					int ly = j+1;
					
					// load tiles
					switch(tile){
						case('X'):
							loadWATER(lx,ly);
							break;
						case('_'):
							loadGRASS(lx,ly);
							break;
						case('0'):
							loadTREE(lx,ly);
							break;
						case('*'):
							loadSAND(lx,ly);
							break;
						/*case('='):
							loadHOUSE(lx,ly);
							break;
						case('+'):
							loadDOOR(lx,ly);
							break;
						case('g'):
							loadPAVER(lx,ly);
							break;
						case('h'):
							loadHOUSEWALL(lx,ly);
							break;*/
						case('i'):
							loadROCK(lx,ly);
							break;
						default:
							loadGRASS(lx,ly);
							break;
					}
				}
			}
		}
		#endregion
	
		#region TileObjects
		/*
			public const char WATER = 'X';	
			public const char GRASS= '_';	
			public const char TREE= '0';	
			public const char SAND= '*';	
			public const char HOUSE = '=';	
			public const char DOOR = '+';	
			public const char PAVER = 'g';	
			public const char HOUSEWALL = 'h';	
		*/
		
		// instantiate grass object
		void loadGRASS(int x, int y){
			GameObject e = Instantiate(grassObject) as GameObject;
			GrassTile spawnedParticle = e.GetComponent<GrassTile>();
			if(spawnedParticle != null) {
				e.transform.position = new Vector3(x,y,0);
				mapobjects.Add(e);
			}
			else {
				print ("error creating grass");
			}
		}
		
		// instantiate water object
		void loadWATER(int x, int y){
			GameObject e = Instantiate(waterObject) as GameObject;
			WaterTile spawnedParticle = e.GetComponent<WaterTile>();
			if(spawnedParticle != null) {
				e.transform.position = new Vector3(x,y,0);
				mapobjects.Add(e);
			}
			else {
				print ("error creating water");
			}
		}
		
		// instantiate sand object
		void loadSAND(int x, int y){
			GameObject e = Instantiate(sandObject) as GameObject;
			SandTile spawnedParticle = e.GetComponent<SandTile>();
			if(spawnedParticle != null) {
				e.transform.position = new Vector3(x,y,0);
				mapobjects.Add(e);
			}
			else {
				print ("error creating sand");
			}
		}
		
		// instantiate tree object
		void loadTREE(int x, int y){
			GameObject e = Instantiate(treeObject) as GameObject;
			TreeTile spawnedParticle = e.GetComponent<TreeTile>();
			if(spawnedParticle != null) {
				e.transform.position = new Vector3(x,y,0);
				mapobjects.Add(e);
			}
			else {
				print ("error creating tree");
			}
		}
		
		// instantiate house object
		void loadHOUSE(int x, int y){
		
		}
		
		// instantiate door object
		void loadDOOR(int x, int y){
		
		}
		
		// instantiate paver object
		void loadPAVER(int x, int y){
		
		}
		
		// instantiate housewall object
		void loadHOUSEWALL(int x, int y){
		
		}
		
		// instantiate rock object
		void loadROCK(int x, int y){
			GameObject e = Instantiate(rockObject) as GameObject;
			RockTile spawnedParticle = e.GetComponent<RockTile>();
			if(spawnedParticle != null) {
				e.transform.position = new Vector3(x,y,0);
				mapobjects.Add(e);
			}
			else {
				print ("error creating rock");
			}
		}
		#endregion
	}
}
