using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownOperation : MonoBehaviour
{
    [SerializeField] GameObject CountDownObject;
    public void StartCountDown(float time)
    {
        CountDown.timeLimit = time;
        CountDownObject.SetActive(true);
    }

    public void StopCountDown()
    {
        CountDownObject.SetActive(false);
    }
}
