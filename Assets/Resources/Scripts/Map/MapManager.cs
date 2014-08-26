using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MapManager : MonoBehaviour {
	
	private char[,] map;
	ArrayList mapobjects;
	private int xpos = 0;
	private int ypos = 0;
	
	private int SCREEN_HEIGHT = 10;
	private int SCREEN_WIDTH = 17;
	
	private GameObject waterObject;
	private GameObject grassObject;
	private GameObject sandObject;
	private GameObject treeObject;
	
	// Use this for initialization
	void Start () {
		waterObject = Resources.Load("Prefabs/watertile") as GameObject;
		grassObject = Resources.Load("Prefabs/grasstile") as GameObject;
		sandObject = Resources.Load("Prefabs/sandtile") as GameObject;
		treeObject = Resources.Load("Prefabs/treetile") as GameObject;
		
		mapobjects = new ArrayList();
		
		generateMap();
		
		
		MoveMap((map.GetLength(0)/17)/2,(map.GetLength(1)/10)/2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void generateMap(){
		MapGenerator m = new MapGenerator();
		map = m.getMap();	
		//print(map);
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream ms = new MemoryStream();
		byte[] Array;
		bf.Serialize(ms, map);
		Array = ms.ToArray();
		print ("Length: " + Array.Length);
		
	}
	
	public void MoveMap(int dx, int dy){
		ypos += dy * (SCREEN_HEIGHT-1);
		xpos += dx * (SCREEN_WIDTH-1);
		
		foreach ( GameObject obj in mapobjects){
			Destroy(obj.gameObject);
		}
		
		LoadMapScreen(xpos, ypos);
	}
	
	private void LoadMapScreen(int x, int y){
		//Tokens t = new Tokens();
		for(int i = 0; i < SCREEN_WIDTH; i++){
			for(int j = 0; j < SCREEN_HEIGHT; j++){
				char tile = map[xpos+i,ypos+j];
				//print ("Tile: " + i + ", " + j);
				//print ("Map: " + xpos+i + ", " + ypos+j);
				int lx = i+1;
				int ly = j+1;
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
				default:
					loadGRASS(lx,ly);
					break;
				}
			}
		}
	}

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
	
	case(t.getWATER()):
		loadWATER(i,j);
		break;
	case(t.getGRASS()):
		loadGRASS(i,j);
		break;
	case(t.getTREE()):
		loadTREE(i,j);
		break;
	case(t.getSAND()):
		loadSAND(i,j);
		break;
	case(t.getHOUSE()):
		loadHOUSE(i,j);
		break;
	case(t.getDOOR()):
		loadDOOR(i,j);
		break;
	case(t.getPAVER()):
		loadPAVER(i,j);
		break;
	case(t.getHOUSEWALL()):
		loadHOUSEWALL(i,j);
		break;
		
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
	
	void loadHOUSE(int x, int y){
	
	}
	
	void loadDOOR(int x, int y){
	
	}
	
	void loadPAVER(int x, int y){
	
	}
	
	void loadHOUSEWALL(int x, int y){
	
	}
	

#endregion
}
