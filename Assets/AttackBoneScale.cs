using UnityEngine;
using System.Collections;

public class AttackBoneScale : StateMachineBehaviour {

    public float animTime;
    public string boneName;
    public float scale;
    public float animSpeed = 1.5f;

    private Transform bone;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bone = animator.GetComponent<Transform>().Find(boneName);
        bone.GetComponent<CapsuleCollider>().enabled = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime < animTime / (2* animSpeed))
        {
            bone.localScale = Vector3.Lerp(bone.localScale, new Vector3(scale, scale, scale), Time.deltaTime);
        }
        else
        {
            bone.localScale = Vector3.Lerp(bone.localScale, new Vector3(1, 1, 1), Time.deltaTime * 4);
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bone.localScale = new Vector3(1, 1, 1);
        bone.GetComponent<CapsuleCollider>().enabled = false;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
