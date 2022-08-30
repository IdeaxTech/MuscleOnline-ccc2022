using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOperation : MonoBehaviour
{
    [SerializeField] Animator Anim;


    void StartAnimation()
    {
        Anim.SetBool("isActive", true);
    }

    void StopAnimation()
    {
        Anim.SetBool("isActive", false);
    }
}
