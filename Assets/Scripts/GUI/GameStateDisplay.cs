using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStateDisplay : MonoBehaviour {

	Text text;
	void Awake()
	{
		text = GetComponent<Text> ();
	}

	void Update () 
	{
		text.text = "Current GameState = " + GameStates.gameState;
	}
}
