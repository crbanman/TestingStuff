using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLevel : MonoBehaviour {

	public GameObject wall;
	public GameObject door;
	public GameObject floor;
	public GameObject stone;
	
	public static int width = 25;
	public static int height = 25;

	public int maxRooms = 15;
	public int minRooms = 5;

	public static int floorValue = 4;
	public static int doorValue = 0;
	public static int wallValue = 5;
	public static int stoneValue = 25;

	private int[,] mapValues;
	private GameObject[,] mapTiles;
	private List<Room> rooms;

	void Start () {
		FillMap ();
		PlaceRooms ();
		DigPaths ();
		InstantiateMap();
	}

	void FillMap () {
		mapValues = new int[width, height];
		mapTiles = new GameObject[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				mapValues[i,j] = wallValue;
			}
		}
	}

	void InstantiateMap () {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if(mapValues[i,j] == floorValue){
					mapTiles[i,j] = Instantiate(floor, new Vector3(i, j), Quaternion.identity) as GameObject;
					mapTiles[i,j].transform.parent = transform;
				}
				else if (mapValues[i,j] == wallValue) {
					mapTiles[i,j] = Instantiate(wall, new Vector3(i, j), Quaternion.identity) as GameObject;
					mapTiles[i,j].transform.parent = transform;
				}
				else if (mapValues[i,j] == stoneValue) {
					mapTiles[i,j] = Instantiate(stone, new Vector3(i, j), Quaternion.identity) as GameObject;
					mapTiles[i,j].transform.parent = transform;
				}
				else if (mapValues[i,j] == doorValue) {
					mapTiles[i,j] = Instantiate(door, new Vector3(i, j), Quaternion.identity) as GameObject;
					mapTiles[i,j].transform.parent = transform;
				}
			}
		}
	}

	void PlaceRooms () {
		int numRooms = Random.Range(minRooms, maxRooms);
		rooms = new List<Room>();

		for(int attempt = 0; rooms.Count < numRooms && attempt < 100;) {
			int x = Random.Range (0, width - 1);
			int y = Random.Range (0, height - 1);
			Room room = new Room(new Vector2(x, y));

			if(RoomFits(room)) {
				for(int i = 0; i < room.width; i++){
					for(int j = 0; j < room.height; j++){
						mapValues[i + (int)room.position.x, j + (int)room.position.y] = room.room[i,j];
					}
				}
				rooms.Add (room);
				attempt = 0;
			} else {
				attempt++;
			}
		}
	}

	void DigPaths () {
		for (int j = 0; j < rooms.Count - 1; j++) {
			float startingX = rooms[j].position.x + (rooms[j].width / 2);
			float startingY = rooms[j].position.y + (rooms[j].height / 2);
			float targetX = rooms[j+1].position.x + (rooms[j+1].width / 2);
			float targetY = rooms[j+1].position.y + (rooms[j+1].height / 2);
			
			List<Vector2> path = PathFinder.instance.AStar (mapValues, startingX, startingY, targetX, targetY);
			for(int i = 0; i < path.Count; i++) {
				if(mapValues[(int)path[i].x, (int)path[i].y] == stoneValue){
					if(i + 1 < path.Count && mapValues[(int)path[i+1].x, (int)path[i+1].y] == stoneValue) {
						mapValues[(int)path[i].x, (int)path[i].y] = floorValue;
					}
					else {
						mapValues[(int)path[i].x, (int)path[i].y] = doorValue;
					}
				}
				else if(mapValues[(int)path[i].x, (int)path[i].y] == wallValue) {
					mapValues[(int)path[i].x, (int)path[i].y] = floorValue;
				}
			}
		}
	}

	bool RoomFits(Room room) {
		float x = room.position.x;
		float y = room.position.y;
		for(int i = 0; i < room.width; i++) {
			for (int j = 0; j < room.height; j++) {
				if (i + (int)x >= width || j + (int)y >= height || mapValues[i + (int)x,j + (int)y] != wallValue) {
					return false;
				}
			}
		}
		return true;
	}



}
