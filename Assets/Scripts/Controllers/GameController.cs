using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public enum gameState
	{
		CHOOSING,
		FIGHTING,
		TRADING,
		LOOTING,
		WAITING
	}

	public enum eventType
	{
		TREASUREROOM,
		STORY,
		LEGENDARYMONSTER,
		MONSTER
	}
}
