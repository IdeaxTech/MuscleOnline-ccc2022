using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PVPMain : MonoBehaviour
{
    [SerializeField] GameObject ReadyBtn;
    void Start()
    {

        GameObject.FindWithTag("MyName").GetComponent<TMP_Text>().text = UserInfo.UserName;
        ReadyBtn.SetActive(true);

        GameObject.FindWithTag("MyCount").GetComponent<TMP_Text>().text = OperateCostomProperty.GetUserCustomProperty("Count").ToString();

        int LocalUserNo = Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]);

        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            int PlayerNum = Convert.ToInt32(player.CustomProperties["PlayerNo"]);

            if (player.IsLocal)
                continue;

            if (LocalUserNo < PlayerNum && LocalUserNo == 1)
                PlayerNum--;

            GameObject.FindWithTag("UserCount" + PlayerNum).GetComponent<TMP_Text>().text = player.CustomProperties["Count"].ToString();

        }

        PlayerNo.SetDisplayPlayerNo();
    }
}
