using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishAttackAnimation : MonoBehaviour
{
    void FinishAttackAnimationToAnotherScene()
    {
        SceneManager.LoadScene("BossBattle");

        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isTraining", false);
    }
}
