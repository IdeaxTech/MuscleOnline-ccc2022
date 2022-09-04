using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerNo : MonoBehaviourPunCallbacks
{
    private const int PlayerUpperLimit = 4 + 1;

    /// <summary>
    /// プレイヤーに番号を与える
    /// </summary>
    public static void SetPlayerNo()
    {
        //自分のクライアントの同期オブジェクトにのみ
        List<int> PlayerSetableCountList = new List<int>();

        //制限人数までの数字のリストを作成
        //例) 制限人数 = 4 の場合、{0,1,2,3}
        int Count = 1;
        for (int i = 1; i < PlayerUpperLimit; i++)
        {
            PlayerSetableCountList.Add(i);
            Count++;
        }

        //他の全プレイヤー取得
        Player[] OtherPlayers = PhotonNetwork.PlayerListOthers;

        //他のプレイヤーがいなければカスタムプロパティの値を"0"に設定
        if (OtherPlayers.Length <= 0)
        {
            //ローカルのプレイヤーのカスタムプロパティを設定
            int PlayerAssignNum = OtherPlayers.Length;
            OperateCostomProperty.SetUserCustomProperty("PlayerNo", 1);
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + "Set PlayerNo.1 Forcibly");
            return;
        }

        Debug.Log(OtherPlayers.Length);
        //他のプレイヤーのカスタムプロパティー取得してリスト作成
        List<int> PlayerAssignNums = new List<int>();
        for (int i = 0; i < OtherPlayers.Length; i++)
        {
            Debug.Log(OtherPlayers[i].NickName + "is " + OtherPlayers[i].CustomProperties["PlayerNo"]);
            PlayerAssignNums.Add((int)OtherPlayers[i].CustomProperties["PlayerNo"]);
        }

        //リスト同士を比較し、未使用の数字のリストを作成
        //例) 0,1にプレーヤーが存在する場合、返すリストは2,3
        PlayerSetableCountList.RemoveAll(PlayerAssignNums.Contains);

        //ローカルのプレイヤーのカスタムプロパティを設定
        //空いている場所のうち、一番若い数字の箇所を利用
        OperateCostomProperty.SetUserCustomProperty("PlayerNo", PlayerSetableCountList[0]);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "Set PlayerNo." + PlayerSetableCountList[0].ToString());
    }

    public static void SetDisplayPlayerNo()
    {
        int LocalUserNo = Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]);

        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            int PlayerNum = Convert.ToInt32(player.CustomProperties["PlayerNo"]);

            if (player.IsLocal)
                continue;

            if (LocalUserNo < PlayerNum && LocalUserNo == 1)
                PlayerNum--;

            Debug.Log("OtherPlayerName" + PlayerNum.ToString());
            GameObject Canvas = GameObject.Find("Canvas");
            GameObject UserAvatar = Canvas.transform.Find("UserAvatar-" + (PlayerNum+1).ToString()).gameObject;
            UserAvatar.SetActive(true);

            
            GameObject OtherPlayerName = Canvas.transform.Find("OtherPlayerName" + PlayerNum.ToString()).gameObject;
            OtherPlayerName.SetActive(true);
            OtherPlayerName.GetComponent<TMP_Text>().text = player.NickName;

            GameObject UserNameBackground = Canvas.transform.Find("UserNameBackground-" + (PlayerNum + 1).ToString()).gameObject;
            UserNameBackground.SetActive(true);
        }
    }
}
