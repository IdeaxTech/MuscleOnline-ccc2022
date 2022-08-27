using System;
using System.Threading;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCustomProperty : MonoBehaviourPunCallbacks
{
    object value = null;
    private static readonly Hashtable propsToSet = new Hashtable();

    public static double StartTime;
    [SerializeField] GameObject ReadyBtn;
    [SerializeField] GameObject BossBattleReadyBtn1;
    [SerializeField] GameObject BossBattleReadyBtn2;
    [SerializeField] GameObject BossBattleReadyBtn3;
    [SerializeField] GameObject TimerObject;
    [SerializeField] GameObject CountDownObject;
    [SerializeField] GameObject RestTimeObject;
    GameObject tmpobject;


    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log(propertiesThatChanged);

        if (propertiesThatChanged.TryGetValue("BossHP", out value))
        {
            GameObject.FindWithTag("BossHP").GetComponent<TMP_Text>().text = propertiesThatChanged["BossHP"].ToString();
            Debug.Log("BossHP: " + propertiesThatChanged["BossHP"]);

            //ボスが倒れたらゲームを終了
            //if ((int)OperateCostomProperty.GetRoomCustomProperty("BossHP") <= 0)
            //{
            //    if (PhotonNetwork.IsMasterClient)
            //    {
            //        OperateCostomProperty.SetRoomCustomProperty("isBattle", false);
            //        Debug.Log("勝利しました");
            //    }
            //}

        }

        if (propertiesThatChanged.TryGetValue("TotalHP", out value))
        {
            GameObject.FindWithTag("TotalHP").GetComponent<TMP_Text>().text = propertiesThatChanged["TotalHP"].ToString();
            Debug.Log("TotalHP: " + propertiesThatChanged["TotalHP"]);

            if (PhotonNetwork.IsMasterClient)
            {
                if (propertiesThatChanged.TryGetValue("isBattle", out value))
                {
                    //HPが0になったら敗北
                    if ((int)OperateCostomProperty.GetRoomCustomProperty("TotalHP") <= 0)
                    {
                        OperateCostomProperty.SetRoomCustomProperty("isBattle", false);
                        Debug.Log("敗北しました");
                    }
                }

            }

        }

        if (propertiesThatChanged.TryGetValue("BossDefence", out value))
        {
            BossBattleScript.damage = UserInfo.UserAttack - (int)propertiesThatChanged["BossDefence"];
        }


        //全員クエストの準備完了したら
        if (propertiesThatChanged.TryGetValue("NumOfReadyPlayers", out value))
        {
            if ((int)propertiesThatChanged["NumOfReadyPlayers"] == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                if (PhotonNetwork.IsMasterClient)
                    // シーン遷移
                    PhotonNetwork.LoadLevel("BossBattle");
            }
        }

        //全員がトレーニング内容を確認し、トレーニングの準備完了したら
        if (propertiesThatChanged.TryGetValue("isTrainingReady", out value))
        {
            if ((int)propertiesThatChanged["isTrainingReady"] == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                GameObject.FindWithTag("isTrainingReadyBtn").GetComponentInChildren<Text>().text = "Ready";
                ReadyBtn.SetActive(false);

                //トレーニング前カウントダウン
                TrainingCountDown.time = 0f;
                TrainingCountDown.timeLimit = 9;
                CountDownObject.SetActive(true);

                OperateCostomProperty.SetUserCustomProperty("isTrainingReady", false);
                OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", 0);
            }
        }

        if (propertiesThatChanged.TryGetValue("isBattle", out value))
        {
            if ((bool)propertiesThatChanged["isBattle"])
            {
                //ユーザーのターン
                //筋トレの種類を設定
                BossBattleScript.SetTrainingOption();
                // 筋トレ内容を表示させ、準備をする
                ReadyBtn.SetActive(true);

            }
            else
            {

            }

        }

        if (propertiesThatChanged.TryGetValue("isTraining", out value))
        {
            
            if ((bool)propertiesThatChanged["isTraining"])
            {
                // TODOクエストに合わせたトレーニング時間に変更
                TrainingTimer.time = 0f;
                TrainingTimer.timeLimit = 10;
                TimerObject.SetActive(true);
            }
            else
            {
                //5. カウントが0になったらカウント、攻撃力等を用いてダメージ量を計算
                BossBattleScript.AllyAttack();

                //ボスのターン
                BossBattleScript.BossAttack();

                if ((bool)OperateCostomProperty.GetRoomCustomProperty("isBattle"))
                {
                    //TODOデバッグ用
                    RestTimeTimer.time = 0f;
                    RestTimeTimer.timeLimit = 10;
                    RestTimeObject.SetActive(true);
                }
            }
        }

        if (propertiesThatChanged.TryGetValue("StartTime", out value))
        {

            StartTime = (double)propertiesThatChanged["StartTime"];
        }

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log(propertiesThatChanged);
        if (propertiesThatChanged.TryGetValue("Count", out value))
        {
            Debug.Log($"{targetPlayer.NickName}のカウントが{propertiesThatChanged["count"]}になりました。");
            if (!targetPlayer.IsLocal)
            {
                int player_num = Convert.ToInt32(targetPlayer.CustomProperties["PlayerNo"]);
                if (player_num > Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]))
                {
                    player_num--;
                }

                GameObject.FindWithTag("UserCount" + player_num).GetComponent<TMP_Text>().text = targetPlayer.NickName + " : " + propertiesThatChanged["Count"];
            }
            else
            {
                GameObject.FindWithTag("MyCount").GetComponent<TMP_Text>().text = "MyCount : " + propertiesThatChanged["Count"];
            }

        }

        if (propertiesThatChanged.TryGetValue("isBossBattleReady", out value))
        {
            if (!targetPlayer.IsLocal)
            {
                //そのユーザーのラベルを変更する
                Debug.Log("TargetPlayer is " + targetPlayer.CustomProperties["PlayerNo"]);
                Debug.Log("localnumber" + PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"].ToString());
                int player_num = Convert.ToInt32(targetPlayer.CustomProperties["PlayerNo"]);
                if (player_num > Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]))
                {
                    player_num--;
                }

                if (player_num == 1)
                {
                    tmpobject = BossBattleReadyBtn1;
                }
                else if (player_num == 2)
                {
                    tmpobject = BossBattleReadyBtn2;
                }
                else if (player_num == 3)
                {
                    tmpobject = BossBattleReadyBtn3;
                }

                bool is_ready = (bool)targetPlayer.CustomProperties["isBossBattleReady"];


                if (is_ready)
                {
                    //GameObject.FindWithTag("BossBattleReadyBtn" + player_num).GetComponentInChildren<TMP_Text>().text = "Ready";
                    tmpobject.SetActive(true);

                }
                else
                {
                    //GameObject.FindWithTag("BossBattleReadyBtn" + player_num).GetComponentInChildren<TMP_Text>().text = "Preparation";
                    tmpobject.SetActive(false);
                }

            }
        }

        if (propertiesThatChanged.TryGetValue("isTrainingReady", out value))
        {
            Debug.Log("トレーニング変更情報を受け取りました");
        }

        if (propertiesThatChanged.TryGetValue("PlayerNo", out value))
        {
            if (!targetPlayer.IsLocal)
                PlayerNo.SetDisplayPlayerNo();
        }

        if (propertiesThatChanged.TryGetValue("MyHP", out value))
        {
            Debug.Log("SetHP");
            Hashtable roomhash = PhotonNetwork.CurrentRoom.CustomProperties;
            if (!roomhash.TryGetValue("TotalHP", out value))
            {

                propsToSet.Add("TotalHP", (int)propertiesThatChanged["MyHP"]);
                PhotonNetwork.CurrentRoom.SetCustomProperties(propsToSet);
                propsToSet.Clear();
            }
            else
            {
                OperateCostomProperty.SetRoomCustomProperty("TotalHP", (int)OperateCostomProperty.GetRoomCustomProperty("TotalHP") + (int)propertiesThatChanged["MyHP"]);

            }
        }
    }
}