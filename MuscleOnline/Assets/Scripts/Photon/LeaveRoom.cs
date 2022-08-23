using UnityEngine.SceneManagement;
using Photon.Pun;

public class LeaveRoom : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveGameRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LeaveThisRoom()
    {
        PhotonNetwork.LeaveRoom();
        Invoke("ToFirstScene", 1.5f);
    }

    void ToFirstScene()
    {
        SceneManager.LoadScene("Quest");
    }
}
