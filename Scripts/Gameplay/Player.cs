using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;

public enum PlayerType { DWARF, GOBLIN }

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
[RequireComponent(typeof(Hand), typeof(Controls), typeof(PlayerMovement))]
public class Player : MonoBehaviour {

	[SerializeField]
	private PlayerType playerType;
	public PlayerType GetPlayerType {
		get { return playerType; }
	}

	[System.Serializable]
	public struct CarParts {
		public GameObject[] weapons;
		public GameObject[] wheels;
		public int activeWeapon;
		public int activeWheels;
	}
	[SerializeField]
	private CarParts carParts;
	public CarParts GetCarParts {
		get { return carParts; }
	}

	[SerializeField]
	private ParticleSystem damageVehicleSmokeParticle;
	[SerializeField]
	private ParticleSystem healParticle;

	[SerializeField]
	private CardsManager cardsManager;
	public CardsManager GetCardsManager {
		get { return cardsManager; }
	}

	[SerializeField]
	private GameManager gameManager;
	public GameManager GetGameManager {
		get { return gameManager; }
	}

	[SerializeField]
	private AudioManager audioManager;
	public AudioManager GetAudioManager {
		get { return audioManager; }
	}

	private Weapon weapon;
	public Weapon GetWeapon {
		get { return weapon; }
	}

	public Text phaseText;
	public PhaseIndicator phaseIndicator;

	public GameObject WinModel;
	public AudioClip winSound;

	public bool IsAfterStartFlag { get; private set; }

	[SerializeField]
	private Animator gunAnim;
	public Animator GunAnim {
		get { return gunAnim; }
	}

	[SerializeField]
	private AttackPointer attackPointer;

	[SerializeField]
	private Transform indicatorPointer;
	public Transform IndicatorPointer { 
		get { return indicatorPointer; }
	}

	[SerializeField]
	private GameplayStateMachineRunner machine;

	public GameplayStateMachine GetMachine {
		get { return machine.Machine; }
	}

	[SerializeField]
	private HealthCounter healthCounter;

	[SerializeField]
	private ParticleSystem actionPoint01;
	[SerializeField]
	private ParticleSystem actionPoint02;

	[SerializeField]
	private int playerNumber;
	public int PlayerNumber {
		get { return playerNumber; }
	}
	
	[SerializeField]
	private Camera handCamera;
	public Camera HandCamera {
		get { return handCamera; }
	}

	private int healthAmount = 20;

	private bool isActive = false;
	public bool IsActive {
		get { return isActive; }
	}

	private bool canBurn = false;
	public bool CanBurn {
		get { return canBurn; }
	}

	private int actionPointCounter = 2;
	private int burnCounter = 2;

	private Player opponent;
	public Player Opponent {
		get { return opponent; }
	}

	private Hand hand;
	public Hand GetHand {
		get { return hand; }
	}

	private InputDevice inputDevice;
	public InputDevice GetInputDevice {
		get { return inputDevice; }
	}
	
	private AudioSource audioSource;
	public AudioSource GetAudioSource {
		get { return audioSource; }
	}

	private Animator playerAnim;
	public Animator PlayerAnim {
		get { return playerAnim; }
	}

	private Controls controlInput;
	private PlayerMovement playerMovement;

	private int movementCount;

	private BoxCollider boxCol;

	private bool isOnEdge;
	private Quaternion originalRot;

	private bool defeated = false;

	public BaseStatusEffect boardStatusEffect;

	private bool isMoving = false;
	public bool IsMoving {
		get { return isMoving; }
	}

	[SerializeField]
	private LayerMask gridLayerMask;

	private struct EscapeDirections {
		public string escapeFromLeft, escapeFromRight, escapeFromFront, escapeFromBack;
	}
	public struct BlockMoveDirections {
		/// <summary>
		/// The blocked directions:
		/// true = blocked
		/// false = free
		/// </summary>
		public bool forward, back, left, right;

		/// <summary>
		/// Setting all directions to free
		/// </summary>
		public void SetAllDirectionsFree () {
			forward = false;
			back = false;
			left = false;
			right = false;
		}
	}

	private EscapeDirections escapeDirections;
	private BlockMoveDirections blockedDirections;
	public BlockMoveDirections BlockedDirections {
		get { return blockedDirections; }
	}

	[SerializeField]
	private AnimationCurve animCrashCurve;
	private float crashTimer;
	private Vector3 crashPos;

	private void Awake () {
		controlInput = GetComponent<Controls>();
		hand = GetComponent<Hand>();
		playerMovement = GetComponent<PlayerMovement>();
		playerAnim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		boxCol = GetComponent<BoxCollider>();
		originalRot = transform.rotation;

		if (healthCounter == null)
			healthCounter = GetComponentInChildren<HealthCounter>();
		if (attackPointer == null)
			attackPointer = GetComponentInChildren<AttackPointer>();
	}

	private void Start () {
		indicatorPointer.gameObject.SetActive(false);
		IsAfterStartFlag = false;

		SetCarParts ();
	}

	private void SetCarParts () {
		int chosenWheels = 0;
		int chosenWeapon = 0;
		switch (playerType) {
		case PlayerType.DWARF:
			chosenWheels = PlayerPrefs.GetInt ("dwarf_wheels");
			chosenWeapon = PlayerPrefs.GetInt ("dwarf_weapon");
			break;
		case PlayerType.GOBLIN:
			chosenWheels = PlayerPrefs.GetInt ("goblin_wheels");
			chosenWeapon = PlayerPrefs.GetInt ("goblin_weapon");
			break;
		}
		carParts.activeWeapon = chosenWeapon;
		carParts.activeWheels = chosenWheels;

		// Wheels check
		if (chosenWheels == 0) {
			for (int i = 0; i < carParts.wheels.Length; i++) {
				if (carParts.wheels[i].CompareTag("Wheels01"))
					carParts.wheels[i].SetActive (true);
				else
					carParts.wheels[i].SetActive (false);
			}
		} else if (chosenWheels == 1) {
			for (int i = 0; i < carParts.wheels.Length; i++) {
				if (carParts.wheels[i].CompareTag("Wheels02"))
					carParts.wheels[i].SetActive (true);
				else
					carParts.wheels[i].SetActive (false);
			}
		}

		// Weapons check
		if (chosenWeapon == 0) {
			for (int i = 0; i < carParts.weapons.Length; i++) {
				Weapon w = carParts.weapons[i].GetComponentInChildren<Weapon>();
				if (w is Mortar || w is CrossBow) {
					weapon = w;
				} else {
					carParts.weapons[i].gameObject.SetActive (false);
				}
			}
		} else if (chosenWeapon == 1) {
			for (int i = 0; i < carParts.weapons.Length; i++) {
				Weapon w = carParts.weapons[i].GetComponentInChildren<Weapon>();
				if (w is Canon || w is Flamethrower) {
					weapon = w;
				} else  {
					carParts.weapons[i].gameObject.SetActive (false);
				}
			}
		}
	}
	
	private void Update () {
		inputDevice = (InputManager.Devices.Count > playerNumber) ? InputManager.Devices[playerNumber] : null;
		controlInput.UpdateControls(inputDevice);

		#if UNITY_EDITOR
		if (playerType == PlayerType.GOBLIN) {
			if (Input.GetKeyDown(KeyCode.K)) {
				healthAmount = 0;
			} else if (Input.GetKeyDown(KeyCode.H)) {
				Heal (1);
			}
		}
		#endif

		GridCollisionCheck ();

		// Defeated
		if (healthAmount <= 0 && !defeated) {
			audioManager.PlayLoseClip (playerType);
			playerAnim.SetTrigger("death");
			defeated = true;

			audioSource.clip = winSound;
			audioSource.Play ();
			opponent.WinModel.SetActive (true);
		}
		if (defeated) {
			crashTimer += Time.deltaTime;
			if (crashPos == Vector3.zero) {
				crashPos = transform.position;
			}
			transform.position = new Vector3 (crashPos.x, crashPos.y, crashPos.z+animCrashCurve.Evaluate(crashTimer));
		}
	}

	public void ActionPointUsed () {
		if (actionPoint01.emissionRate != 0) {
			actionPoint01.emissionRate = 0;
		} else {
			actionPoint02.emissionRate = 0;
		}
		actionPointCounter--;
		if (actionPointCounter <= 0) {
			actionPointCounter = 0;
			machine.Machine.AdvanceAndRun ();
		}
	}

	public void BurnPointUsed () {
		burnCounter--;
		if (burnCounter <= 0) {
			burnCounter = 0;
			canBurn = false;
			GetMachine.AdvanceAndRun ();
		}
	}

	public void SetCanBurn (bool canPlayerBurn) {
		canBurn = canPlayerBurn;
	}

	public void SetOpponent (Player opponent) {
		this.opponent = opponent;
	}

	private void ResetStats () {
		actionPointCounter = 2;
		burnCounter = 2;
		actionPoint01.emissionRate = 25;
		actionPoint02.emissionRate = 25;
	}

	public void SetPlayerActive () {
		isActive = true;
		ResetStats ();
	}

	public void SetPlayerPassive () {
		isActive = false;
		actionPoint01.emissionRate = 0;
		actionPoint02.emissionRate = 0;
	}

	public IEnumerator Movement (string inputDir) {
		if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || playerAnim.GetCurrentAnimatorStateInfo(0).IsName("dangerzonerumble1")
			|| playerAnim.GetCurrentAnimatorStateInfo(0).IsName("dangerzonerumble2")) {
			if (hand.GetMovementCount > 0) {
				isMoving = true;
				playerMovement.InitMovement (inputDir);
				yield return StartCoroutine (playerMovement.SmoothMove ());
				hand.DecreaseMovementCounter ();

				// For cards with 0 movement / >0 damage or >0 movement / 0 damage
				if (hand.GetMovementCount <= 0 && hand.GetAttackCardFromHand().damage > 0) {
					yield return StartCoroutine (Attack());
				} else if (hand.GetMovementCount <= 0 && hand.GetAttackCardFromHand().damage <= 0){
					hand.NextCardAfterMovement ();
				}
				/* ================================================================================ */
			} else if (hand.GetMovementCount <= 0 && hand.GetAttackCardFromHand().damage > 0) {
				yield return StartCoroutine (Attack());
			}
			yield return isMoving = false;
		}
	}

	private IEnumerator Attack () {
		audioManager.PlayAttackClip (playerType);
		Card card = hand.GetAttackCardFromHand();
		yield return StartCoroutine (weapon.Deploy (this, card, attackPointer, opponent));
		yield return new WaitForSeconds (1);
		hand.NextCardAfterMovement ();
	}

	public void ReceiveDamage (int amount, bool damageIsFromOpponent) {
		if (amount > 0 && healthAmount > 0) {
			StartCoroutine (healthCounter.RollHealth (amount, true));
			playerAnim.SetTrigger("takedamage");
			healthAmount -= amount;
			if (damageIsFromOpponent)
				audioManager.PlayGetDamageClips(playerType, healthAmount);
			else
				audioManager.PlayDangerZoneClip(playerType);

			ParticleSystem p = Instantiate (damageVehicleSmokeParticle) as ParticleSystem;
			p.transform.SetParent (transform);
			p.transform.localPosition = Vector3.zero;
			p.Play();
		}
	}

	public void Heal (int amount) {
		if (amount > 0 && healthAmount < 20) {
			StartCoroutine (healthCounter.RollHealth (amount, false));
			healthAmount += amount;
			audioManager.PlayPullPositionClip (playerType);

			ParticleSystem p = Instantiate (healParticle) as ParticleSystem;
			p.transform.position = this.transform.position;
			p.Play();
		}
	}

	private void GridCollisionCheck () {
		RaycastHit hit;
		Vector3 start = new Vector3 (transform.position.x, 0f, transform.position.z);
		if (Physics.Raycast (start, Vector3.down, out hit, 1, gridLayerMask.value)) {
			TileType tileType = hit.transform.GetComponent<GridTile>().GetTileType;
			switch (tileType) {
			case TileType.TILE:
				isOnEdge = false;
				blockedDirections.SetAllDirectionsFree ();
				if (transform.rotation != originalRot)
					transform.rotation = Quaternion.Lerp (transform.rotation, originalRot, 5*Time.deltaTime);
				if (boardStatusEffect != null)
					boardStatusEffect = null;
				break;
			case TileType.PULLPOSLEFT:
				isOnEdge = true;
				blockedDirections.forward = true;
				blockedDirections.back = false;
				blockedDirections.right = false;
				blockedDirections.left = false;

				if (transform.rotation != originalRot)
					transform.rotation = Quaternion.Lerp (transform.rotation, originalRot, 5*Time.deltaTime);
				if (!(boardStatusEffect is HealPerTurn))
					boardStatusEffect = new HealPerTurn (this);

				escapeDirections.escapeFromBack = "left";
				escapeDirections.escapeFromRight = "left";
				escapeDirections.escapeFromLeft = "right";
				break;
			case TileType.PULLPOSMIDDLE:
				isOnEdge = true;
				blockedDirections.forward = true;
				blockedDirections.back = false;
				blockedDirections.right = false;
				blockedDirections.left = false;

				if (transform.rotation != originalRot)
					transform.rotation = Quaternion.Lerp (transform.rotation, originalRot, 5*Time.deltaTime);
				if (!(boardStatusEffect is HealPerTurn))
					boardStatusEffect = new HealPerTurn (this);

				escapeDirections.escapeFromBack = "left";
				escapeDirections.escapeFromRight = "left";
				escapeDirections.escapeFromLeft = "right";
				break;
			case TileType.PULLPOSRIGHT:
				isOnEdge = true;
				blockedDirections.forward = true;
				blockedDirections.back = false;
				blockedDirections.right = false;
				blockedDirections.left = false;

				if (transform.rotation != originalRot)
					transform.rotation = Quaternion.Lerp (transform.rotation, originalRot, 5*Time.deltaTime);
				if (!(boardStatusEffect is HealPerTurn))
					boardStatusEffect = new HealPerTurn (this);

				escapeDirections.escapeFromBack = "left";
				escapeDirections.escapeFromRight = "left";
				escapeDirections.escapeFromLeft = "right";
				break;
			case TileType.BACKLEFT:
				isOnEdge = true;
				blockedDirections.forward = false;
				blockedDirections.back = true;
				blockedDirections.right = false;
				blockedDirections.left = false;

				if (transform.rotation != originalRot)
					transform.rotation = Quaternion.Lerp (transform.rotation, originalRot, 5*Time.deltaTime);
				if (boardStatusEffect != null)
					boardStatusEffect = null;

				escapeDirections.escapeFromFront = "left";
				escapeDirections.escapeFromRight = "left";
				escapeDirections.escapeFromLeft = "right";
				break;
			case TileType.BACKMIDDLE:
				isOnEdge = true;
				blockedDirections.forward = false;
				blockedDirections.back = true;
				blockedDirections.right = false;
				blockedDirections.left = false;

				if (transform.rotation != originalRot)
					transform.rotation = Quaternion.Lerp (transform.rotation, originalRot, 5*Time.deltaTime);
				if (boardStatusEffect != null)
					boardStatusEffect = null;

				escapeDirections.escapeFromFront = "left";
				escapeDirections.escapeFromRight = "left";
				escapeDirections.escapeFromLeft = "right";
				break;
			case TileType.BACKRIGHT:
				isOnEdge = true;
				blockedDirections.forward = false;
				blockedDirections.back = true;
				blockedDirections.right = false;
				blockedDirections.left = false;

				if (transform.rotation != originalRot)
					transform.rotation = Quaternion.Lerp (transform.rotation, originalRot, 5*Time.deltaTime);
				if (boardStatusEffect != null)
					boardStatusEffect = null;

				escapeDirections.escapeFromFront = "left";
				escapeDirections.escapeFromRight = "left";
				escapeDirections.escapeFromLeft = "right";
				break;
			case TileType.DITCHRIGHTFRONT:
				isOnEdge = true;

				blockedDirections.forward = true;
				blockedDirections.back = false;
				blockedDirections.right = true;
				blockedDirections.left = false;

				if (!(boardStatusEffect is DamagePerTurn))
					boardStatusEffect = new DamagePerTurn (this);

				escapeDirections.escapeFromLeft = "back";
				escapeDirections.escapeFromBack = "left";
				break;
			case TileType.DITCHRIGHTMIDDLE:
				isOnEdge = true;

				blockedDirections.forward = false;
				blockedDirections.back = false;
				blockedDirections.right = true;
				blockedDirections.left = false;

				if (!(boardStatusEffect is DamagePerTurn))
					boardStatusEffect = new DamagePerTurn (this);

				escapeDirections.escapeFromBack = "forward";
				escapeDirections.escapeFromFront = "back";
				escapeDirections.escapeFromLeft = "back";
				break;
			case TileType.DITCHRIGHTBACK:
				isOnEdge = true;

				blockedDirections.forward = false;
				blockedDirections.back = true;
				blockedDirections.right = true;
				blockedDirections.left = false;

				if (!(boardStatusEffect is DamagePerTurn))
					boardStatusEffect = new DamagePerTurn (this);

				escapeDirections.escapeFromFront = "left";
				escapeDirections.escapeFromLeft = "forward";
				break;
			case TileType.DITCHLEFTFRONT:
				isOnEdge = true;

				blockedDirections.forward = true;
				blockedDirections.back = false;
				blockedDirections.right = false;
				blockedDirections.left = true;

				if (!(boardStatusEffect is DamagePerTurn))
					boardStatusEffect = new DamagePerTurn (this);

				escapeDirections.escapeFromRight = "back";
				escapeDirections.escapeFromBack = "right";
				break;
			case TileType.DITCHLEFTMIDDLE:
				isOnEdge = true;

				blockedDirections.forward = false;
				blockedDirections.back = false;
				blockedDirections.right = false;
				blockedDirections.left = true;

				if (!(boardStatusEffect is DamagePerTurn))
					boardStatusEffect = new DamagePerTurn (this);

				escapeDirections.escapeFromBack = "forward";
				escapeDirections.escapeFromFront = "back";
				escapeDirections.escapeFromRight = "back";
				break;
			case TileType.DITCHLEFTBACK:
				isOnEdge = true;

				blockedDirections.forward = false;
				blockedDirections.back = true;
				blockedDirections.right = false;
				blockedDirections.left = true;

				if (!(boardStatusEffect is DamagePerTurn))
					boardStatusEffect = new DamagePerTurn (this);

				escapeDirections.escapeFromFront = "right";
				escapeDirections.escapeFromRight = "forward";
				break;
			}

			// Change the rotation of the player when he drives in a ditch to the slope angle of the ditch
			if (tileType == TileType.DITCHLEFTBACK || tileType == TileType.DITCHLEFTFRONT || tileType == TileType.DITCHLEFTMIDDLE ||
				tileType == TileType.DITCHRIGHTBACK || tileType == TileType.DITCHRIGHTFRONT || tileType == TileType.DITCHRIGHTMIDDLE) {
				Quaternion toRot = hit.transform.localRotation;
				toRot.z = -toRot.z;
				transform.rotation = Quaternion.Lerp (transform.rotation, toRot, 5f*Time.deltaTime);
				playerAnim.SetLayerWeight (2, 0.5f);
			} else {
				if (playerAnim.GetLayerWeight(2) != 0) {
					playerAnim.SetLayerWeight (2, Mathf.Lerp(playerAnim.GetLayerWeight(2), 0, Time.deltaTime * 5));
				}
			}
		}
	}

	private void OnTriggerEnter (Collider c) {
		// If this is the passive player and collides with the active player
		if (!isActive) {
			if (c.name == opponent.name) {
				boxCol.enabled = false;
				// vector that points from target pos to the player pos;
				Vector3 dirVector = (transform.position - c.transform.position).normalized;
				if (Mathf.Abs (dirVector.z) < 0.5f) {
					if (dirVector.x > 0) {
						if (isOnEdge) {

							playerMovement.InitMovement (escapeDirections.escapeFromLeft);
							StartCoroutine (playerMovement.SmoothMove ());
						} else {
							playerMovement.InitMovement ("right");
							StartCoroutine (playerMovement.SmoothMove ());
						}
					} else if (dirVector.x < 0) {
						if (isOnEdge) {
							playerMovement.InitMovement (escapeDirections.escapeFromRight);
							StartCoroutine (playerMovement.SmoothMove ());
						} else {
							playerMovement.InitMovement ("left");
							StartCoroutine (playerMovement.SmoothMove ());
						}
					}
				} else {
					if (dirVector.z > 0) {
						if (isOnEdge) {
							playerMovement.InitMovement (escapeDirections.escapeFromBack);
							StartCoroutine (playerMovement.SmoothMove ());
						} else {
							playerMovement.InitMovement ("forward");
							StartCoroutine (playerMovement.SmoothMove ());
						}
					} else if (dirVector.z < 0) {
						if (isOnEdge) {
							playerMovement.InitMovement (escapeDirections.escapeFromFront);
							StartCoroutine (playerMovement.SmoothMove ());
						} else {
							playerMovement.InitMovement ("back");
							StartCoroutine (playerMovement.SmoothMove ());
						}
					}
				}
			}
		}

		if (c.name == "StartFlag") {
			handCamera.enabled = true;
			indicatorPointer.gameObject.SetActive(true);
			hand.ScaleSelectedCard ();
			IsAfterStartFlag = true;
		}
	}
}
