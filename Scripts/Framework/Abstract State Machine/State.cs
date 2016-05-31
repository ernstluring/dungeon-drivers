using UnityEngine;
using System.Collections.Generic;

public abstract class State {

	public abstract State NextState { get; set; }

	// Function to get the name of the state
	public abstract string GetName ();

	// Do something
	public abstract void Run ();

}
