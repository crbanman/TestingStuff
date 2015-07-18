using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLevel : MonoBehaviour {

	public GameObject wall;
	public GameObject floor;
	public GameObject stone;
	public Room room;
	
	public int width = 150;
	public int height = 150;

	public int maxRooms = 10;
	public int minRooms = 5;

	public static int floorValue = 1;
	public static int wallValue = 4;
	public static int stoneValue = 20;

	private int[,] mapValues;
	private GameObject[,] mapTiles;
	private List<GameObject> rooms;

	void Start () {
		FillMap ();
		PlaceRooms ();
	}

	void FillMap () {
		mapValues = new int[width, height];
		mapTiles = new GameObject[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				mapValues[i,j] = wallValue;
				mapTiles[i,j] = Instantiate(wall, new Vector3(i, j), Quaternion.identity) as GameObject;
				mapTiles[i,j].transform.parent = transform;
			}
		}
	}

	void PlaceRooms () {
		int numRooms = Random.Range(minRooms, maxRooms);
		rooms = new List<GameObject>();

		for(int attempt = 0; rooms.Count < numRooms && attempt < 100;) {
			int x = Random.Range (0, width - 1);
			int y = Random.Range (0, height - 1);

			if(RoomFits(x, y)) {
				ClearArea(x, y);
				List<GameObject> list = room.RoomToList();
				foreach (GameObject tile in list) {			
					GameObject newTile = Instantiate(tile, new Vector3(tile.transform.position.x + x, tile.transform.position.y + y), Quaternion.identity) as GameObject;
					newTile.transform.parent = transform;
				}
				attempt = 0;
			} else {
				attempt++;
			}
		}
	}

	bool RoomFits(int x, int y) {
		for(int i = 0; i < room.width; i++) {
			for (int j = 0; j < room.height; j++) {
				if (i + x >= width || j + y >= height || mapValues[i + x,j + y] != wallValue) {
					return false;
				}
			}
		}
		return true;
	}

	void ClearArea(int x, int y) {
		List<GameObject> tiles = room.RoomToList();
		int[,] tileValues = room.RoomToIntArray();

		foreach (GameObject tile in tiles) {
			int tileX = (int)tile.transform.position.x + x;
			int tileY = (int)tile.transform.position.y + y;

			Destroy (mapTiles[tileX,tileY]);
			mapValues[tileX, tileY] = tileValues[(int)tile.transform.position.x, (int)tile.transform.position.y];
		}
	}



}
