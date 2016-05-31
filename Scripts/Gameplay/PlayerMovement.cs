using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	private Player player;

	private Animator anim;
	
	private List<ObjectRotation> wheels = new List<ObjectRotation>();

	[SerializeField]
	private float moveSpeed = 5f;

	private float gridWidth;
	private float gridHeight;

	private float gridSize;

	private Vector3 moveDir = Vector3.zero;

	private string animationString;

	private Vector3 input, startPosition, endPosition;

	private float t;

	private void Awake () {
		anim = GetComponent<Animator>();
		player = GetComponent<Player>();
	}

	private void Start () {
//		gridWidth = GameManager.Instance.gridInfo.GridWidth;
//		gridHeight = GameManager.Instance.gridInfo.GridHeight;
		gridWidth = player.GetGameManager.gridInfo.GridWidth;
		gridHeight = player.GetGameManager.gridInfo.GridHeight;

//		foreach (ObjectRotation c in GetComponentsInChildren<ObjectRotation>()) {
//			if (c.gameObject.activeInHierarchy)
//				wheels.Add (c);
//		}
	}

	public void InitMovement (string direction) {
		if (direction == "right") {
			moveDir = Vector3.right;
			gridSize = gridWidth;
			animationString = "right";
		} else if (direction == "left") {
			moveDir = Vector3.left;
			gridSize = gridWidth;
			animationString = "left";
		} else if (direction == "forward") {
			moveDir = Vector3.forward;
			gridSize = gridHeight;
			animationString = "forward";
		} else if (direction == "back") {
			moveDir = Vector3.back;
			gridSize = gridHeight;
			animationString = "back";
			foreach (ObjectRotation w in wheels) {
				w.Speed = 200;
			}
		} else {
			Debug.LogError("The filled in string parameter is not supported!" + " : " + direction);
		}
	}

	public IEnumerator SmoothMove () {
		t = 0;
		startPosition = transform.position;
		endPosition = new Vector3 (startPosition.x + moveDir.x * gridSize, startPosition.y, startPosition.z + moveDir.z * gridSize);

		anim.SetTrigger(animationString);

		while (Vector3.Distance(startPosition, endPosition) > 0.1f) {
			t += Time.deltaTime * (moveSpeed/gridSize);
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			startPosition = transform.position;
			yield return null;
		}
//		if (wheels[0].Speed < 250) {
//			foreach (ObjectRotation w in wheels) {
//				w.Speed = 250;
//			}
//		}
		yield return new WaitForSeconds (0.5f);
		GetComponent<BoxCollider>().enabled = true;
	}
}
