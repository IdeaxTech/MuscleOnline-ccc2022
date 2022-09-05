using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            int MyCount = (int)OperateCostomProperty.GetUserCustomProperty("Count");
            int OtherCount = 0;
            foreach(var player in PhotonNetwork.PlayerListOthers)
            {
                OtherCount = (int)player.CustomProperties["Count"];
            }

            if (MyCount > OtherCount)
            {
                Debug.Log("勝利しました");
                SceneManager.LoadScene("BattleResultWin");
            } else if (OtherCount > MyCount)
            {
                Debug.Log("敗北しました");
                SceneManager.LoadScene("BattleResultLose");
            } else
            {
                Debug.Log("引き分けでした");
                PhotonNetwork.LoadLevel("BattleResult");
            }
        }
        clock.UpdateClock(timer);
    }
}
