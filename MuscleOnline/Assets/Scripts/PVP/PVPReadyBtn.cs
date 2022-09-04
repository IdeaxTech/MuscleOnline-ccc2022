using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVPReadyBtn : MonoBehaviour
{
    public void ReadyTraining()
    {
        GameObject Canvas = GameObject.Find("Canvas");
        GameObject OKLavel = Canvas.transform.Find("OK").gameObject;

        if ((bool)OperateCostomProperty.GetUserCustomProperty("isTrainingReady") == false)
        {
            OperateCostomProperty.SetUserCustomProperty("isTrainingReady", true);
            //そのユーザーのボタンのテキストを変更する
            GameObject.FindWithTag("isTrainingReadyBtn").GetComponentInChildren<Text>().text = "Cancel";

            int NumOfReadyPlayers = (int)OperateCostomProperty.GetRoomCustomProperty("isTrainingReady") + 1;
            OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", NumOfReadyPlayers);

            OKLavel.SetActive(true);
        }
        else
        {
            OperateCostomProperty.SetUserCustomProperty("isTrainingReady", false);
            //そのユーザーのボタンのテキストを変更する
            GameObject.FindWithTag("isTrainingReadyBtn").GetComponentInChildren<Text>().text = "Ready";
            OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", (int)(object)OperateCostomProperty.GetRoomCustomProperty("isTrainingReady") - 1);
            OKLavel.SetActive(false);
        }
    }
}
