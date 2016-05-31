using UnityEngine;
using System.Collections;

public class ObjectRotation : MonoBehaviour {

	[SerializeField]
	private float speed = 250;
	public float Speed {
		get { return speed; }
		set { speed = value; }
	}

	private enum RotateDirection {
		RIGHT,
		LEFT,
		UP,
		DOWN,
		FORWARD,
		BACK
	}

	[SerializeField]
	private RotateDirection rotationDirection = RotateDirection.RIGHT;

	private Vector3 rotationVector;

	private void OnEnable () {
		switch (rotationDirection) {
		case RotateDirection.RIGHT:
			rotationVector = Vector3.up;
			break;
		case RotateDirection.LEFT:
			rotationVector = Vector3.down;
			break;
		case RotateDirection.UP:
			rotationVector = Vector3.forward;
			break;
		case RotateDirection.DOWN:
			rotationVector = Vector3.back;
			break;
		case RotateDirection.FORWARD:
			rotationVector = Vector3.right;
			break;
		case RotateDirection.BACK:
			rotationVector = Vector3.left;
			break;
		}
	}

	private void Update () {
		transform.Rotate(rotationVector, speed * Time.deltaTime, Space.Self);
	}
}