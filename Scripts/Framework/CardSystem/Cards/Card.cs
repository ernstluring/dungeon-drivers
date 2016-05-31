using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Card : BaseCard {

	private IAbility ability;
	private BaseAttack attack;

	public BaseAttack GetAttack {
		get { return attack; }
	}

	public IAbility GetAbility {
		get { return ability; }
	}

	[XmlAttributeAttribute()]
	public string race;

	public string name;
	public int damage;

	[XmlArray("attackDirections"), XmlArrayItem("attackDirection")]
	public string[] attackDirections;

	public int movementCount;
	public int actionPointUse;
	public string art;
	public string description;

	[XmlArray("abilities"), XmlArrayItem("ability")]
	public string[] abilitiesArray;

	public override void SetAbility (IAbility ability)
	{
		this.ability = ability;
	}

	public override void SetAttack (BaseAttack attack)
	{
		this.attack = attack;
	}
}
