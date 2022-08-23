using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    [SerializeField] float timeLimit;
    float time = 0f;

    [SerializeField] Clock clock;
    public Text timerText;
    float totaltime;

    void Start()
    {
        //timerText = GetComponent<Text>();
        //timerText.text = timeLimit.ToString();
        //Debug.Log(1);

    }


    void Update()
    {

        time += Time.deltaTime;
        float timer = time / timeLimit;
        Debug.Log(timer);

        
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


    //float _updateTimer()
    //{
    //    //経過時間の取得
    //    time += Time.deltaTime;

    //    //Fill Amountは0から1の範囲で指定した割合を画像表示
    //    float timer = time / timeLimit;
    //    Debug.Log(timer);

    //    return timer;

    //}
}
