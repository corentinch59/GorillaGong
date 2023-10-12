using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameStates
{
	[CreateAssetMenu(
	    fileName = "GameStateVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "GameState",
	    order = 0)]
	public class GameStateVariable : BaseVariable<GameState, UnityEvent<GameState>>
	{
	}
}