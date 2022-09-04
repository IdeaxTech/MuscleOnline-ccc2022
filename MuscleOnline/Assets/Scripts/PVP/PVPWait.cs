using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
using Firebase.Firestore;
using System.Threading.Tasks;
using Photon.Realtime;

public class PVPWait : MonoBehaviourPunCallbacks
{
    void Start()
    {
        GameObject.Find("MyName").GetComponent<TMP_Text>().text = UserInfo.UserName;

        //何番目に入ったユーザか
        PlayerNo.SetPlayerNo();
        PlayerNo.SetDisplayPlayerNo();

        Debug.Log("部屋に入りました");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PVPSetting();
            LoadBattleRoom(3000);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("leave room" + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            CloseRoom();
        }
    }

    async void CloseRoom()
    {
        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot RoomData = await db.Collection("rooms").GetSnapshotAsync();

        Dictionary<string, object> UpdateRoomData = new Dictionary<string, object>
            {
                { "is_open", false }
            };
        DatabaseOperation.UpdateData("rooms", RoomOperation.RoomId, UpdateRoomData);
    }

    async void LoadBattleRoom(int Delay)
    {
        await Task.Delay(Delay);
        PhotonNetwork.LoadLevel("BattleRoom");
    }

    void PVPSetting()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", 0);
        }


        //カウントを初期化
        OperateCostomProperty.SetUserCustomProperty("Count", 0);
        OperateCostomProperty.SetUserCustomProperty("TotalCount", 0);

        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PVPSetting();
    }

    }