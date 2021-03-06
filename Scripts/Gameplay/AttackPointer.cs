﻿using UnityEngine;
using System.Collections;

public class AttackPointer : MonoBehaviour {

	[SerializeField]
	private Player target;

	private int damage;
	private BoxCollider boxCollider;

	private void Awake () {
		boxCollider = GetComponent<BoxCollider>();

		DisableCollider ();

		if (target == null) {
			Debug.LogError ("Target of " + this.name + " is null, it needs to be set in the inspector");
		}
	}

	public void StartExplosion (GameObject g) {
		StartCoroutine (PlayExplosion (g));
	}

	private IEnumerator PlayExplosion (GameObject g) {
		GameObject go = Instantiate(g, transform.position, Quaternion.identity) as GameObject;
		EnableCollider ();
		yield return new WaitForSeconds (3f);
		DisableCollider ();
		Destroy (go);
		transform.localPosition = Vector3.zero;
	}

	public void EnableCollider () {
		boxCollider.enabled = true;
	}

	public void DisableCollider () {
		boxCollider.enabled = false;
	}

	public void SetDamage (int damageAmount) {
		damage = damageAmount;
	}

	private void OnTriggerEnter (Collider col) {
		if (col.name == target.name) {
			// If collides with opponent
			target.ReceiveDamage (damage, true);
		}
	}
}
