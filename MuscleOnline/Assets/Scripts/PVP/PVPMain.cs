using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PVPMain : MonoBehaviour
{
    [SerializeField] GameObject ReadyBtn;
    void Start()
    {
        PlayerNo.SetDisplayPlayerNo();
        GameObject.FindWithTag("MyName").GetComponent<TMP_Text>().text = UserInfo.UserName;

        ReadyBtn.SetActive(true);
    }
}
