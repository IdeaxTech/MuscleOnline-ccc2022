using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadyBtn : MonoBehaviour
{
    // トレーニング前のボタンが押された時
    public void ReadyTraining()
    {
        Debug.Log("Readyボタンが押されました");
        OperateCostomProperty.SetUserCustomProperty("isTrainingReady", true);
        if ((bool)OperateCostomProperty.GetUserCustomProperty("isTrainingReady") == false)
        {
            OperateCostomProperty.SetUserCustomProperty("isTrainingReady", true);
            //そのユーザーのボタンのテキストを変更する
            GameObject.FindWithTag("isTrainingReadyBtn").GetComponentInChildren<Text>().text = "Cancel";

            int NumOfReadyPlayers = (int)OperateCostomProperty.GetRoomCustomProperty("isTrainingReady") + 1;
            OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", NumOfReadyPlayers);

        }
        else
        {
            OperateCostomProperty.SetUserCustomProperty("isTrainingReady", false);
            //そのユーザーのボタンのテキストを変更する
            GameObject.FindWithTag("isTrainingReadyBtn").GetComponentInChildren<Text>().text = "Ready";
            OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", (int)(object)OperateCostomProperty.GetRoomCustomProperty("isTrainingReady") - 1);
        }
    }
}
