using System.Collections.Generic;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserRegisterScript : MonoBehaviour
{
    int initialize_level = 0;
    int initialize_exp = 0;
    int initialize_maxexp = 20;
    int initialize_hp = 50;
    int initialize_attack = 5;
    int initialize_defence = 2;
    //- 名前の入力を求める


    //- 完了ボタンを押すとデータベースに登録、ログインをし、ホーム画面に遷移
    public void ClickCompleteBtn()
    {
        string input = GameObject.FindWithTag("InputName").GetComponent<InputField>().text;

        if (!CheckName(input))
            return;

        UserRegister(input);
        Debug.Log(input + "情報を登録しました");
        ToHome();
    }

    //- ユーザー名が未入力、長すぎる場合(15文字以内)エラー
    bool CheckName(string name)
    {
        
        if (name == "")
        {
            // 名前を入力してください
            Debug.Log("ユーザー名を入力してください");
            return false;
        }

        if (name.Length >= 15)
        {
            // 15文字以内で入力してください
            Debug.Log("15文字以内で入力してください");
            return false;
        }

        return true;

    }
    //- 入力された名前をもとにユーザーを登録
    void UserRegister(string name)
    {
        string id = RandomPassword.Generate(16);

        Dictionary<string, object> SendData = new Dictionary<string, object>()
        {
            {"username", name},
            {"initial_username", name},
            {"user_level", initialize_level},
            {"user_exp", initialize_exp},
            {"user_maxexp", initialize_maxexp},
            {"user_hp", initialize_hp},
            {"user_attack", initialize_attack},
            {"user_defence", initialize_defence},
            {"create_at", Timestamp.GetCurrentTimestamp()},
            {"update_at", Timestamp.GetCurrentTimestamp()}
        };

        DatabaseOperation.AddData("users", id, SendData);

        UserInfo.UserName = name;
        UserInfo.UserId = id;

        UserInfo.UserLevel = initialize_level;
        UserInfo.UserExp = initialize_exp;
        UserInfo.UserMaxExp = initialize_maxexp;

        UserInfo.UserHP = initialize_hp;
        UserInfo.UserAttack = initialize_attack;
        UserInfo.UserDefence = initialize_defence;

        PlayerPrefs.SetString("UserID", id);
        PlayerPrefs.Save();
    }

    void ToHome()
    {
        SceneManager.LoadScene("Home");
    }


}
