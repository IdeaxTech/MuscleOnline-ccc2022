using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;

public class QuestResult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //経験値

        //レベル


        GameObject.Find("UserLevel").GetComponent<TMP_Text>().text = UserInfo.UserLevel.ToString();
        GameObject.Find("MaxExp").GetComponent<TMP_Text>().text = UserInfo.UserLevel.ToString();
        GameObject.Find("NowExp").GetComponent<TMP_Text>().text = UserInfo.UserLevel.ToString();

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

}
