using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackFieldButton : MonoBehaviour
{
    public void OnClickBackFieldButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Home");
    }
}
