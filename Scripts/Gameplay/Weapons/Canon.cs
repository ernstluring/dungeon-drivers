using UnityEngine;
using System.Collections;

public class Canon : Weapon {

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
		yield return new WaitForSeconds (0.3f);
		// Play the shoot audio clip
		audioSources[0].Play ();
		yield return new WaitForSeconds (0.5f);
		attackPointer.StartExplosion (player.GetGameManager.attackParticles.explosionObject);
		// Play the impact audio clip
		audioSources[1].Play ();
	}

	public override IEnumerator Deploy (Player player, Card card, AttackPointer attackPointer, Player opponent) {
		Animator playerAnim = player.PlayerAnim;
		card.GetAttack.InitializeAttack (attackPointer, player, opponent, card.attackDirections, card.damage);
		StartCoroutine (base.StartLooking (attackPointer, playerAnim, player));
		yield return null;
	}

	public override void Reset (Animator playerAnim) {
		
	}
}
