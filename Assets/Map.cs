using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public static float xMin = 0;
	public static float yMin = 0;

	public static float xMax = 0;
	public static float yMax = 0;

	public static int width;
	public static int height;

	public static GroundTile[,] walkableTiles;

	// Use this for initialization
	void Awake () {
		IdentifyMaxPosition();
		IdentifyWalkableTiles ();

	}

	void IdentifyMaxPosition () {
		foreach (Transform child in transform) {
			if(child.position.x < xMin)
				xMin = child.position.x;
			if(child.position.y < yMin)
				yMin = child.position.y;
			if(child.position.x > xMax)
				xMax = child.position.x;
			if(child.position.y > yMax)
				yMax = child.position.y;
		}
		width = (int) (xMax + -xMin) + 1;
		height = (int) (yMax + -yMin) + 1;
		Debug.Log ("Max Position is (" + xMax + ", " + yMax + ")");
	}

	void IdentifyWalkableTiles () {
		walkableTiles = new GroundTile[width, height];
		
		foreach (Transform child in transform) {
			GroundTile tile = child.gameObject.GetComponent<GroundTile>();
			walkableTiles[(int) -(xMin - tile.transform.position.x), (int) -(yMin - tile.transform.position.y)] = tile;
		}
	}

	public static bool IsTileWalkable(float x, float y) {
		if(x < xMin || y < yMin || x > xMax || y > yMax || walkableTiles[(int) -(xMin - x), (int) -(yMin - y)] == null) {
			return false;
		}
		return walkableTiles[(int) -(xMin - x), (int) -(yMin - y)].isWalkable();
	}
}
