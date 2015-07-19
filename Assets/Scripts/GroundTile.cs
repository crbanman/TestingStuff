using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour {

	bool occupied = false;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Entered");
		occupied = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log ("Exited");
		occupied = false;
	}
	
	public bool isWalkable() {
		return !occupied;
	}
}
