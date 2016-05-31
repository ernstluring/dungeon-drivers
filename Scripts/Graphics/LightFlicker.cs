using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	private Light lightObject;

	[Tooltip ("The speed that determines how fast this light will glow")]
	[Range (0.1f, 200)]
	[SerializeField]
	private float glowSpeed;

	[Tooltip ("The glow value that this light will be at its minimum during glowing")]
	[Range (0, 7)]
	[SerializeField]
	private float minimumIntensityValue;

	[Tooltip ("The glow value that this light will be at its maximum during glowing")]
	[Range (0.1f, 3)]
	[SerializeField]
	private float addedIntensity = 0.1f;

	private float randomValue;

	private float currentLerpTime;

	private const float LERPTIME = 1f;
	
	private void Start () {
		lightObject = GetComponent<Light>();
		StartCoroutine( GetRandomValue());
	}

	private IEnumerator GetRandomValue () {
		while (true) {
			randomValue = Random.Range(minimumIntensityValue, minimumIntensityValue + addedIntensity);

			LerpToValue ();

			yield return new WaitForSeconds(0.08f);
		}
	}

	private void LerpToValue () {
		currentLerpTime = 0;
		currentLerpTime += Time.deltaTime * glowSpeed;
		if (currentLerpTime > LERPTIME)
			currentLerpTime = LERPTIME;
		float value = currentLerpTime / LERPTIME;
		lightObject.intensity = Mathf.Lerp(lightObject.intensity, randomValue, value);
	}
}
