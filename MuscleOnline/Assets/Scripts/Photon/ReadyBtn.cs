using UnityEngine;

public class ReadyBtn : MonoBehaviour
{
    // トレーニング前のボタンが押された時
    public void ReadyTraining()
    {
        Debug.Log("Readyボタンが押されました");
        OperateCostomProperty.SetUserCustomProperty("isTrainingReady", true);
    }

}
