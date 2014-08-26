using UnityEngine;
using System.Collections;

public class PondGenerator{

	private char [,] map;
	private int width;
	private int height;
	private const int MIN = 5;
	private const int MAX = 50;
	private int COUNTMAX;
	private int count;
	
	char token;
	
	public PondGenerator(){
		Tokens Token = new Tokens();
		token = Token.getWATER();
	}
	
	/**
		get the default pond size char array
	**/
	public char[,] getPond(){
		return getPond(MAX, MAX);
	}
	
	/**
		get a costimized pond size char array
	**/
	public char[,] getPond(int MaxWidth, int MaxHeight){
		
		//check for constraints
		if(MaxWidth < MIN){
			MaxWidth = MIN + 1;
		}
		if(MaxHeight < MIN){
			MaxHeight = MIN + 1;
		}
		
		// build char array
		width = MaxWidth;
		height = MaxHeight;
		map = new char[width,height];
		COUNTMAX = (int) ((width * height) * .5f);
		
		count = 0;
		int trys = (int) Mathf.Sqrt(COUNTMAX);
		while(count < COUNTMAX && trys > 0){
			BuildPond(width/2, height/2);
			trys--;
		}
		return map;
	}
	
	private void BuildPond(int x, int y){
		if(y >= height || y < 0){
			return;
		}
		
		if(x >= width || x <= 0){
			return;
		}
		
		if(map[x,y] != token){
			map[x,y] = token;
			count++;
		}
		
		int u = Random.Range(0,4);
		
		switch(u){
		case(0):
			BuildPond(x,y+1);
			break;
		case(1):
			BuildPond(x+1,y);
			break;
		case(2):
			BuildPond(x,y-1);
			break;
		case(3):
			BuildPond(x-1,y);
			break;
		}
	}
}
