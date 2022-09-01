using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserStates : MonoBehaviour
{
    float userExp;
    float userMaxExp;
    float userExpDivision;

    // Start is called before the first frame update
    void Start()
    {
        //レベル関連
        GameObject.Find("UserExp").GetComponent<TMP_Text>().text = UserInfo.UserExp.ToString();
        GameObject.Find("UserMaxExp").GetComponent<TMP_Text>().text = UserInfo.UserMaxExp.ToString();
        GameObject.Find("UserLevel").GetComponent<TMP_Text>().text = UserInfo.UserLevel.ToString();
    
        //ステータス関連
        GameObject.Find("UserHP").GetComponent<TMP_Text>().text = UserInfo.UserHP.ToString();
        GameObject.Find("UserAttack").GetComponent<TMP_Text>().text = UserInfo.UserAttack.ToString();
        GameObject.Find("UserDefence").GetComponent<TMP_Text>().text = UserInfo.UserDefence.ToString();

    }


}
