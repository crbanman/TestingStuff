using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public static Map instance;

	public static float xMin = 0;
	public static float yMin = 0;

	public static float xMax = 0;
	public static float yMax = 0;

	public static int width;
	public static int height;

	public static GroundTile[,] walkableTiles;


	void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	public void InitializeMap () {
		IdentifyMaxPosition();
		IdentifyWalkableTiles ();
	}



	void IdentifyMaxPosition () {
		xMin = 0;
		xMax = width - 1;
		yMin = 0;
		yMax = height - 1;
		width = GenerateLevel.width;
		height = GenerateLevel.height;
	}

	void IdentifyWalkableTiles () {
		walkableTiles = new GroundTile[width, height];
		
		foreach (Transform child in transform) {
			if (child.tag == "Floor"){
				GroundTile tile = child.gameObject.GetComponent<GroundTile>();
				walkableTiles[(int) tile.transform.position.x, (int) tile.transform.position.y] = tile;
			}
		}
	}

	public static bool IsTileWalkable(float x, float y) {
		if(x < xMin || y < yMin || x > xMax || y > yMax || walkableTiles[(int)x, (int) y] == null) {
			return false;
		}
		return walkableTiles[(int)x, (int) y].isWalkable();
	}
}
