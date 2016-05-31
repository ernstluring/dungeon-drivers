using UnityEngine;
using System.Collections;
using InControl;

public class Controls : MonoBehaviour{
	private Player player;
	private Hand hand;

	private void Awake () {
		player = GetComponent<Player>();
	}
	private void Start () {
		hand = player.GetHand;
	}

	public void UpdateControls (InputDevice inputDevice) {
		if (inputDevice == null)
			KeyBoardInput ();
		else {
			ControllerInput (inputDevice);
		}
	}
	
	private void KeyBoardInput () {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit ();

		#region Keyboard Select cards
		if (Input.GetKeyDown(KeyCode.RightArrow) && !hand.CanMove && player.IsAfterStartFlag) {
			SelectCardInHandRight ();
			return;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow) && !hand.CanMove && player.IsAfterStartFlag) {
			SelectCardInHandLeft ();
			return;
		}
		#endregion
		
		#region Keyboard Play card
		if (Input.GetKeyDown(KeyCode.Space) && player.IsActive && !player.CanBurn && !hand.CanMove && player.IsAfterStartFlag) {
			PlayCardFromHand ();
			return;
		}
		#endregion
		
		#region Keyboard Move player
		if (!player.IsMoving && player.IsAfterStartFlag) {
			if (Input.GetKeyDown(KeyCode.D) && hand.CanMove && player.IsActive && !player.BlockedDirections.right && !player.BlockedDirections.right) {
				MovePlayerCar ("right");
				return;
			} else if (Input.GetKeyDown(KeyCode.A) && hand.CanMove && player.IsActive && !player.BlockedDirections.left && !player.BlockedDirections.left) {
				MovePlayerCar ("left");
				return;
			} else if (Input.GetKeyDown(KeyCode.W) && hand.CanMove && player.IsActive && !player.BlockedDirections.forward && !player.BlockedDirections.forward) {
				MovePlayerCar ("forward");
				return;
			} else if (Input.GetKeyDown(KeyCode.S) && hand.CanMove && player.IsActive && !player.BlockedDirections.back && !player.BlockedDirections.back) {
				MovePlayerCar ("back");
				return;
			}
		}
		#endregion
		
		#region Keyboard Burn card
		if (Input.GetKeyDown(KeyCode.B) && player.IsActive && player.CanBurn && player.IsAfterStartFlag) {
			BurnCardInHand ();
			return;
		}
		#endregion
		
		#region Keyboard Examine card
		if (Input.GetKeyDown(KeyCode.X) && player.IsAfterStartFlag) {
			ExamineCardInHand ();
			return;
		}
		#endregion
		
		#region Keyboard End turn
		if (Input.GetKeyDown(KeyCode.T) && player.IsActive && !hand.CanMove && player.IsAfterStartFlag) {
			NextPhase ();
			return;
		}
		#endregion
	}
	
	private void ControllerInput (InputDevice inputDevice) {
		#region Controller Select cards (LeftStick)
		if (inputDevice.LeftStickRight.WasPressed && !hand.CanMove && player.IsAfterStartFlag) {
			SelectCardInHandRight ();
			return;
		} else if (inputDevice.LeftStickLeft.WasPressed && !hand.CanMove && player.IsAfterStartFlag) {
			SelectCardInHandLeft ();
			return;
		}
		#endregion
		
		#region Controller Play Card Button (A)
		if (inputDevice.Action1.WasReleased && player.IsActive && !player.CanBurn && !hand.CanMove && player.IsAfterStartFlag) {
			PlayCardFromHand ();
			return;
		}
		#endregion
		
		#region Controller Move player (DPAD)
		if (!player.IsMoving && player.IsAfterStartFlag) {
			if (inputDevice.DPadRight.WasPressed && hand.CanMove && player.IsActive && !player.BlockedDirections.right) {
				MovePlayerCar ("right");
				return;
			} else if (inputDevice.DPadLeft.WasPressed && hand.CanMove && player.IsActive && !player.BlockedDirections.left) {
				MovePlayerCar ("left");
				return;
			} else if (inputDevice.DPadUp.WasPressed && hand.CanMove && player.IsActive && !player.BlockedDirections.forward) {
				MovePlayerCar ("forward");
				return;
			} else if (inputDevice.DPadDown.WasPressed && hand.CanMove && player.IsActive && !player.BlockedDirections.back) {
				MovePlayerCar ("back");
				return;
			}
		}

		#endregion
		
		#region Controller Burn Card Button (B)
		if (inputDevice.Action2.WasPressed && player.IsActive && player.CanBurn && !hand.CanMove && player.IsAfterStartFlag) {
			BurnCardInHand ();
			return;
		}
		#endregion
		
		#region Controller Examine Card Button (X)
		if (inputDevice.Action3.WasPressed && player.IsAfterStartFlag) {
			ExamineCardInHand ();
			return;
		}
		#endregion
		
		#region Controller End Turn Button (Y)
		if (inputDevice.Action4.WasPressed && player.IsActive && !hand.CanMove && player.IsAfterStartFlag) {
			NextPhase ();
			return;
		}
		#endregion
	}

	private void SelectCardInHandRight () {
		hand.SelectCardRight ();
	}
	private void SelectCardInHandLeft () {
		hand.SelectCardLeft ();
	}
	private void PlayCardFromHand () {
		hand.PlayCard ();
	}
	private void MovePlayerCar (string dir) {
		StartCoroutine (player.Movement (dir));
		return;
	}
	private void BurnCardInHand () {
		hand.BurnCard ();
	}
	private void ExamineCardInHand () {
		hand.ExamineCard ();
	}
	private void NextPhase () {
		player.GetMachine.AdvanceAndRun ();
	}
}
