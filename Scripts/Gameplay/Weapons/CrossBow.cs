using UnityEngine;
using System.Collections;

public class CrossBow : Weapon {

	[SerializeField]
	private AudioClip shootAudioClip;
	[SerializeField]
	private AudioClip impactAudioClip;

	private void Start () {
		audioSources[0].clip = shootAudioClip;
		audioSources[1].clip = impactAudioClip;
	}

	public override IEnumerator Shoot (AttackPointer attackPointer, Player player) {
		gunAnim.SetTrigger ("shoot");
		// Play the shoot audio clip
		audioSources[0].Play ();
		yield return new WaitForSeconds (0.6f);
		// Play the impact audio clip
		audioSources[1].Play ();
		yield return new WaitForSeconds (0.2f);
		attackPointer.StartExplosion (player.GetGameManager.attackParticles.explosionObject);
	}

	public override IEnumerator Deploy (Player player, Card card, AttackPointer attackPointer, Player opponent) {
		/* Set the layer weight of the Hatch Layer to 1 and the
		 * bool parameter hatch to true so the hatch animations start playing */
		Animator playerAnim = player.PlayerAnim;
		playerAnim.SetLayerWeight(1,1);
		playerAnim.SetBool("hatch", true);
		yield return new WaitForSeconds (1.5f);
		card.GetAttack.InitializeAttack (attackPointer, player, opponent, card.attackDirections, card.damage);
		StartCoroutine (base.StartLooking (attackPointer, playerAnim, player));
	}

	public override void Reset (Animator playerAnim) {
		playerAnim.SetBool ("hatch", false);
	}
}
