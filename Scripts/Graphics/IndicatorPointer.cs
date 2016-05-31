using UnityEngine;
using System.Collections;

public class IndicatorPointer : MonoBehaviour {

	Quaternion originalRot;
	float originalY;

	private void Start () {
		originalRot = transform.rotation;
		originalY = transform.position.y;
	}

	void Update () {
		transform.rotation = originalRot;
		Vector3 temp = transform.position;
		temp.y = originalY;
		transform.position = temp;
	}
}
