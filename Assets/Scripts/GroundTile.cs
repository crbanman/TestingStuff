using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour {

	bool occupied = false;

	void OnTriggerEnter2D(Collider2D other) {
		occupied = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		occupied = false;
	}
	
	public bool isWalkable() {
		return !occupied;
	}
}
