using UnityEngine;
using System.Collections;

public class FastnFurious : BaseAttack {

	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target,
	                                       string[] attackDirections, int damageAmount) 
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);   
	}
}

public class Rushem : BaseAttack {
	
	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target,
	                                       string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);  
	}
}

public class Twinshot : BaseAttack {

	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target,
	                                       string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);  
	}
}

public class DriveByShooting : BaseAttack {
	
	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
	                                       string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections); 
	}
}

public class BastardBallista : BaseAttack {
	
	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
	                                       string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);
	}
}

public class TheBigLoad : BaseAttack {
	
	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
	                                       string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);   
	}
}

public class MakeitRain : BaseAttack {
	
	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
	                                       string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);
	}
}

public class BumpyRide : BaseAttack {

	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
		string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);
	}
}

public class HellFire : BaseAttack {

	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
		string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);
	}
}

public class HellRain : BaseAttack {

	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
		string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);
	}
}

public class Bonfire : BaseAttack {

	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
		string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);
	}
}

public class MeteorStrike : BaseAttack {

	public override void InitializeAttack (AttackPointer attackPointer, Player start, Player target, 
		string[] attackDirections, int damageAmount)
	{
		// Pass the right amount of damage to the attackPointer, so the target will get this damage by collision check
		base.SetDamage(attackPointer, damageAmount);
		// Check the direction of the attack and place it there
		base.AttackPlacement (attackPointer, start, target, attackDirections);
	}
}