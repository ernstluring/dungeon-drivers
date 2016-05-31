using UnityEngine;
using System.Collections.Generic;

public abstract class BaseDeck {

	public List<BaseCard> graveyard;

	public abstract List<BaseCard> GetAllCardsInDeck { get; }
	public abstract void AddToDeck (BaseCard card);
	public abstract bool Shuffle ();
	public abstract BaseCard Draw ();
	public abstract int Count ();
	public abstract bool IsEmpty ();
}
