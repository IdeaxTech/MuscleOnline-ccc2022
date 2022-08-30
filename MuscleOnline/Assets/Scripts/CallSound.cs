using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSound : MonoBehaviour
{
    [SerializeField] AudioClip ClickSound;
    [SerializeField] AudioSource Source;

    public void ClickedBtn()
    {
        Source.PlayOneShot(ClickSound);
    }

}
