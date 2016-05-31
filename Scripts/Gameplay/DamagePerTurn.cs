using UnityEngine;
using System.Collections;

public class DamagePerTurn : BaseStatusEffect {

	private Player target;

	public DamagePerTurn (Player target) {
		this.target = target;
	}

	public override void Execute () {
		target.ReceiveDamage(2, false);
	}
}
