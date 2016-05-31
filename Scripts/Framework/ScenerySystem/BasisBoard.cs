using UnityEngine;
using System.Collections;

public class BasisBoard : Board {

	void Update () {
		Move();
	}

	public override void Move () {
		myTransform.Translate(Vector3.back * Time.deltaTime * base.speed, Space.World);
	}
}
