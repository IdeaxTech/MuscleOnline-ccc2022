using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PVPResult : MonoBehaviour
{
    [SerializeField] Image ExpImg;

    // Start is called before the first frame update
    void Start()
    {
        GameObject BattleBGM = GameObject.Find("BattleBGM");
        if (BattleBGM)
            Destroy(BattleBGM);

        LevelOperation();


        GameObject.Find("UserLevel").GetComponent<Text>().text = UserInfo.UserLevel.ToString();

        GameObject.Find("Experiencepoint").GetComponent<Text>().text = UserInfo.UserExp.ToString() + "/" + UserInfo.UserMaxExp.ToString();

        GameObject.Find("Player1Name").GetComponent<Text>().text = UserInfo.UserName;

        GameObject Canvas = GameObject.Find("Canvas");

        //自分のカウント
        GameObject MyCount = Canvas.transform.Find("Player1Reps").gameObject;
        int TotalCount = Convert.ToInt32(OperateCostomProperty.GetUserCustomProperty("TotalCount"));
        MyCount.GetComponent<Text>().text = TotalCount.ToString();

        int LocalUserNo = Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]);

        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            int PlayerNum = Convert.ToInt32(player.CustomProperties["PlayerNo"]);

            if (player.IsLocal)
                continue;

            if (LocalUserNo < PlayerNum && LocalUserNo == 1)
                PlayerNum--;


            //GameObject ResultPlayer = Canvas.transform.Find("ResultPlayer" + (PlayerNum+1).ToString()).gameObject;
            //ResultPlayer.SetActive(true);


            GameObject PlayerBackground = Canvas.transform.Find("Player" + (PlayerNum + 1).ToString() + "Background").gameObject;
            PlayerBackground.SetActive(true);

            //各プレイヤーの名前
            GameObject OtherPlayerName = Canvas.transform.Find("Player" + (PlayerNum + 1).ToString() + "Name").gameObject;
            OtherPlayerName.SetActive(true);
            OtherPlayerName.GetComponent<Text>().text = player.NickName;

            // 各プレイヤーのカウント
            GameObject PlayerCount = Canvas.transform.Find("Player" + (PlayerNum + 1).ToString() + "Reps").gameObject;
            PlayerCount.SetActive(true);
            PlayerCount.GetComponent<Text>().text = player.CustomProperties["TotalCount"].ToString();

        }


    }

    async void LevelOperation()
    {
        Dictionary<string, object> QuestReward = (Dictionary<string, object>)Convert.ChangeType(OperateCostomProperty.GetRoomCustomProperty("QuestReward"), typeof(Dictionary<string, object>));
        int RewardExp = (int)Convert.ChangeType(QuestReward["exp"], typeof(int));

        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot ExpData = await db.Collection("required_exp").GetSnapshotAsync();

        while (true)
        {
            int tmp = UserInfo.UserMaxExp - (RewardExp + UserInfo.UserExp);
            if (tmp < 1)
            {
                UserInfo.UserLevel++;

                // TODOどのくらい上げるか
                UserInfo.UserAttack += UnityEngine.Random.Range(0, 3);
                UserInfo.UserHP += UnityEngine.Random.Range(0, 5);
                UserInfo.UserDefence += UnityEngine.Random.Range(0, 3);

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
                UserInfo.UserExp += RewardExp;
                break;
            }


        }
        Dictionary<string, object> SendData = new Dictionary<string, object>()
        {
            {"user_level", UserInfo.UserLevel},
            {"user_exp", UserInfo.UserExp},
            {"user_maxexp", UserInfo.UserMaxExp},
            {"user_hp", UserInfo.UserHP},
            {"user_attack", UserInfo.UserAttack},
            {"user_defence", UserInfo.UserDefence},
            {"update_at", Timestamp.GetCurrentTimestamp()}
        };

        DatabaseOperation.UpdateData("users", UserInfo.UserId, SendData);

        ExpImg.fillAmount = (float)UserInfo.UserExp / UserInfo.UserMaxExp;

    }

}
