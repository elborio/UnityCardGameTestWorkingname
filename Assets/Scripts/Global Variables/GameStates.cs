using UnityEngine;
using System.Collections;

namespace States
{
	public enum GameState
	{
		CHOOSING,
		FIGHTING,
		TRADING,
		LOOTING,
		WAITING
	}
	;
	
	public enum EventType
	{
		TREASUREROOM,
		STORY,
		LEGENDARYMONSTER,
		MONSTER
	}
	;
}

public static class GameStates
{
	public static States.GameState gameState;
	public static States.EventType eventType;
}
