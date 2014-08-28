using UnityEngine;
using System.Collections;

namespace Skyda{
	public class BuildingGenerator {
		
		public BuildingGenerator(){	}
		
		public char[,] getBuilding(int width, int height){
			Tokens token = new Tokens();
			char[,] map = new char[width,height];
			
			for(int i = 0; i < width; i++){
				for(int j = 0; j < height; j++){
					map[i,j] = token.getHOUSE();
				}
			}
			
			map[width/2,0] = token.getDOOR();
			
			return map;
		}
	}
}