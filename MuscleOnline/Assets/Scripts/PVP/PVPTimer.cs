using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PVPTimer : MonoBehaviour
{
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

            //トレーニングタイマーの場合
            //OperateCostomProperty.SetRoomCustomProperty("isTraining", false);
            PhotonNetwork.LoadLevel("BattleResult");
        }
        clock.UpdateClock(timer);
    }
}
