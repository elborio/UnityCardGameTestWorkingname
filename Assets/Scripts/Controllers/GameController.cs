using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	void Start()
	{
		GameStates.gameState = States.GameState.WAITING;
	}
	void Update()
	{
		Debug.Log (GameStates.gameState);
	}

	public void ChangeState()
	{
		GameStates.gameState = States.GameState.FIGHTING;
	}
}
