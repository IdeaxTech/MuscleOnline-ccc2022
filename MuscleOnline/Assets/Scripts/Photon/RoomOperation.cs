using System;
using System.Collections.Generic;
using Firebase.Firestore;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RoomOperation : MonoBehaviourPunCallbacks
{
    byte MaxPlayerPerRoom = 4;
    public static string RoomId;

    void Start()
    {
        if (PhotonNetwork.IsConnected == false)
            PhotonNetwork.ConnectUsingSettings();
    }

    //- 部屋の作成ボタンを押したらボス戦協力待機画面(部屋作成)に遷移
    public void CreateBossBattleRoom()
    {
        //ルーム名を取得
        string RoomName = GameObject.FindWithTag("RoomName").GetComponent<TMP_InputField>().text;
        if (RoomName == "")
        {
            Debug.Log("ルーム名が入力されていません");
            return;
        }

        InitialSetting();
        RoomId = RandomPassword.Generate(16);
        //RoomId = RoomName;
        PhotonNetwork.JoinOrCreateRoom(RoomId, new RoomOptions { MaxPlayers = MaxPlayerPerRoom }, TypedLobby.Default);

        
        //OperateCostomProperty.SetRoomCustomProperty("RoomId", RoomId);

        //データベースへの追加
        Dictionary<string, object> RoomData = new Dictionary<string, object>
        {
            { "room_name", RoomName },
            { "max_player", 4 },
            { "now_player", 1 },
            { "quest_id", "1rrPh4Kl8N0U3FYEcPKv"},
            { "is_open", true }
        };
        DatabaseOperation.AddData("rooms", RoomId, RoomData);

        // シーン遷移
        Invoke("ToGameScene", 1.5f);
    }

    public void CreateSoloBossBattleRoom()
    {
        InitialSetting();
        string RoomId = RandomPassword.Generate(16);
        PhotonNetwork.JoinOrCreateRoom(RoomId, new RoomOptions { MaxPlayers = 1, IsOpen = false }, TypedLobby.Default);


        OperateCostomProperty.SetRoomCustomProperty("RoomId", RoomId);

        //データベースへの追加
        Dictionary<string, object> RoomData = new Dictionary<string, object>
        {
            { "max_player", 1 },
            { "now_player", 1 },
            { "quest_id", "1rrPh4Kl8N0U3FYEcPKv"},
            { "is_open", false }
        };
        DatabaseOperation.AddData("rooms", RoomId, RoomData);

        // シーン遷移
        Invoke("ToGameScene", 1.5f);
    }

    //- 部屋情報を取得し、リストとして表示
    //- いずれかの部屋をクリックしたらキャラを表示する待機画面へ遷移
    //- 部屋へ参加ボタンを押したら、ボス戦協力待機画面(部屋選択)に遷移
    public void JoinBossBattleRoom(BaseEventData data)
    {
        GameObject PointerObject = (data as PointerEventData).pointerClick;

        GameObject RoomId = PointerObject.transform.Find("RoomId").gameObject;
        Debug.Log(RoomId.GetComponent<TMP_Text>().text);
        //モーダル内の要素をクリックしたら次の画面に遷移
        //ルームに入る
        InitialSetting();

        //string RoomName = "aaa";
        //PhotonNetwork.JoinRoom(RoomName);

         PhotonNetwork.JoinRoom(RoomId.GetComponent<TMP_Text>().text);
    }

    void InitialSetting()
    {

        //２画面でアクセスしていて、その両方で実行してしまっている。　→　masterだけ呼び出すように変更
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("NumOfPlayer", PhotonNetwork.CurrentRoom.PlayerCount);
        }

        PhotonNetwork.NickName = UserInfo.UserName;
        OperateCostomProperty.SetUserCustomProperty("isBossBattleReady", false);
    }

    void ToGameScene()
    {
        SceneManager.LoadScene("QuestWait");
    }

    public async void JoinRandomMatch()
    {
        string id = "JERcM6p8i8kvqYKTUna5";
        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot RoomData = await db.Collection("rooms").GetSnapshotAsync();
        foreach (var document in RoomData.Documents)
        {
            Dictionary<string, object> DictionaryData = document.ToDictionary();
            if (DictionaryData["quest_id"].ToString() == id)
            {
                if ((bool)Convert.ChangeType(DictionaryData["is_open"], typeof(bool)) == true)
                {
                    //レート戦の場合ここでレートの上限下限を設定する
                    Debug.Log(document.Id);
                    PhotonNetwork.JoinRoom(document.Id.ToString());

                    Dictionary<string, object> UpdateRoomData = new Dictionary<string, object>
                    {
                        { "now_player", 2 },
                        { "is_open", false }
                    };
                    DatabaseOperation.UpdateData("rooms", document.Id, UpdateRoomData);
                    Invoke("ToRandomMatch", 1.5f);

                    return;

                }

            }
        }

        //ルームが見つからなかった場合
        InitialSetting();
        RoomId = RandomPassword.Generate(16);
        PhotonNetwork.JoinOrCreateRoom(RoomId, new RoomOptions { MaxPlayers = MaxPlayerPerRoom }, TypedLobby.Default);


        

        //データベースへの追加
        Dictionary<string, object> AddRoomData = new Dictionary<string, object>
        {
            { "max_player", 2 },
            { "now_player", 1 },
            { "quest_id", id},
            { "is_open", true }
        };

        DatabaseOperation.AddData("rooms", RoomId, AddRoomData);

        // シーン遷移
        Invoke("ToRandomMatch", 1.5f);
    }

    void ToRandomMatch()
    {
        SceneManager.LoadScene("BattleWait");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("RoomId", RoomId);
        }
    }
}
