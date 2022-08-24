using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MultiBossWaitRoomScript : MonoBehaviourPunCallbacks
{

    byte MaxPlayerPerRoom = 4;
    [SerializeField] GameObject BossBattleReadyBtn0;

    private void Awake()
    {
        ConnectPhoton();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //- ログイン中ユーザの情報を表示
    //- photonサーバーに接続し、ルームに参加
    void ConnectPhoton()
    {
        //Photonへ接続する -- 1
        if (PhotonNetwork.IsConnected == false)
            PhotonNetwork.ConnectUsingSettings();
        
    }

    //OKボタンを押したらユーザーカスタムプロパティを変更
    public void isBossBattleReady()
    {
        if ((bool)OperateCostomProperty.GetUserCustomProperty("isBossBattleReady") == false)
        {
            OperateCostomProperty.SetUserCustomProperty("isBossBattleReady", true);
            //そのユーザーのボタンのテキストを変更する
            GameObject.FindWithTag("BossBattleReadyBtn").GetComponentInChildren<Text>().text = "Cancel";
            BossBattleReadyBtn0.SetActive(true);

            int NumOfReadyPlayers = (int)OperateCostomProperty.GetRoomCustomProperty("NumOfReadyPlayers") + 1;
            OperateCostomProperty.SetRoomCustomProperty("NumOfReadyPlayers", NumOfReadyPlayers);

        }
        else
        {
            BossBattleReadyBtn0.SetActive(false);
            OperateCostomProperty.SetUserCustomProperty("isBossBattleReady", false);
            //そのユーザーのボタンのテキストを変更する
            GameObject.FindWithTag("BossBattleReadyBtn").GetComponentInChildren<Text>().text = "Ready";
            OperateCostomProperty.SetRoomCustomProperty("NumOfReadyPlayers", (int)(object)OperateCostomProperty.GetRoomCustomProperty("NumOfReadyPlayers") - 1);
        }



    }

    public void ForceStartBossBattle()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("BossBattle");
        }
    }

     // Photonサーバーへの接続が成功したら呼ばれる
    public override void OnConnectedToMaster()
    {
        Debug.Log("接続に成功しました。");
    }

    // Photonサーバーへの接続が失敗したら呼ばれる
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}の理由で繋げませんでした。");
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("NumOfReadyPlayers", 0);
            // 強制開始ボタンを表示
        }

        //何番目に入ったユーザか
        PlayerNo.SetPlayerNo();
        PlayerNo.SetDisplayPlayerNo();
    }

    //他のプレイヤーがルームに入ったときに呼ばれる　自身が入ったときには呼ばれない
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
            {
                //これ以上人が入るのを防ぐ
                Debug.Log(PhotonNetwork.NickName + "がルームに参加しました。");
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }


}
