using UnityEngine;
using System.Collections;

public abstract class StateMachine {

	// Accessor to look at the current state.
	public abstract State CurrentState { get; }

	// Advance to the next state and run it.
	public abstract void AdvanceAndRun ();

	// Is this state a "completion" state?
	public abstract bool IsComplete ();
}
