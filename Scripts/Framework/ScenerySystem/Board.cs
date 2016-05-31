using UnityEngine;
using System.Collections;

public abstract class Board : MonoBehaviour {
	//Lower = faster, higher = slower
	protected float smoothTime = 0.13f;
	protected Transform myTransform;
	protected float speed = 7f;

	protected Vector3 startPosition, endPosition;

	protected Vector3 velocity;

	protected virtual void Start () {
		myTransform = transform;
	}

	public abstract void Move ();
}
