using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExpScript : MonoBehaviour
{
    [SerializeField] Image userExpImage;
    float userExp;
    float userMaxExp;
    float userExpDivision;

    // Start is called before the first frame update
    void Start()
    {
        userExpImage = GetComponent<Image>();

        //userExp = (float)UserInfo.UserExp;
        userExp = 10;
        //userMaxExp = (float)UserInfo.UserMaxExp;
        userMaxExp = 20;
        Debug.Log("userExp" + userExp);
        Debug.Log("userMaxExp" + userMaxExp);



        userExpDivision = userExp / userMaxExp;
        Debug.Log("userExpDivision" + userExpDivision);
        UpdateUserExp(userExpDivision);
    }

    //private void Update()
    //{
    //    ////userExp = (float)UserInfo.UserExp;
    //    //userExp = 10;
    //    ////userMaxExp = (float)UserInfo.UserMaxExp;
    //    //userMaxExp = 20;
    //    //Debug.Log("userExp" + userExp);
    //    //Debug.Log("userMaxExp" + userMaxExp);



    //    //userExpDivision = userExp / userMaxExp;
    //    //Debug.Log("userExpDivision" + userExpDivision);
    //    //UpdateUserExp(userExpDivision);

    //}

    //fillAmountの値を変更する関数
    public void UpdateUserExp(float userExpDivision)
    {
        userExpImage.fillAmount = userExpDivision;
    }

}
