using UnityEngine;
using System.Collections;

public class HealPerTurn : BaseStatusEffect {

	private Player target;

	public HealPerTurn (Player target) {
		this.target = target;
	}

	public override void Execute ()	{
		target.Heal (1);
	}
}
