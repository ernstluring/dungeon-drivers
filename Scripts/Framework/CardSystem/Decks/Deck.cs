using UnityEngine;
using System.Collections.Generic;

public class Deck : BaseDeck {

	private List<BaseCard> deck = new List<BaseCard>();

	public override List<BaseCard> GetAllCardsInDeck {
		get {
			return deck;
		}
	}

	public Deck () {
		base.graveyard = new List<BaseCard>();
	}

	public override void AddToDeck (BaseCard card) {
		deck.Add ((BaseCard)card);
	}

	public override bool Shuffle () {
		if (deck.Count > 0) {
			for (int i = deck.Count - 1; i > 0; i--) {
				int j = GameManager.random.Next(i+1);
				BaseCard temp = deck[i];
				deck[i] = deck[j];
				deck[j] = temp;
			}
			return true;
		} else {
			return false;
		}
	}

	public override BaseCard Draw () {
		BaseCard cardToReturn = deck[0];
		deck.RemoveAt(0);
		base.graveyard.Add (cardToReturn);
		return cardToReturn;
	}

	public override int Count () {
		return deck.Count;
	}

	public override bool IsEmpty () {
		return deck.Count == 0;
	}
}
