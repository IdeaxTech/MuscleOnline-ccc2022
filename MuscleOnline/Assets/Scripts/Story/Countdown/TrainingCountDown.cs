using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingCountDown : MonoBehaviour
{
    [SerializeField] GameObject CountDownObject;
    public static float timeLimit;
    public static float time;

    //[SerializeField] Clock clock;
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

            //トレーニング開始カウントダウンの場合
            BossBattleScript.StartTraining();
            CountDownObject.SetActive(false);
        }


        //clock.UpdateClock(timer);
    }
}
