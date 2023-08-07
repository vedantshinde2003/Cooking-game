using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomizer : StateMachineBehaviour
{
    public int number_of_options;
    public float waitingTime = 1;

    private int anim_index;

    private bool invoke = false;
    public bool do_it = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim_index = Random.Range(0,number_of_options);
    }

   

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitingTime -= Time.deltaTime;
        if (waitingTime < 0)
        {
            if(!invoke)
            {
                animator.SetTrigger(anim_index.ToString());
                 invoke = true;
            }
           
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         invoke = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
