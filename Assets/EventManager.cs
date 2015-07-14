using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	
	public delegate void ButtonPress();

	public static event ButtonPress OnDirectionButtonPress;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) && OnDirectionButtonPress != null) {
			OnDirectionButtonPress();
		}
	}
}
