using UnityEngine;
using System;
using ExtensionMethods;

public class CardsManager : MonoBehaviour {

	[SerializeField]
	private TextAsset xmlFile;

	private string xmlPath = "Resources/cards.xml";

	public CardLibrary cardLibrary;

	private void Start () {
		// Load cards from XML
		cardLibrary = CardLibrary.Load(xmlFile);

		InitializeCards ();
	}

	private void InitializeCards () {
		// Set the attack & abilities of all card classes in the cardlibrary
//		foreach (Card card in cardLibrary.cards) {
//			string attackName = card.name.RemoveWhitespace();
//			card.SetAttack((BaseAttack)Activator.CreateInstance(Type.GetType(attackName, true)));
//			string abilityName = card.abilitiesArray[0].RemoveWhitespace();
//			card.SetAbility((IAbility)Activator.CreateInstance(Type.GetType(abilityName, true)));
//		}
		foreach (Card genericCard in cardLibrary.genericCards) {
			string attackName = genericCard.name.RemoveWhitespace();
			genericCard.SetAttack((BaseAttack)Activator.CreateInstance(Type.GetType(attackName, true)));
		}
		foreach (Card card in cardLibrary.cards) {
			string attackName = card.name.RemoveWhitespace();
			card.SetAttack((BaseAttack)Activator.CreateInstance(Type.GetType(attackName, true)));
		}
	}
}
