using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    [SerializeField] private string SceneName;

    public void On2ndSceneMove()
    {
        SceneName = "BossBattle";
        SceneManager.LoadScene(SceneName);

        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
    }
}
