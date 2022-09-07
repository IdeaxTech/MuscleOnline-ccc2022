using System;
using System.Collections.Generic;
using System.Threading;
using ExitGames.Client.Photon;
using Firebase.Firestore;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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

    [SerializeField] GameObject TapBtn;
    GameObject tmpobject;

    Animator TrainingAnimator;

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log(propertiesThatChanged);

        if (propertiesThatChanged.TryGetValue("BossHP", out value))
        {
            GameObject.Find("BossHP").GetComponent<TMP_Text>().text = propertiesThatChanged["BossHP"].ToString();
        }

        if (propertiesThatChanged.TryGetValue("TotalHP", out value))
        {
        }

        if (propertiesThatChanged.TryGetValue("BossDefence", out value))
        {
            BossBattleScript.damage = UserInfo.UserAttack - (int)propertiesThatChanged["BossDefence"];
            OperateCostomProperty.SetUserCustomProperty("AttackDamage", BossBattleScript.damage);
        }


        //全員クエストの準備完了したら
        if (propertiesThatChanged.TryGetValue("NumOfReadyPlayers", out value))
        {
            if ((int)propertiesThatChanged["NumOfReadyPlayers"] == PhotonNetwork.CurrentRoom.PlayerCount)
            {

                BossBattleScript.BossBattle();
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.LoadLevel("Loading");

                    Dictionary<string, object> RoomData = new Dictionary<string, object>
                    {
                        { "is_open", false },
                        { "start_time", Timestamp.GetCurrentTimestamp()}
                    };

                    DatabaseOperation.UpdateData("rooms", OperateCostomProperty.GetRoomCustomProperty("RoomId").ToString(), RoomData);                    
                }
                

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
                TrainingCountDown.timeLimit = 5;
                CountDownObject.SetActive(true);

                OperateCostomProperty.SetUserCustomProperty("isTrainingReady", false);
                OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", 0);
            }
        }

        if (propertiesThatChanged.TryGetValue("isBattle", out value))
        {
            if ((bool)propertiesThatChanged["isBattle"])
            {
                int TotalHP = 0;
                foreach (var player in PhotonNetwork.PlayerList)
                {
                    Debug.Log("Add HP!");
                    TotalHP += (int)player.CustomProperties["MyHP"];
                }
                OperateCostomProperty.SetRoomCustomProperty("TotalHP", TotalHP);
                OperateCostomProperty.SetRoomCustomProperty("AllyMaxHP", TotalHP);

                LoadBossBattleScene(2000);

                //ユーザーのターン
                //筋トレの種類を設定
                //BossBattleScript.SetTrainingOption();
                // 筋トレ内容を表示させ、準備をする
                //ReadyBtn.SetActive(true);

            }

            static async void LoadBossBattleScene(int Delay)
            {
                await Task.Delay(Delay);
                SceneManager.LoadScene("BossBattle");

            }

        }

        if (propertiesThatChanged.TryGetValue("isTraining", out value))
        {
            
            if ((bool)propertiesThatChanged["isTraining"])
            {
                // TODOクエストに合わせたトレーニング時間に変更
                TrainingTimer.time = 0f;
                TrainingTimer.timeLimit = 15;
                TimerObject.SetActive(true);
                TapBtn.SetActive(true);
            }
            else
            {
                TapBtn.SetActive(false);
                ////5. カウントが0になったらカウント、攻撃力等を用いてダメージ量を計算
                //BossBattleScript.AllyAttack();

                ////ボスのターン
                //BossBattleScript.BossAttack();

                if ((bool)OperateCostomProperty.GetRoomCustomProperty("isBattle"))
                {

                    BossBattleScript.SetTrainingOption();
                    ReadyBtn.SetActive(true);
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
        GameObject Canvas = GameObject.Find("Canvas");

        if (propertiesThatChanged.TryGetValue("Count", out value))
        {
            if (!targetPlayer.IsLocal)
            {
                int PlayerNum = Convert.ToInt32(targetPlayer.CustomProperties["PlayerNo"]);
                if (PlayerNum > Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]))
                {
                    PlayerNum--;
                }
                GameObject UserCountObj = Canvas.transform.Find("UserCount" + PlayerNum.ToString()).gameObject;
                UserCountObj.SetActive(true);
                GameObject.FindWithTag("UserCount" + PlayerNum).GetComponent<TMP_Text>().text = propertiesThatChanged["Count"].ToString();
                Debug.Log(PlayerNum.ToString() + "が筋トレをしました");

                Animator AvatarAnimation = GameObject.Find("UserAvatar-" + (PlayerNum+1).ToString()).GetComponent<Animator>();
                Debug.Log(AvatarAnimation);
                AvatarAnimation.SetTrigger("isActive");
                //AvatarAnimation.ResetTrigger("isActive");
            }
            else
            {
                GameObject.FindWithTag("MyCount").GetComponent<TMP_Text>().text = propertiesThatChanged["Count"].ToString();
            }

        }

        if (propertiesThatChanged.TryGetValue("isBossBattleReady", out value))
        {
            if (!targetPlayer.IsLocal)
            {
                //そのユーザーのラベルを変更する
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
                    tmpobject.SetActive(true);

                else
                    tmpobject.SetActive(false);
            }
        }

        if (propertiesThatChanged.TryGetValue("isTrainingReady", out value))
        {
        }

        if (propertiesThatChanged.TryGetValue("PlayerNo", out value))
        {
            if (!targetPlayer.IsLocal)
                PlayerNo.SetDisplayPlayerNo();
        }

        if (propertiesThatChanged.TryGetValue("MyHP", out value))
        {
        }
    }

    public override void OnPlayerLeftRoom(Player LeftPlayer)
    {
        Debug.Log(LeftPlayer.NickName + "が退出しました。");
        int PlayerNum = (int)LeftPlayer.CustomProperties["PlayerNo"];
        if (PlayerNum > Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]))
        {
            PlayerNum--;
        }

        //アバターを非表示
        GameObject UserAvatar = GameObject.Find("UserAvatar-" + (PlayerNum + 1).ToString()).gameObject;
        UserAvatar.SetActive(false);

        //プレイヤー名を非表示
        GameObject OtherPlayerName = GameObject.Find("OtherPlayerName" + PlayerNum.ToString()).gameObject;
        OtherPlayerName.SetActive(false);

        //カウントを非表示
        GameObject UserCountObj = GameObject.Find("UserCount" + PlayerNum.ToString()).gameObject;
        UserCountObj.SetActive(false);

        //ユーザー名背景を非表示
        GameObject UserNameBackground = GameObject.Find("UserNameBackground-" + (PlayerNum + 1).ToString()).gameObject;
        UserNameBackground.SetActive(false);



    }
}