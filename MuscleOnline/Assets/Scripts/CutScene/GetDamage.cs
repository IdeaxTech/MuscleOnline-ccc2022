using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class GetDamage : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject Canvas = GameObject.Find("Canvas");
        Canvas.transform.Find("BossDamage").gameObject.GetComponent<TMP_Text>().text = OperateCostomProperty.GetRoomCustomProperty("BossAttack").ToString();


        int MyDamage = (int)OperateCostomProperty.GetUserCustomProperty("Count") * (int)OperateCostomProperty.GetUserCustomProperty("AttackDamage");
        Canvas.transform.Find("MyDamage").gameObject.GetComponent<TMP_Text>().text = MyDamage.ToString();
        
    }
}
