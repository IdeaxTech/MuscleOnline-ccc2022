using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SetRoomName : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindWithTag("RoomName").GetComponent<TMP_Text>().text = PhotonNetwork.CurrentRoom.Name;
        GameObject.FindWithTag("MyName").GetComponent<TMP_Text>().text = PhotonNetwork.NickName;
    }
}
