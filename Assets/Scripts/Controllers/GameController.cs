using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public bool isDragging;

    void Start()
    {
        isDragging = false;
        GameStates.gameState = States.GameState.WAITING;
    }

    void Update()
    {
        //Debug.Log(GameStates.gameState);
    }

    public void ChangeState()
    {
        GameStates.gameState = States.GameState.FIGHTING;
    }
}
