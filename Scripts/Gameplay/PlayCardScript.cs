using UnityEngine;
using System.Collections;
using ExtensionMethods;

public class PlayCardScript : MonoBehaviour {

	private bool playedThisCard = false;

	private Vector3 carPosition;

	private Vector3 poolPosition;

	private float timer;
	private float speed = 20;

	void Update(){
		if(playedThisCard){
			Run ();
		}
	}

	public void Play (Vector3 carPos, Vector3 poolPos) {
		carPosition = carPos;
		poolPosition = poolPos;
		playedThisCard = true;
	}

	private void Run(){
		Vector3 originalScale = this.gameObject.transform.localScale;

		Vector3 targetPos = new Vector3(carPosition.x, carPosition.y+100, carPosition.z);

		timer = 0.1f * Time.deltaTime;

		transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
		transform.localScale = new Vector3 (originalScale.x - timer, originalScale.y - timer, 
		                                    originalScale.z - timer);
		if (transform.localScale.x < 0.01f) {
			transform.localScale = originalScale;
		}
		if ((Vector3.Distance (transform.position, targetPos)) < 0.1f){
			playedThisCard = false;
			transform.position = poolPosition;
			transform.localScale = originalScale;
			gameObject.SetActive(false);
			timer = 0;
		}
	}
}
