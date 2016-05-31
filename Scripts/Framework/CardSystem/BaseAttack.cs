using UnityEngine;
using System.Collections;

public abstract class BaseAttack {

	#region Abstract Functions
	/// <summary>
	/// Calculates the direction of the attack.
	/// </summary>
	/// <param name="testObj">AttackPointer moves to where the attack needs to be placed.</param>
	/// <param name="start">Start position.</param>
	/// <param name="target">Target player position.</param>
	/// <param name="attackDirections">Attack directions.</param>
	public abstract void InitializeAttack (AttackPointer attackPointer, Player start, Player target,
	                                       string[] attackDirections, int damageAmount);

	#endregion

	/// <summary>
	/// Calculates the position of where the attack needs to be placed with the attackDirections of the current card
	/// </summary>
	/// <param name="attackPointer">attackPointer moves to where the attack needs to be placed.</param>
	/// <param name="start">Start position.</param>
	/// <param name="target">Target player position.</param>
	/// <param name="attackDirections">Attack directions.</param>
	protected void AttackPlacement (AttackPointer attackPointer, Player start, Player target, string[] attackDirections) {
		float gridHeight = start.GetGameManager.gridInfo.GridHeight;
		float gridWidth = start.GetGameManager.gridInfo.GridWidth;
		attackPointer.DisableCollider ();
		attackPointer.transform.localPosition = Vector3.zero;
		foreach (string s in attackDirections) {
			if (s == "forward") {
				attackPointer.transform.position += Vector3.forward*gridHeight;
			}
			else if (s == "back") {
				attackPointer.transform.position += Vector3.back*gridHeight;
			}
			else if (s == "left") {
				attackPointer.transform.position += Vector3.left*gridWidth;
			}
			else if (s == "right") {
				attackPointer.transform.position += Vector3.right*gridWidth;
			}
		}
	}

	/// <summary>
	/// Sets the damage to the attackPointer so the right amount of damage will be given when it collides with target.
	/// </summary>
	/// <param name="attackpointer">Attackpointer.</param>
	/// <param name="damageAmount">Damage amount.</param>
	protected void SetDamage (AttackPointer attackpointer, int damageAmount) {
		attackpointer.SetDamage (damageAmount);
	}
	protected void SetParticle (AttackPointer attackPointer, GameObject obj) {
		attackPointer.StartExplosion (obj);
	}
}
