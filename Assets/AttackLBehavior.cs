using UnityEngine;
using System.Collections;

public class AttackLBehavior : StateMachineBehaviour {
    public float animTime = 1.067f;

    private Transform forearmL;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        forearmL = animator.GetComponent<Transform>().Find("CG/Pelvis/Spine/Spine1/Neck/L Clavicle/L UpperArm/L Forearm");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime < animTime / 3)
        {
            forearmL.localScale = Vector3.Lerp(forearmL.localScale, new Vector3(2, 2, 2), Time.deltaTime);
        }
        else
        {
            forearmL.localScale = Vector3.Lerp(forearmL.localScale, new Vector3(1, 1, 1), Time.deltaTime * 4);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        forearmL.localScale = new Vector3(1, 1, 1);
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
