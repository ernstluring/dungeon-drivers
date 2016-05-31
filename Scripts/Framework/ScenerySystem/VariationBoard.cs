using UnityEngine;
using System.Collections;

public class VariationBoard : Board {

	void Update () {
		Move();
	}
	
	public override void Move () {
//		base.startPosition = myTransform.position;
//		base.endPosition = base.startPosition + Vector3.back;

		myTransform.Translate(Vector3.back * Time.deltaTime * base.speed, Space.World);

//		transform.position = Vector3.SmoothDamp(base.startPosition, base.endPosition, ref base.velocity, base.smoothTime);
	}
}
