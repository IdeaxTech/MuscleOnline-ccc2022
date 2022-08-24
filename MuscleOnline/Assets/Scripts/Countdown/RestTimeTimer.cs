using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestTimeTimer : MonoBehaviour
{
    [SerializeField] GameObject CountDownObject;
    public static float timeLimit;
    float time = 0f;

    [SerializeField] Clock clock;
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


        }


        clock.UpdateClock(timer);
    }
}
