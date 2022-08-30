using Photon.Pun;
using UnityEngine;

public class BattleOperation : MonoBehaviour
{
    //[SerializeField] AudioSource PlayAudio;
    //[SerializeField] AudioClip BGM;
    [SerializeField] GameObject PlayAudio;

    // Start is called before the first frame update
    void Start()
    {

        BossBattleScript.BossBattle();
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
            BossBattleScript.BossBattle();
        }

        GameObject BossWaitBGM = GameObject.Find("BossWaitBGM").gameObject;
        Destroy(BossWaitBGM);

        PlayAudio.SetActive(true);
        //PlayAudio.PlayOneShot(BGM);
        Destroy(this.gameObject);

    }
}
