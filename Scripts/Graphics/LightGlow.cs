using UnityEngine;
using System.Collections;

public class LightGlow : MonoBehaviour {

	private Light lightObject;

	[Tooltip ("The speed that determines how fast this light will glow")]
	[Range (0.1f, 10)]
	[SerializeField]
	private float glowSpeed;

	[Tooltip ("The glow value that this light will be at its minimum during glowing")]
	[Range (0, 7)]
	[SerializeField]
	private float minimumGlowValue;

	[Tooltip ("The glow value that this light will be at its maximum during glowing")]
	[Range (1, 8)]
	[SerializeField]
	private float maximumGlowValue = 1;

	private void Start () {
		lightObject = GetComponent<Light>();
	}

	private void Update () {
		lightObject.intensity = PingPong(Time.time * glowSpeed, minimumGlowValue, maximumGlowValue);
	}

	private float PingPong (float value, float min, float max) {
		return Mathf.PingPong(value, max-min) + min;
	}
}
