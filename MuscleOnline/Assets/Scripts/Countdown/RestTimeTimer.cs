using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestTimeTimer : MonoBehaviour
{
    [SerializeField] GameObject ReadyBtn;
    [SerializeField] GameObject RestTimeObject;
    public static float timeLimit;
    public static float time;

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

            RestTimeObject.SetActive(false);

            //ユーザーのターン
            //筋トレの種類を設定
            BossBattleScript.SetTrainingOption();


            // 筋トレ内容を表示させ、準備をする
            ReadyBtn.SetActive(true);
        }


        clock.UpdateClock(timer);
    }
}
