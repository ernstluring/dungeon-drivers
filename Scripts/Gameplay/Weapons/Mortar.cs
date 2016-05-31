using UnityEngine;
using System.Collections;

public class Mortar : Weapon {

	[SerializeField]
	private AudioClip shootAudioClip;
	[SerializeField]
	private AudioClip impactAudioClip;
	[SerializeField]
	private GameObject canonBal;
	private Vector3 topPoint = Vector3.up * 10;
	private Vector3 canonBallOriginalPos;

	private void Start () {
		audioSources[0].clip = shootAudioClip;
		audioSources[1].clip = impactAudioClip;
		canonBallOriginalPos = canonBal.transform.localPosition;
	}

	public override IEnumerator Shoot (AttackPointer attackPointer, Player player) {
		gunAnim.SetTrigger ("shoot");

		yield return new WaitForSeconds (1.5f);
		// Play the shoot audio clip
		audioSources[0].Play ();

		while (canonBal.transform.position.y < topPoint.y) {
			canonBal.transform.Translate(Vector3.up * Time.deltaTime * 40, Space.World);
			yield return new WaitForEndOfFrame ();
		}
		// Play the impact audio clip
		audioSources[1].Play ();
		yield return new WaitForSeconds (1.5f);
		attackPointer.StartExplosion (player.GetGameManager.attackParticles.explosionObject);

		canonBal.transform.localPosition = canonBallOriginalPos;
	}

	public override IEnumerator Deploy (Player player, Card card, AttackPointer attackPointer, Player opponent) {
		card.GetAttack.InitializeAttack (attackPointer, player, opponent, card.attackDirections, card.damage);
		StartCoroutine (Shoot (attackPointer, player));
		yield return null;
	}

	public override void Reset (Animator playerAnim) {
		
	}
}
