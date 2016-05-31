public interface IAttack {
	void Damage ();
	void CalcDirection (UnityEngine.GameObject testObj, Player start, Player target, string[] attackDirections);
	void Attack ();
}

public interface IAbility {
	void Ability ();
}
