using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using Random = System.Random;

public class Hand : MonoBehaviour {

	private Player player;
	private List<CardModel> poolObjectsList = new List<CardModel>(10);

	private Vector3 poolPosition = new Vector3 (0, -100, 0);

	private List<CardModel> cardsInHandList = new List<CardModel>(6);

	[SerializeField]
	private Transform[] cardPositions;

	[SerializeField]
	private CardModel cardPrefab;

	private int cardSelectedPointer;
	private CardModel selectedCardObject;

	private DeckBuilder deckBuilder;
	private BaseDeck currentDeck;

	private float originalScale;
	private float originalYposition;
	private float growScale = 0.03f;
	private float growPosition = 1;

	private bool isZooming = false;

	private bool canMove = false;
	public bool CanMove {
		get { return canMove; }
	}

	private int movementCount;
	public int GetMovementCount {
		get { return movementCount; }
	}

	void Awake () {
		player = GetComponent<Player>();
	}

	void Start(){
		deckBuilder = new DeckBuilder (player.GetCardsManager);
		currentDeck = deckBuilder.BuildDeck (player.GetPlayerType, player.GetCarParts.activeWheels, player.GetCarParts.activeWeapon);
		currentDeck.Shuffle ();
		SetUpPool ();
	}

	private void SetUpPool () {
		for (int i = 0; i < 10; i++) {
			CardModel poolObject = (CardModel)Instantiate(cardPrefab);
			poolObject.transform.position = poolPosition;
			poolObjectsList.Add (poolObject);
			poolObject.gameObject.SetActive(false);
		}
		originalScale = poolObjectsList[0].transform.localScale.x;
		originalYposition = cardPositions[0].position.y;
	}

	public void DrawCards(){
		if (selectedCardObject != null) {
			ResetSelectedCard ();
		}
		if (cardsInHandList.Count < 6) {
			int count = (cardsInHandList.Count + 6) - 6;
			for (int i = count; i < 6; i++) {
				CardModel cardObject = poolObjectsList[0];
				poolObjectsList.RemoveAt (0);
				cardObject.transform.position = cardPositions[i].position;
				cardObject.transform.localScale = new Vector3 (originalScale, originalScale, originalScale);
				cardObject.transform.SetParent (player.HandCamera.transform);
				cardObject.gameObject.SetActive(true);
				cardObject.CardData = (Card)currentDeck.Draw ();
				cardObject.FillCardFields ();
				cardsInHandList.Add (cardObject);
			}
		}
		cardSelectedPointer = 0;
		selectedCardObject = cardsInHandList[cardSelectedPointer];
	}

	public void SelectCardRight () {
		isZooming = false;
		if (cardSelectedPointer >= 0 && cardSelectedPointer < cardsInHandList.Count-1) {
			IncreaseCardSelectedPointer ();
			ScaleSelectedCard ();
		}
	}

	public void SelectCardLeft () {
		isZooming = false;
		if (cardSelectedPointer != 0) {
			DecreaseCardSelectedPointer ();
			ScaleSelectedCard ();
		}
	}

	public void PlayCard () {
		// Move the card toward the car
		PlayCardScript playCardComp = selectedCardObject.GetComponent<PlayCardScript>();
		playCardComp.Play (transform.position, poolPosition);
		// Get movementCount of current selected card
		movementCount = selectedCardObject.CardData.movementCount;
		// Set canMove to true, so the DPAD/WASD can be used to move the car
		if (movementCount > 0)
			canMove = true;
		else
			StartCoroutine (player.Movement(""));
		player.ActionPointUsed ();
	}

	public void NextCardAfterMovement () {
		poolObjectsList.Add (selectedCardObject);
		cardsInHandList.RemoveAt (cardSelectedPointer);
		ReorderHand ();
		ScaleSelectedCard ();
		canMove = false;
	}

	private void ReorderHand () {
		// Move the remaining cards after the played card to the left to fill up the space in hand
		for (int i = cardSelectedPointer; i < cardsInHandList.Count; i++) {
			cardsInHandList[i].transform.position = cardPositions[i].position;
		}
		cardSelectedPointer = 0;
	}

	public void BurnCard () {
		//TODO Implement burning particle
		selectedCardObject.transform.position = poolPosition;
		selectedCardObject.gameObject.SetActive(false);

		poolObjectsList.Add (selectedCardObject);
		cardsInHandList.RemoveAt (cardSelectedPointer);

		ReorderHand ();
		ScaleSelectedCard ();
		player.BurnPointUsed ();
	}

	public void ExamineCard () {
		//TODO Implement examine card
		isZooming = !isZooming;
		if (isZooming == true)
			selectedCardObject.transform.localScale = Vector3.one * 0.05f;
		else
			selectedCardObject.transform.localScale = Vector3.one * originalScale;
	}

	public Card GetAttackCardFromHand () {
		return selectedCardObject.CardData;
	}

	private void IncreaseCardSelectedPointer () {
		cardSelectedPointer++;
	}

	private void DecreaseCardSelectedPointer () {
		cardSelectedPointer--;
	}

	public void DecreaseMovementCounter () {
		movementCount--;
	}

	public void ScaleSelectedCard () {
		// Reset scale of previous card
		ResetSelectedCard ();

		// Set current selected card
		selectedCardObject = cardsInHandList[cardSelectedPointer];

		// Increase the scale of the selected card
		selectedCardObject.transform.localScale = Vector3.one * growScale;

		// Increase the position of the selected card
		Vector3 tempPos = selectedCardObject.transform.position;
		tempPos.y += growPosition;
		selectedCardObject.transform.position = tempPos;

		SelectedCardIndicators ();
	}

	private void SelectedCardIndicators () {

		player.IndicatorPointer.gameObject.SetActive(false);

		string[] attackDirs = selectedCardObject.CardData.attackDirections;

		// Set indicatorPointer to start position
		player.IndicatorPointer.localPosition = new Vector3 (0f, 0, 0);

		for (int i = 0; i < attackDirs.Length; i++) {
			if (attackDirs[i] == "forward") {
				player.IndicatorPointer.position += Vector3.forward * player.GetGameManager.gridInfo.GridHeight;
			} else if (attackDirs[i] == "back") {
				player.IndicatorPointer.position += Vector3.back * player.GetGameManager.gridInfo.GridHeight;
			} else if (attackDirs[i] == "left") {
				player.IndicatorPointer.position += Vector3.left * player.GetGameManager.gridInfo.GridWidth;
			} else if (attackDirs[i] == "right") {
				player.IndicatorPointer.position += Vector3.right * player.GetGameManager.gridInfo.GridWidth;
			}
		}
		player.IndicatorPointer.gameObject.SetActive(true);
	}

	private void ResetSelectedCard () {
		selectedCardObject.transform.localScale = Vector3.one * originalScale;
		selectedCardObject.transform.position = new Vector3 (selectedCardObject.transform.position.x, originalYposition, selectedCardObject.transform.position.z);
	}
}
