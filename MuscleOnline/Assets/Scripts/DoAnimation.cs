using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoAnimation : MonoBehaviour
{
    static Animator animator;

    // スタート時に呼ばれる
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    public static void StartAnimation()
    {
        Debug.Log("addCount");
        animator.SetTrigger("isActive");

    }

}
