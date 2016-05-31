using UnityEngine;
using System.Collections;

public class Flamethrower : Weapon {

	[SerializeField]
	private AudioClip shootAudioClip;
	[SerializeField]
	private AudioClip impactAudioClip;
	[SerializeField]
	private GameObject fireObject;
	private ParticleSystem[] fireParticles;
//	private ParticleSystem fireParticle;


	private void Start () {
		fireObject.SetActive (false);

		audioSources[0].clip = shootAudioClip;
		audioSources[1].clip = impactAudioClip;

		fireParticles = GetComponentsInChildren<ParticleSystem>();
//		fireParticle = GetComponentInChildren<ParticleSystem>();
	}

	public override IEnumerator Shoot (AttackPointer attackPointer, Player player) {
		gunAnim.SetTrigger ("shoot");

		fireObject.SetActive (true);

		for (int i = 0; i < fireParticles.Length; i++) {
			fireParticles[i].Play();
		}
//		fireParticle.Play ();

		yield return new WaitForSeconds (0.04f);
		// Play the shoot audio clip
		audioSources[0].Play ();
		yield return new WaitForSeconds (1.5f);
		attackPointer.StartExplosion (player.GetGameManager.attackParticles.explosionObject);
		yield return new WaitForSeconds (1.3f);
		// Play the impact audio clip
		audioSources[1].Play ();

		fireObject.SetActive (false);
		for (int i = 0; i < fireParticles.Length; i++) {
			fireParticles[i].Stop ();
		}
//		fireParticle.Stop ();
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
