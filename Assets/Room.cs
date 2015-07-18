using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	public int width;
	public int height;

	public int[,] RoomToIntArray() {
		int[,] roomIntArray = new int[width, height];
		foreach (Transform child in transform){
			int x = (int)child.position.x;
			int y = (int)child.position.y;

			if(child.tag == "Floor") {
				roomIntArray[x, y] = GenerateLevel.floorValue;
			}
			else if (child.tag == "Wall") {
				roomIntArray[x, y] = GenerateLevel.wallValue;
			}
			else if (child.tag == "Stone") {
				roomIntArray[x, y] = GenerateLevel.stoneValue;
			}
		}
		return roomIntArray;
	}

	public List<GameObject> RoomToList() {
		List<GameObject> list = new List<GameObject>();
		foreach(Transform child in transform) {
			list.Add (child.gameObject);
		}
		return list;
	}
}
