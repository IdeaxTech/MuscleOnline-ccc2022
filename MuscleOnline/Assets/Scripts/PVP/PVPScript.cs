using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPScript : MonoBehaviour
{
    [SerializeField] GameObject CountDownObject;

    void StartBattle()
    {
        TrainingCountDown.time = 0f;
        TrainingCountDown.timeLimit = 9;
        CountDownObject.SetActive(true);
    }
}


