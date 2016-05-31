using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour {

	protected List<AudioSource> audioSources = new List<AudioSource>();

	protected Animator gunAnim;
	public Animator GunAnim { get { return gunAnim; } }

	protected Quaternion startRot;
	protected float timer;
	protected float speed = 2.5f;


	void Awake () {
		startRot = transform.localRotation;
		AudioSource[] a = GetComponents<AudioSource>();
		for (int i = 0; i < a.Length; i++) {
			audioSources.Add (a[i]);
		}
		gunAnim = GetComponentInParent<Animator>();
	}

	protected IEnumerator StartLooking (AttackPointer target, Animator playerAnim, Player player) {
		Quaternion targetRot = Quaternion.LookRotation(target.transform.position - transform.position);

		while (Quaternion.Angle(transform.rotation, targetRot) > 0.1f) {
			timer += Time.deltaTime * speed;
			transform.rotation = Quaternion.Slerp(startRot, targetRot, timer);
			yield return new WaitForEndOfFrame();
		}

		yield return StartCoroutine (Shoot (target, player));

		timer = 0;
		while (Quaternion.Angle(transform.rotation, startRot) > 0.1f) {
			timer += Time.deltaTime * speed;
			transform.rotation = Quaternion.Slerp(targetRot, startRot, timer);
			yield return new WaitForEndOfFrame();
		}
		timer = 0;

		Reset (playerAnim);
	}

	public abstract IEnumerator Shoot (AttackPointer attackPointer, Player player);

	public abstract IEnumerator Deploy (Player player, Card card, AttackPointer attackPointer, Player opponent);
	public abstract void Reset (Animator playerAnim);
}
