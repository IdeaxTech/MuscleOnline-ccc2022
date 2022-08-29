using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;
using Firebase.Firestore;
using System.Threading.Tasks;

public class QuestResult : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {


        //レベル
        //Task task = Task.Run(() =>
        //{
        //    return LevelOperation();
        //});

        //Task.WhenAll(task);
        //LevelOperation();
        //経験値
        Dictionary<string, object> QuestReward = (Dictionary<string, object>)Convert.ChangeType(OperateCostomProperty.GetRoomCustomProperty("QuestReward"), typeof(Dictionary<string, object>));
        Debug.Log(QuestReward);
        Debug.Log(QuestReward["exp"]);
        int RewardExp = (int)Convert.ChangeType(QuestReward["exp"], typeof(int));
        Debug.Log(RewardExp);

        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot ExpData = await db.Collection("required_exp").GetSnapshotAsync();
        //QuerySnapshot ExpData = (QuerySnapshot)Convert.ChangeType(db.Collection("required_exp").GetSnapshotAsync(), typeof(QuerySnapshot));

        while (true)
        {
            int tmp = UserInfo.UserMaxExp - (RewardExp + UserInfo.UserExp);
            if (tmp < 1)
            {
                UserInfo.UserLevel++;

                // TODOどのくらい上げるか
                UserInfo.UserAttack += 5;
                UserInfo.UserHP += 10;
                UserInfo.UserDefence += 3;

                RewardExp = (RewardExp + UserInfo.UserExp) - UserInfo.UserMaxExp;
                UserInfo.UserExp = 0;

                foreach (var document in ExpData.Documents)
                {
                    Dictionary<string, object> DictionaryData = document.ToDictionary();
                    if (document.Id.Equals("level"))
                    {
                        UserInfo.UserMaxExp = (int)Convert.ChangeType(DictionaryData[UserInfo.UserLevel.ToString()], typeof(int));
                    }
                }
            }
            else
            {
                UserInfo.UserExp = RewardExp;
                break;
            }


        }
        Debug.Log("Level :" + UserInfo.UserLevel);
        Debug.Log("Exp :" + UserInfo.UserExp);
        Debug.Log("MaxExp :" + UserInfo.UserMaxExp); 

        GameObject.Find("UserLevel").GetComponent<TMP_Text>().text = UserInfo.UserLevel.ToString();

        GameObject.Find("MaxExp").GetComponent<TMP_Text>().text = UserInfo.UserMaxExp.ToString();
        GameObject.Find("NowExp").GetComponent<TMP_Text>().text = UserInfo.UserExp.ToString();

        GameObject.Find("MyName").GetComponent<TMP_Text>().text = UserInfo.UserName;

        GameObject Canvas = GameObject.Find("Canvas");

        //自分のカウント
        GameObject MyCount = Canvas.transform.Find("MyCount").gameObject;
        int TotalCount = Convert.ToInt32(OperateCostomProperty.GetUserCustomProperty("TotalCount"));
        MyCount.GetComponent<TMP_Text>().text = TotalCount.ToString();

        //自分のトータルダメージ
        GameObject MyTotalDamage = GameObject.Find("MyTotalDamage").gameObject;
        MyTotalDamage.GetComponent<TMP_Text>().text = (TotalCount * BossBattleScript.damage).ToString();


        int LocalUserNo = Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]);

        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            int PlayerNum = Convert.ToInt32(player.CustomProperties["PlayerNo"]);

            if (player.IsLocal)
                continue;

            if (LocalUserNo < PlayerNum && LocalUserNo == 1)
                PlayerNum--;

            
            

            //各プレイヤーの名前
            GameObject OtherPlayerName = Canvas.transform.Find("OtherPlayerName-" + PlayerNum.ToString()).gameObject;
            OtherPlayerName.SetActive(true);
            OtherPlayerName.GetComponent<TMP_Text>().text = player.NickName;

            // 各プレイヤーのカウント
            GameObject PlayerCount = Canvas.transform.Find("PlayerCount-" + PlayerNum.ToString()).gameObject;
            PlayerCount.SetActive(true);
            PlayerCount.GetComponent<TMP_Text>().text = player.CustomProperties["TotalCount"].ToString();

            // 各プレイヤーのトータルダメージ
            GameObject PlayerTotalDamage = Canvas.transform.Find("PlayerTotalDamage-" + PlayerNum.ToString()).gameObject;
            PlayerTotalDamage.SetActive(true);
            PlayerTotalDamage.GetComponent<TMP_Text>().text = (Convert.ToInt32(player.CustomProperties["TotalCount"]) * Convert.ToInt32(player.CustomProperties["AttackDamage"])).ToString();

        }


    }

    async Task<int> LevelOperation()
    {
        //経験値
        Dictionary<string, object> QuestReward = (Dictionary<string, object>)Convert.ChangeType(OperateCostomProperty.GetRoomCustomProperty("QuestReward"), typeof(Dictionary<string, object>));
        int RewardExp = (int)QuestReward["exp"];
        Debug.Log(RewardExp);

        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot ExpData = await db.Collection("required_exp").GetSnapshotAsync();
        while (true)
        {
            int tmp = UserInfo.UserMaxExp - (RewardExp + UserInfo.UserExp);
            if (tmp < 1)
            {
                UserInfo.UserLevel++;

                // TODOどのくらい上げるか
                UserInfo.UserAttack += 5;
                UserInfo.UserHP += 10;
                UserInfo.UserDefence += 3;

                RewardExp = (RewardExp + UserInfo.UserExp) - UserInfo.UserMaxExp;
                UserInfo.UserExp = 0;

                foreach (var document in ExpData.Documents)
                {
                    Dictionary<string, object> DictionaryData = document.ToDictionary();
                    if (document.Id.Equals("level"))
                    {
                        UserInfo.UserMaxExp = (int)Convert.ChangeType(DictionaryData[UserInfo.UserLevel.ToString()], typeof(int));
                    }
                }
            }
            else
            {
                break;
            }


        }
        return 1;
    }

}
