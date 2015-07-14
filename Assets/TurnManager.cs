using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	public delegate void Turn();
	public static event Turn PlayerTurn;
	public static event Turn EnemyTurn;

	public static void ExecuteTurn () {
		Debug.Log("Player Turn");
		if (PlayerTurn != null){
			PlayerTurn();
		}

		Debug.Log ("Enemy Turn");
		if (EnemyTurn != null) {
			EnemyTurn();
		}
	}

	public void OnEnable () {
		EventManager.OnDirectionButtonPress += ExecuteTurn;
	}

	public void OnDisable () {
		EventManager.OnDirectionButtonPress -= ExecuteTurn;
	}


}
