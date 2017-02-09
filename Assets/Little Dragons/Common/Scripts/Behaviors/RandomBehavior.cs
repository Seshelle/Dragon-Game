using UnityEngine;
using System.Collections;

public class RandomBehavior : StateMachineBehaviour {

    public int Range;
    
	override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){

        int result = Random.Range(0, Range);
        animator.SetInteger("DragoInt", result);
        //If is in idle, start to count , to get to sleep
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            
            animator.SetInteger("Tired", animator.GetComponent<DragonController>().Tired++);
            //Debug.Log(animator.GetComponent<DragonController>().Tired);
            if (animator.GetComponent<DragonController>().Tired >= animator.GetComponent<DragonController>().GotoSleep - 5)
            {
                //animator.SetInteger("DragoInt", 5);
            }
            if (animator.GetComponent<DragonController>().Tired >= animator.GetComponent<DragonController>().GotoSleep)
            {
                animator.SetInteger("DragoInt", -1);
                animator.GetComponent<DragonController>().Tired=0;
            }
          
        }
    }

}
