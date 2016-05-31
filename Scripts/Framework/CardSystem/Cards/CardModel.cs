using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CardModel : MonoBehaviour {

	private Card cardData;

	private MeshRenderer meshRenderer;

	[SerializeField]
	private Text cardnameText, descriptionText, damageAmountText, movementAmountText;
	[SerializeField]
	private Image[] attackDirectionArrows;
	[SerializeField]
	private Material artMat;

	public enum CardRaceType {
		DWARF,
		GOBLIN
	}
	[SerializeField]
	private CardRaceType cardRaceType;
	private string texturePath;

	public Card CardData {
		get { return cardData; }
		set { if (cardData == null) cardData = value; }
	}

	private void Awake () {
		meshRenderer = GetComponent<MeshRenderer>();
		switch (cardRaceType) {
		case CardRaceType.DWARF:
			texturePath = "dwarf/dwarf_";
			break;
		case CardRaceType.GOBLIN:
			texturePath = "goblin/goblin_";
			break;
		}
	}

	public void FillCardFields () {

		cardnameText.text = cardData.name;
		descriptionText.text = cardData.description;
		damageAmountText.text = cardData.damage.ToString();

		movementAmountText.text = cardData.movementCount.ToString();

		if (cardData.attackDirections.Length > 0) {
			for (int i = 0; i < cardData.attackDirections.Length; i++) {
				string dir = cardData.attackDirections[i];
				if (dir != "") {
					attackDirectionArrows[i].sprite = Resources.Load<Sprite>("attackDir_"+dir);
					attackDirectionArrows[i].enabled = true;
				}
			}
		}
		SetNewMaterial();
	}

	private void SetNewMaterial () {
		Material newMat = new Material(artMat);
		newMat.mainTexture = Resources.Load<Texture>(texturePath+cardData.art);

		Material[] materials = meshRenderer.materials;
		Array.Resize(ref materials, materials.Length+1);
		materials[materials.Length-1] = newMat;
		meshRenderer.materials = materials;
	}
}
