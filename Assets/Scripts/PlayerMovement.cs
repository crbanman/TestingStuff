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
	}

	void OnEnable () {
		TurnManager.PlayerTurn += MoveButton;
		EventManager.OnLeftMouseClick += OnMouseClick;
	}

	void OnDisable () {
		TurnManager.PlayerTurn -= MoveButton;
		EventManager.OnLeftMouseClick += OnMouseClick;
	}

	void OnMouseClick(Vector3 position) {
		path = PathFinder.instance.AStar ((int)transform.position.x, (int)transform.position.y, (int)position.x, (int)position.y, true);
		if(isMoving)
			StopCoroutine("MoveOnPath");
		StartCoroutine("MoveOnPath");
	}

	IEnumerator MoveOnPath() {
		isMoving = true;
		while(path != null && path.Count > 0) {
			Move(path[path.Count - 1]);
			path.RemoveAt(path.Count - 1);
			
			yield return new WaitForSeconds(0.05f);
		}
		isMoving = false;
		yield return null;
	}

	void Move (Vector3 position) {
		if(position.x + 0.5f > transform.position.x) {
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if(position.x + 0.5f < transform.position.x) {
			transform.localScale = new Vector3(-1, 1, 1);
		}

		transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f);
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
