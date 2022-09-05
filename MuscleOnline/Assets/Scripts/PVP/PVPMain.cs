using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PVPMain : MonoBehaviour
{
    [SerializeField] GameObject ReadyBtn;
    void Start()
    {
        
        GameObject.FindWithTag("MyName").GetComponent<TMP_Text>().text = UserInfo.UserName;

        ReadyBtn.SetActive(true);
        PlayerNo.SetDisplayPlayerNo();
    }
}
