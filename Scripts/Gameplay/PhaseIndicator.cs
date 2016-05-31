using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhaseIndicator : MonoBehaviour {

	private Vector3 startPos;
	private Vector3 endPos;
	private float timer;

	private Text displayText;
	private Light light;

	[SerializeField]
	private float speed = 0.1f;

	private void Start () {
		startPos = transform.localPosition;
		endPos = new Vector3 (startPos.x, 1.0f, startPos.z);
		displayText = GetComponentInChildren<Text>();
		light = GetComponentInChildren<Light>();
	}

	public void DoTransition () {
		if (light) light.enabled = true;
		StartCoroutine (Transition());
	}

	public void SetText (string text) {
		displayText.text = text;
	}

	private IEnumerator Transition () {
		while (Vector3.Distance(transform.localPosition, endPos) > 0.01f) {
			timer += Time.deltaTime * speed;
			transform.localPosition = Vector3.Lerp(transform.localPosition, endPos, timer);
			yield return null;
		}
		timer = 0;
		yield return new WaitForSeconds(1);
		while (Vector3.Distance(transform.localPosition, startPos) > 0.01f) {
			timer += Time.deltaTime * speed;
			transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, timer);
			yield return null;
		}
		if (light) light.enabled = false;
	}
}

