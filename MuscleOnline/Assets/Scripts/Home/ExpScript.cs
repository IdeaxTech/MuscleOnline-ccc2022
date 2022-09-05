using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExpScript : MonoBehaviour
{
    [SerializeField] Image userExpImage;
    float userExpDivision;

    void Start()
    {
        userExpDivision = (float)UserInfo.UserExp / (float)UserInfo.UserMaxExp;
        userExpImage.fillAmount = userExpDivision;
    }
    //fillAmountの値を変更する関数

}
