using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {

	public static int MINIMUM_SIZE = 4;
	public static int MAXIMUM_SIZE = 8;

	public Vector2 position;
	public int width;
	public int height;

	public int[,] room;

	public Room(Vector2 position) {
		this.position = position;
		this.width = Random.Range (MINIMUM_SIZE, MAXIMUM_SIZE);
		this.height = Random.Range (MINIMUM_SIZE, MAXIMUM_SIZE);
		FillRoom();
	}

	private void FillRoom () {
		room = new int[width,height];
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				if(x == 0 || x == width-1 || y == 0 || y == height-1) {
					room[x,y] = GenerateLevel.stoneValue;
				}
				else {
					room[x,y] = GenerateLevel.floorValue;
				}
			}
		}
	}
}
