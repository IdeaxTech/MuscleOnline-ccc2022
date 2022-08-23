using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    void Start()
    {
        GameObject.FindWithTag("Name").GetComponent<TMP_Text>().text = UserInfo.UserName;
        GameObject.FindWithTag("Level").GetComponent<TMP_Text>().text = UserInfo.UserLevel.ToString();
    }
    //- ログイン中のユーザ情報を取得
    //- ユーザ名とキャラを画面上に表示

    //- BGM流す
    // Unity側で実装

    //- キャラをクリックしたらステータス確認画面に遷移
    void ToStates()
    {
        SceneManager.LoadScene("GameScene");
    }

    //- ストーリーボタンをクリックしたらストーリー画面に遷移
    void ToRPGMain()
    {
        SceneManager.LoadScene("GameScene");
    }

    //- 対人戦ボタンをクリックしたら対人戦画面に遷移
    void ToPVPMain()
    {
        SceneManager.LoadScene("GameScene");
    }

    //- ログイン情報がなかったらタイトル画面に遷移

    void ToTitle()
    {
        SceneManager.LoadScene("GameScene");
    }
}
