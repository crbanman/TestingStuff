using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	
	public delegate void ButtonPress();
	public static event ButtonPress OnDirectionButtonPress;

	public delegate void MouseClick(Vector3 position);
	public static event MouseClick OnLeftMouseClick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) && OnDirectionButtonPress != null) {
			OnDirectionButtonPress();
		}

		if(Input.GetMouseButtonDown(0) && OnLeftMouseClick != null) {
			Vector3 posVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			posVec.z = transform.position.z;
			Debug.Log ("(" + posVec.x + ", " + posVec.y + ")");

			if(posVec.x < 0)
				posVec.x -= 1;

			if(posVec.y < 0)
				posVec.y -= 1;
			

			OnLeftMouseClick(posVec);
		}
	}
}
