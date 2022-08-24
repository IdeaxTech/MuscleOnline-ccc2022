using UnityEngine;

public class ReadyBtn : MonoBehaviour
{
    void Start()
    {
        Debug.Log("ReadyBtnがアクティブになりました");   
    }
    // トレーニング前のボタンが押された時
    public void ReadyTraining()
    {
        Debug.Log("Readyボタンが押されました");
        OperateCostomProperty.SetUserCustomProperty("isTrainingReady", true);
    }

}
