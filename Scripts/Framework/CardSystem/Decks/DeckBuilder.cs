using UnityEngine;
using System.Collections.Generic;

public class DeckBuilder {

	private CardLibrary cardLibrary;

	private BaseDeck deck;

	public DeckBuilder (CardsManager cardsManager) {
		deck = new Deck ();
		cardLibrary = cardsManager.cardLibrary;
	}

	public BaseDeck BuildDeck (PlayerType playerType, int chosenWheels, int chosenWeapon) {
		foreach (Card card in cardLibrary.genericCards) {
			deck.AddToDeck (card);
		}

		switch (playerType) {
		case PlayerType.DWARF:
			foreach (Card card in cardLibrary.cards) {
				if (card.race == "Dwarf") {
					if (chosenWheels == 0) {
						if (card.name == "Fast n Furious") {
							deck.AddToDeck (card);
						}
					}
					if (chosenWheels == 1) {
						if (card.name == "Bumpy Ride") {
							deck.AddToDeck (card);
						}
					}
					if (chosenWeapon == 0) {
						if (card.name == "Hell Rain") {
							deck.AddToDeck (card);
						}
					}
					if (chosenWeapon == 1) {
						if (card.name == "Meteor Strike") {
							deck.AddToDeck (card);
						}
					}
				}
			}
			break;
		case PlayerType.GOBLIN:
			foreach (Card card in cardLibrary.cards) {
				if (card.race == "Goblin") {
					if (chosenWheels == 0) {
						if (card.name == "Fast n Furious") {
							deck.AddToDeck (card);
						}
					}
					if (chosenWheels == 1) {
						if (card.name == "Bumpy Ride") {
							deck.AddToDeck (card);
						}
					}
					if (chosenWeapon == 0) {
						if (card.name == "Hell Fire") {
							deck.AddToDeck (card);
						}
					}
					if (chosenWeapon == 1) {
						if (card.name == "Bonfire") {
							deck.AddToDeck (card);
						}
					}
				}
			}
			break;
		}
		return deck;
	}
}
