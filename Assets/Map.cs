using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public static Map instance;

	public static float xMax = 0;
	public static float yMax = 0;

	public static int width;
	public static int height;

	public static GroundTile[,] walkableTiles;

	// Use this for initialization
	void Awake () {
		if(instance == null) {
			instance = this;
		}
		IdentifyMaxPosition();
		IdentifyWalkableTiles ();

	}

	void IdentifyMaxPosition () {
		foreach (Transform child in transform) {
			if(child.position.x > xMax)
				xMax = child.position.x;
			if(child.position.y > yMax)
				yMax = child.position.y;
		}
		width = (int) xMax + 1;
		height = (int) yMax + 1;
		Debug.Log ("Max Position is (" + xMax + ", " + yMax + ")");
	}

	static void IdentifyWalkableTiles () {
		walkableTiles = new GroundTile[width, height];
		
		foreach (Transform child in instance.transform) {
			GroundTile tile = child.gameObject.GetComponent<GroundTile>();
			walkableTiles[(int) tile.transform.position.x, (int) tile.transform.position.y] = tile;
		}
	}

	public static bool IsTileWalkable(float x, float y) {
		//IdentifyWalkableTiles ();
		if(x < 0 || y < 0 || x > xMax || y > yMax || walkableTiles[(int) x, (int) y] == null) {
			return false;
		}
		return walkableTiles[(int)x, (int)y].isWalkable();
	}
}
