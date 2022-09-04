using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PVPChangeCustomProperty : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject ReadyBtn;
    object value = null;

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log(propertiesThatChanged);

        if (propertiesThatChanged.TryGetValue("isTraining", out value))
        {

            if ((bool)propertiesThatChanged["isTraining"])
            {
                // TODOクエストに合わせたトレーニング時間に変更
                TrainingTimer.time = 0f;
                TrainingTimer.timeLimit = 10;
                //TimerObject.SetActive(true);
            }
            else
            {

                //シーン遷移
                PhotonNetwork.LoadLevel("PVPResult");
            }
        }

        if (propertiesThatChanged.TryGetValue("isBattle", out value))
        {
            if ((bool)propertiesThatChanged["isBattle"])
            {
                SceneManager.LoadScene("BossBattle");

            }
        }

        if (propertiesThatChanged.TryGetValue("isTrainingReady", out value))
        {
            if ((int)propertiesThatChanged["isTrainingReady"] == 2)
            {
                GameObject.FindWithTag("isTrainingReadyBtn").GetComponentInChildren<Text>().text = "Ready";
                ReadyBtn.SetActive(false);

                OperateCostomProperty.SetUserCustomProperty("isTrainingReady", false);

                if (PhotonNetwork.IsMasterClient)
                    OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", 0);

                //トレーニング前カウントダウン
                //シーン遷移
                PhotonNetwork.LoadLevel("BattleRoom");
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log(propertiesThatChanged);
        if (propertiesThatChanged.TryGetValue("Count", out value))
        {
            if (!targetPlayer.IsLocal)
            {
                int player_num = Convert.ToInt32(targetPlayer.CustomProperties["PlayerNo"]);
                if (player_num > Convert.ToInt32(PhotonNetwork.LocalPlayer.CustomProperties["PlayerNo"]))
                {
                    player_num--;
                }

                GameObject.FindWithTag("UserCount" + player_num).GetComponent<TMP_Text>().text = targetPlayer.NickName + " : " + propertiesThatChanged["Count"];
            }
            else
            {
                GameObject.FindWithTag("MyCount").GetComponent<TMP_Text>().text = "MyCount : " + propertiesThatChanged["Count"];
            }

        }

        if (propertiesThatChanged.TryGetValue("PlayerNo", out value))
        {
            if (!targetPlayer.IsLocal)
                PlayerNo.SetDisplayPlayerNo();
        }

        if (propertiesThatChanged.TryGetValue("isTrainingReady", out value))
        {
            if (!targetPlayer.IsLocal)
            {
                //そのユーザーのラベルを変更する
                GameObject Canvas = GameObject.Find("Canvas");
                GameObject OKLavel = Canvas.transform.Find("OtherPlayerOK").gameObject;
                bool is_ready = (bool)targetPlayer.CustomProperties["isBossBattleReady"];


                if (is_ready)
                {
                    OKLavel.SetActive(true);

                }
                else
                {
                    OKLavel.SetActive(false);
                }

            }
        }
    }
}
