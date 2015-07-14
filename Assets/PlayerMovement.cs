using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	List<Vector2> path;
	bool isMoving = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			var posVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			posVec.z = transform.position.z;
			
			path = PathFinder.instance.AStar ((int)transform.position.x, (int)transform.position.y, (int)posVec.x, (int)posVec.y);
			if(isMoving)
				StopCoroutine("MoveOnPath");
			StartCoroutine("MoveOnPath");
		}
	}

	void OnEnable () {
		TurnManager.PlayerTurn += MoveButton;
	}

	void OnDisable () {
		TurnManager.PlayerTurn -= MoveButton;
	}

	IEnumerator MoveOnPath() {
		isMoving = true;
		while(path != null && path.Count > 0) {
			transform.position = new Vector3(path[path.Count - 1].x + 0.5f, path[path.Count - 1].y + 0.5f);
			path.RemoveAt(path.Count - 1);
			
			yield return new WaitForSeconds(0.05f);
		}
		isMoving = false;
		yield return null;
	}

	void Move (string direction) {

		if(direction == "up") {
			transform.position += Vector3.up;
		}
		else if (direction == "right") {
			transform.position += Vector3.right;
			transform.localScale = new Vector3(1, 1, 1);

		}
		else if (direction == "down") {
			transform.position += Vector3.down;
		}
		else if (direction == "left") {
			transform.position += Vector3.left;
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	void MoveButton () {
		
		bool up = Input.GetKeyDown(KeyCode.UpArrow);
		bool right = Input.GetKeyDown(KeyCode.RightArrow);
		bool down = Input.GetKeyDown(KeyCode.DownArrow);
		bool left = Input.GetKeyDown(KeyCode.LeftArrow);
		
		if(up) {
			Move ("up");
		}
		else if (right) {
			Move ("right");
		}
		else if (down) {
			Move ("down");
		}
		else if (left) {
			Move ("left");
		}
	}
}
