using UnityEngine;
using System.Collections;

namespace Skyda{
	public class CityGenerator{
	
		public CityGenerator(){}
		
		public char[,] getCity(int width, int height){
			char[,] map = new char[width,height];
			Tokens token = new Tokens();
			Vector2 center = new Vector2(width/2,height/2);
			float rad = width/2;
			
			for(int i = 0; i < width; i++){
				for(int j = 0; j < height; j++){
					Vector2 point = new Vector2(i,j);
					Vector2 dist = point - center;
					if(dist.magnitude < rad){
						map[i,j] = token.getPAVER();
					}
					else{
						map[i,j] = token.getGRASS();
					}
				}
			}
			createBuildings(map);
			savemap(map);
			
			return map;
		}
		
		private char[,] createBuildings(char[,] map){
			Tokens token = new Tokens();
			int w = 16;
			int h = 9;
			int m1 = 1;//Random.Range(1,5);
			int m2 = 1;//Random.Range(1,5);
			
			// start with random sized buildings
			for(int i = 0; i < map.GetLength(0); i++){
				for(int j = 0; j < map.GetLength(1); j++){
					
					// check bulding within map bounds
					if((i + (w*m1)) >= map.GetLength(0)){
						break;
					}
					if((j + (h*m2)) >= map.GetLength(1)){
						break;
					}
					
					// check for building corners within city
					if(map[i,j] == token.getPAVER() &&  				// bottom left corner
						   map[i + (w*m1),j] == token.getPAVER() &&  	// botom right corner
						   map[i,j + (h*m2)] == token.getPAVER() &&  	// top left corner
						   map[i + (w*m1),j + (h*m2)] == token.getPAVER())  // top right corner
					{
						map = buildHouse(map, i, j, w*m1, h*m2);
						//reset house
						i+=2;
						j+=2;
						m1 = Random.Range(1,5);
						m2 = Random.Range(1,5);
					}
					
				}
			}
			
			return map;
		}
		
		private char[,] buildHouse(char[,] map, int x, int y, int w, int h){
			Tokens token = new Tokens();
			//char[,] house = new char[w,h];
			
			/*
			//make house
			for(int i = 0; i < w; i++){
				for(int j = 0; j < h; j++){
					house[i,j] = token.getHOUSE();
				}
			}
			
			// make walls
			for(int i = 0; i < w; i++){map[x + i,y] = token.getHOUSEWALL();} 	// south wall
			for(int i = 0; i < w; i++){map[x + i,y + h-2] = token.getHOUSEWALL();} 	// north wall
			for(int i = 0; i < h; i++){map[x + w-2,y + i] = token.getHOUSEWALL();} 	// east wall
			for(int i = 0; i < h; i++){map[x,y + i] = token.getHOUSEWALL();} 	// west wall
			*/
			
			// make house
			for(int i = x; i < x + w; i++){
				for(int j = y; j < y + h; j++){
					map[i,j] = token.getHOUSE();
				}
			}
			
			// make walls
			for(int i = 0; i < w; i++){map[x + i,y] = token.getHOUSEWALL();} 	// south wall
			for(int i = 0; i < w; i++){map[x + i,y + h-1] = token.getHOUSEWALL();} 	// north wall
			for(int i = 0; i < h; i++){map[x + w-1,y + i] = token.getHOUSEWALL();} 	// east wall
			for(int i = 0; i < h; i++){map[x,y + i] = token.getHOUSEWALL();} 	// west wall
			
			// make door
			map[x + w/2,y] = token.getDOOR();
			
			return map;
		}
		
		private void savemap(char[,] map){
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\patio.txt"))
			{
				file.WriteLine("City");
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