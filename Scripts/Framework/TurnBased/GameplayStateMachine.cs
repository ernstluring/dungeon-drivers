using UnityEngine;
using System.Collections.Generic;

public class GameplayStateMachine : StateMachine {

	private List<Player> players = new List<Player>();
	public List<Player> Players {
		get { return players; }
	}

	public Player activePlayer;
	public int activePlayerNumber;
	public List<Player> passivePlayers = new List<Player>();

	private State currentState;
	private State exitState;

	private State drawState, playState, burnState, endState, endgameState;


	public GameplayStateMachine (List<Player> players) {
		drawState = new DrawState(this);
		playState = new PlayState(this);
		burnState = new BurnState(this);
		endState = new EndState(this);
		endgameState = new EndGameState(this);

		drawState.NextState = playState;
		playState.NextState = burnState;
		burnState.NextState = endState;
		endState.NextState = drawState;

		currentState = drawState;
		exitState = endgameState;

		foreach (Player player in players) {
			this.players.Add(player);
		}

		activePlayer = players[0];
		activePlayerNumber = activePlayer.PlayerNumber;
//		activePlayer.HandCamera.gameObject.SetActive(true);

		foreach (Player p in this.players) {
			if (p.PlayerNumber != activePlayerNumber) {
				passivePlayers.Add (p);
//				p.HandCamera.gameObject.SetActive(false);
			}
		}

		// Only works with 2 players!
		if (passivePlayers.Count == 1) {
			activePlayer.SetOpponent (passivePlayers[0]);
			passivePlayers[0].SetOpponent (activePlayer);
		}
	}

	public override State CurrentState {
		get {
			return currentState;
		}
	}

	public override void AdvanceAndRun () {
		currentState = currentState.NextState;
		currentState.Run ();
	}

	public override bool IsComplete () {
		return currentState == exitState;
	}
}
