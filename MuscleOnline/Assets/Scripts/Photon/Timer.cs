using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviourPunCallbacks
{
    public float NowTime;
    public GameObject countdown;

    void Update()
    {
        if ((bool)OperateCostomProperty.GetRoomCustomProperty("isTraining"))
        {
            NowTime = Mathf.Floor((float)(PhotonNetwork.Time - (double)OperateCostomProperty.GetRoomCustomProperty("StartTime")));
            GameObject.FindWithTag("Timer").GetComponent<TMP_Text>().text = ((int)OperateCostomProperty.GetRoomCustomProperty("TrainingTime") - NowTime).ToString();

            if (!(bool)OperateCostomProperty.GetRoomCustomProperty("isTraining"))
                return;

            if (NowTime == (int)OperateCostomProperty.GetRoomCustomProperty("TrainingTime"))
            {
                OperateCostomProperty.SetRoomCustomProperty("isTraining", false);
                NowTime = 0;
                countdown.SetActive(false);
            }
        }

    }
}
