using UnityEngine;
using System.Collections.Generic;

public class GameplayStateMachineRunner : MonoBehaviour {

	[SerializeField]
	private List<Player> players;
	
	private GameplayStateMachine machine;
	public GameplayStateMachine Machine {
		get { return machine; }
	}

	private void Start () {
        machine = new GameplayStateMachine(players);
		machine.CurrentState.Run ();
	}
}
