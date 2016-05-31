using UnityEngine;
using System.Collections;

public class HatchOpenRumble : StateMachineBehaviour {
	private Animator gunAnim;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!gunAnim)
			gunAnim = animator.GetComponent<Player>().GetWeapon.GunAnim;

		gunAnim.SetBool("deploy", true);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		gunAnim.SetBool("deploy", false);
	}
}
