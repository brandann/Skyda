using UnityEngine;
using System.Collections;

namespace Skyda{
	public class MapGenerator{
	
		private const int WIDTH = 1000;
		private const int HEIGHT = 1000;
		private char[,] map;
		private Tokens token;
		
		/**
			default constructor
		*/
		public MapGenerator(){
			token = new Tokens();
			map = new char[WIDTH,HEIGHT];
		}
		
		/**
			public map generator function
		*/
		public char[,] getMap(){
			makeGrass();	// Debug.Log ("makeGrass");
			makePonds();	// Debug.Log ("makePonds");
			makeSand();		// Debug.Log ("makeSand");
			makeTrees(); 	// Debug.Log ("makeTrees");
			makeBuildings();// Debug.Log ("makeBuildings");
			makeCity(); 	// Debug.Log ("makeCity");
			saveMap(); 		// Debug.Log ("saveMap");
			return map;
		}
		
		/**
			load grass tiles into every position
		*/
		private void makeGrass(){
			for(int i = 0; i < WIDTH; i++){
				for(int j = 0; j < WIDTH; j++){
					map[i,j] = token.getGRASS();
				}
			}
		}
		
		/**
			place ponds on map
		*/
		private void makePonds(){
		
			// randomly choose number of ponds for map to have
			int numberOfPonds = (int) Random.Range(Mathf.Sqrt(WIDTH),WIDTH);
			
			// initilize pond generator
			PondGenerator pond = new PondGenerator();
			
			// make and place ponds
			for(int i = 0; i < numberOfPonds; i++){
				
				// make pond
				char[,] temp = pond.getPond((int) Random.Range(5,WIDTH*.1f),(int) Random.Range(5,WIDTH*.1f));
				
				// get pond point on map
				int x = Random.Range(0,WIDTH);
				int y = Random.Range(0,HEIGHT);
				
				// find starting position of the pond relative to the map
				int startx = x - (temp.GetLength(0) / 2);
				int starty = y - (temp.GetLength(1) / 2);
				
				// put pond on map
				for(int j = 0; j < temp.GetLength(0); j++){
					for(int k = 0; k < temp.GetLength(1); k++){
						int px = startx + j;
						int py = starty + k;
						
						// make sure points are in map
						if(px > 0 && px < WIDTH){
							if(py > 0 && py < HEIGHT){
								//only put water
								if(temp[j,k] == token.getWATER()){
									map[px,py] = token.getWATER();
								}
							}
						}
						
					}
				}
				
			}
		}
		
		/**
			place sand on map where water meets grass
		*/
		private void makeSand(){
			for(int i = 0; i < WIDTH; i++){
				for(int j = 0; j < HEIGHT; j++){
					if (map[i,j] == token.getWATER()){
						for (int k = i-1; k < i+2; k++){
							if(k > 0 && k < WIDTH){
								for (int l = j-1; l < j+2; l++){
									if(l > 0 && l < HEIGHT){
										if(map[k,l] == token.getGRASS()){
											map[k,l] = token.getSAND();
										}
									}
								}
							}
						}
					}
				}
			}
		}
		
		/**
			randomly place trees on grass
		*/
		private void makeTrees(){
		
			// randomly choose qty of trees
			int numoftrees = (int) Random.Range(WIDTH, (int) ((WIDTH * HEIGHT)*.1f) );
			
			// place trees
			for(int i = 0; i < numoftrees; i++){
			
				// randomly pick location
				int locX = Random.Range(0,WIDTH-1);
				int locY = Random.Range(0,HEIGHT-1);
				
				// try to place tree
				if(map[locX,locY] == token.getGRASS()){
					map[locX,locY] = token.getTREE();
				}
				else {
					i--;
				}
			}
		}
		
		
		private void makeBuildings(){
			int bwidth = 10;
			int bheight = 5;
			BuildingGenerator bg = new BuildingGenerator();
			
			char[,] house = bg.getBuilding(bwidth,bheight);
			
			for(int i = 0; i < bwidth; i++){
				for(int j = 0; j < bheight; j++){
					map[i,j] = house[i,j];
				}
			}
		}
		
		private void makeCity(){
			//CityGenerator c = new CityGenerator();
			//char[,] city = c.getCity(150,150);
		}
		
		private void saveMap(){
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"./pond.txt"))
			{
				for(int i = map.GetLength(0) - 1; i >= 0; i--){
					for(int j = 0; j < map.GetLength(1); j++){ //for(int j = map.GetLength(1) - 1; j >= 0; j--){
						file.Write(map[j,i]);
					}
					file.WriteLine("");
				}
			}
		}
			
	}
}
