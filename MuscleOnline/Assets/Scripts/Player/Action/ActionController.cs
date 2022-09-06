using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ActionController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void LowAttack()
    {

        animator.SetTrigger("LowAttack");

    }

    public void HeavyAttack()
    {

        animator.SetTrigger("HeavyAttack");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("attack");
    }
}
