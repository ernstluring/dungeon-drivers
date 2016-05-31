using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Draw state.
/// </summary>
public class DrawState : State {

	private GameplayStateMachine machine;

	private State nextState;

	private int firstTimeDrawingCount = 0;

	public override State NextState {
		get {
			return nextState;
		}
		set {
			if (nextState == null)
				nextState = value;
		}
	}

	public DrawState (GameplayStateMachine machine) {
		this.machine = machine;
	}

	public override string GetName ()
	{
		return this.ToString();
	}

	public override void Run ()
	{
		for (int i = 0; i < machine.Players.Count; i++) {
			machine.Players[i].phaseIndicator.SetText("Drawing");
			machine.Players[i].phaseIndicator.DoTransition();
		}

		// If this draw turn is the first draw turn in the game, draw cards in hand of all players
		if (firstTimeDrawingCount == 0) {
			machine.activePlayer.GetHand.DrawCards ();
			machine.activePlayer.HandCamera.enabled = false;
			foreach (Player p in machine.passivePlayers) {
				p.GetHand.DrawCards ();
				p.HandCamera.enabled = false;
			}
			machine.AdvanceAndRun ();
			firstTimeDrawingCount++;
		} else {
			// Draw cards in active player hand
			machine.activePlayer.GetHand.DrawCards();
			machine.AdvanceAndRun ();
		}
	}
}

/// <summary>
/// Play state.
/// </summary>
public class PlayState : State {

	private GameplayStateMachine machine;
	private State nextState;

	public override State NextState {
		get {
			return nextState;
		}
		set {
			nextState = value;
		}
	}

	public PlayState (GameplayStateMachine machine) {
		this.machine = machine;
	}
	
	public override string GetName ()
	{
		return this.ToString();

	}
	
	public override void Run ()
	{
		// Set current player active and other players passive
		machine.activePlayer.SetPlayerActive();
		machine.activePlayer.phaseIndicator.SetText("Play");
		machine.activePlayer.phaseIndicator.DoTransition();
		foreach (Player p in machine.passivePlayers) {
			p.SetPlayerPassive();
			p.phaseIndicator.SetText("Waiting");
			p.phaseIndicator.DoTransition();
		}
			
		// When the active player is in pull position at the end of his turn, he gets health.
		// When he is in the ditch at the end of his turn, he gets damage
		if (machine.activePlayer.boardStatusEffect != null && machine.activePlayer.boardStatusEffect is DamagePerTurn) {
			machine.activePlayer.boardStatusEffect.Execute ();
			machine.activePlayer.boardStatusEffect = null;
		}
		if (machine.activePlayer.boardStatusEffect != null && machine.activePlayer.boardStatusEffect is HealPerTurn) {
			machine.activePlayer.boardStatusEffect.Execute ();
			machine.activePlayer.boardStatusEffect = null;
		}
	}
}

/// <summary>
/// Burn state.
/// </summary>
public class BurnState : State {
	private GameplayStateMachine machine;
	private State nextState;

	public override State NextState {
		get {
			return nextState;
		}
		set {
			nextState = value;
		}
	}

	public BurnState (GameplayStateMachine machine) {
		this.machine = machine;
	}
	
	public override string GetName ()
	{
		return this.ToString();
	}
	
	public override void Run ()
	{
		machine.activePlayer.SetCanBurn (true);
		machine.activePlayer.phaseIndicator.SetText("Burn");
		machine.activePlayer.phaseIndicator.DoTransition();
		foreach (Player p in machine.passivePlayers) {
			p.SetCanBurn (false);
			p.phaseIndicator.SetText("Waiting");
			p.phaseIndicator.DoTransition();
		}
	}
}

/// <summary>
/// End state.
/// </summary>
public class EndState : State {
	private GameplayStateMachine machine;
	private State nextState;

	public override State NextState {
		get {
			return nextState;
		}
		set {
			nextState = value;
		}
	}

	public EndState (GameplayStateMachine machine) {
		this.machine = machine;
	}
	
	public override string GetName ()
	{
		return this.ToString();
	}
	
	public override void Run ()
	{
		// When the active player is in pull position at the end of his turn, he gets health.
		if (machine.activePlayer.boardStatusEffect != null && machine.activePlayer.boardStatusEffect is HealPerTurn) {
			machine.activePlayer.boardStatusEffect.Execute ();
			machine.activePlayer.boardStatusEffect = null;
		}


		// When there are only 2 players, passive player 0 becomes active player
		if (machine.passivePlayers.Count == 1) {
			machine.passivePlayers.Add (machine.activePlayer);
			machine.activePlayer = machine.passivePlayers[0];
			machine.passivePlayers.RemoveAt (0);
		} else {

		}

		machine.activePlayerNumber = machine.activePlayer.PlayerNumber;
		machine.activePlayer.SetPlayerActive();
		machine.AdvanceAndRun ();
	}
}

/// <summary>
/// End game state.
/// </summary>
public class EndGameState : State {
	private GameplayStateMachine machine;
	private State nextState;

	public override State NextState {
		get {
			return nextState;
		}
		set {
			nextState = value;
		}
	}
	
	public EndGameState (GameplayStateMachine machine) {
		this.machine = machine;
	}
	
	public override string GetName ()
	{
		return this.ToString();
	}
	
	public override void Run ()
	{
		Debug.Log ("EndGame state!");
	}
}
