using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerResetSMB : StateMachineBehaviour
{
    [SerializeField] string triggerName;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(triggerName);    
    }


}
