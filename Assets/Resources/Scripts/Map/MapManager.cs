using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MapManager : MonoBehaviour {
	
	private char[,] map;
	ArrayList mapobjects;
	ArrayList desobjects;
	
	private int xpos = 0;
	private int ypos = 0;
	
	float slide = 0;
	Vector3 slidedir = Vector3.zero;
	float slideFrameRate = 60;
	
	private const int XDIR = 0;
	private const int YDIR = 1;
	
	private int SCREEN_HEIGHT = 10;
	private int SCREEN_WIDTH = 17;
	
	private Camera cam;
	private HeroBehavior hero;
	private GameObject waterObject;
	private GameObject grassObject;
	private GameObject sandObject;
	private GameObject treeObject;
	
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		if (cam != null){
			float camHeight = cam.camera.orthographicSize;
			float asp = cam.camera.aspect;
			
			SCREEN_HEIGHT = (int) ((camHeight * 2) + .5f);
			SCREEN_WIDTH = (int) ((SCREEN_HEIGHT * asp) + .5f);
			SCREEN_HEIGHT++;
			SCREEN_WIDTH++;
		}
		
		hero = GameObject.Find("triangle").GetComponent<HeroBehavior>();
		waterObject = Resources.Load("Prefabs/watertile") as GameObject;
		grassObject = Resources.Load("Prefabs/grasstile") as GameObject;
		sandObject = Resources.Load("Prefabs/sandtile") as GameObject;
		treeObject = Resources.Load("Prefabs/treetile") as GameObject;
		
		mapobjects = new ArrayList();
		desobjects = new ArrayList();
		generateMap();
		
		//MoveMap((map.GetLength(XDIR)/SCREEN_WIDTH)/2,(map.GetLength(YDIR)/SCREEN_HEIGHT)/2);
		LoadMapScreen(map.GetLength(XDIR)/2 , map.GetLength(YDIR)/2 , Vector3.zero);
	}
	float slidespeed = 0;
	// Update is called once per frame
	void Update () {
		/*
		if(slide > 0){
			hero.transform.position += new Vector3(-1,0,0);
			slide--;
		}
		*/
		/*
		if(slide >= 0){
			Vector3 s = new Vector3(slidedir.x*-.1f, slidedir.y*-.1f,0);
			
			foreach ( GameObject obj in mapobjects){
				obj.transform.Translate(s);
			}
			foreach ( GameObject obj in desobjects){
				obj.transform.Translate(s);
			}
			hero.transform.Translate((s));
			slide -= .1f;
		}
		*/
	}
	
	public void generateMap(){
		MapGenerator m = new MapGenerator();
		map = m.getMap();	
	}
	
	private void destroytiles(ArrayList a){
		foreach ( GameObject obj in a){
			Destroy(obj.gameObject);
		}
	}
	
	
	public void MoveMap(int dx, int dy){
		ypos += dy * (SCREEN_HEIGHT-1);
		xpos += dx * (SCREEN_WIDTH-1);
		
		Vector3 dir = new Vector3(dx,dy,0);
		slidedir = dir;
		//Vector3 dir = new Vector3(Mathf.Sign(dx),Mathf.Sign(dy),0);
//		char dir = 'a';
//		if(dx<0) dir = 'l';
//		else if(dx>0) dir = 'r';
//		else if(dy<0) dir = 'd';
//		else if(dy>0) dir = 'u';
		foreach ( GameObject obj in mapobjects){
			desobjects.Add(obj);
		}
		
		if(dx != 0) {
			slide = SCREEN_WIDTH - 2;
		}
		else if (dy != 0) {
			slide = SCREEN_HEIGHT - 2;
		}
		
		mapobjects.Clear();
		
		// remove this later
		// destroytiles(desobjects);
		
		LoadMapScreen(xpos, ypos, dir);
	}
	
	private void LoadMapScreen(int x, int y, Vector3 dir){
		//Tokens t = new Tokens();
		for(int i = 0; i < SCREEN_WIDTH; i++){
			for(int j = 0; j < SCREEN_HEIGHT; j++){
				char tile = map[xpos+i,ypos+j];
				//print ("Tile: " + i + ", " + j);
				//print ("Map: " + xpos+i + ", " + ypos+j);
				int lx = i+1;
				int ly = j+1;
				
				//lx += (int) (dir.x * SCREEN_WIDTH);
				//ly += (int) (dir.y * SCREEN_HEIGHT);
				
//				if(dir == 'l') lx -= SCREEN_WIDTH;
//				else if(dir == 'r') lx += SCREEN_WIDTH;
//				else if(dir == 'd') ly -= SCREEN_HEIGHT;
//				else if(dir == 'u') ly += SCREEN_HEIGHT;
				
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
