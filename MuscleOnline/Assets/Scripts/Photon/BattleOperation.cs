using Photon.Pun;
using UnityEngine;

public class BattleOperation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //BossBattleScript.BossBattle();
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
        //    BossBattleScript.BossBattle();
        //}


        Destroy(this.gameObject);

    }
}
