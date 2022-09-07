using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMOperation : MonoBehaviour
{
    void Start()
    {
        GameObject BossWaitBGM = GameObject.Find("BossWaitBGM");
        if (BossWaitBGM)
            Destroy(BossWaitBGM);
    }
}
