using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputFieldScript : MonoBehaviour
{
    public InputField inputField;
    public Text displayText;

    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
   

    }

    public void GetinputName()
    {
        //InputFieldからテキスト情報を取得する
        string name = inputField.text;
        Debug.Log(name);

        //入力フォームのテキストを空にする
        inputField.text = "";
    }

    public void OnClickDecision()
    {
        SceneManager.LoadScene("Home");
    }

    public void OnEndEdit()
    {
        string inputFieldText = GetComponent<InputField>().text;
        displayText.text = inputFieldText;
    }
}
