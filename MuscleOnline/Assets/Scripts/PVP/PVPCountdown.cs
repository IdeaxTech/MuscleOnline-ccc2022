using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVPCountdown : MonoBehaviour
{
    [SerializeField] GameObject CountDownObject;

    public static float timeLimit;
    public static float time;

    public Text timerText;
    float totaltime;


    void Update()
    {
        time += Time.deltaTime;
        float timer = time / timeLimit;
        totaltime = (int)timeLimit - (int)time;

        if (totaltime > 0)
        {
            timerText.text = totaltime.ToString();
        }
        else
        {
            timerText.text = "0";
            this.gameObject.SetActive(false);

            OperateCostomProperty.SetRoomCustomProperty("isTraining", true);

        }
    }
}
