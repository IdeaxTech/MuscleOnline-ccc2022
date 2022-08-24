using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomOperation : MonoBehaviour
{
    byte MaxPlayerPerRoom = 4;

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
        PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions { MaxPlayers = MaxPlayerPerRoom }, TypedLobby.Default);

        // シーン遷移
        Invoke("ToGameScene", 1.5f);
    }


    //- 部屋情報を取得し、リストとして表示
    //- いずれかの部屋をクリックしたらキャラを表示する待機画面へ遷移
    //- 部屋へ参加ボタンを押したら、ボス戦協力待機画面(部屋選択)に遷移
    public void JoinBossBattleRoom()
    {
        //クエストルーム情報をデータベースから取得

        //モーダルを表示

        //モーダル内の要素をクリックしたら次の画面に遷移
        //ルームに入る
        InitialSetting();
        string RoomName = "aaa";
        PhotonNetwork.JoinRoom(RoomName);
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
}
