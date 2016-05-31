using UnityEngine;
using System.Collections.Generic;

public abstract class BaseCard {
	public abstract void SetAbility (IAbility ability);
	public abstract void SetAttack (BaseAttack attack);
}
