using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	bool occupied = false;

	public bool isWalkable() {
		return !occupied;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "entity") {
			occupied = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "entity") {
			occupied = false;
		}
	}

}
