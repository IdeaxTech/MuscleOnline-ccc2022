using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    [SerializeField] GameObject TitleBGM;
    void Start()
    {
        GameObject BGMObject = GameObject.Find("TitleBGM");
        if(!BGMObject)
        {
            TitleBGM.SetActive(true);
            Debug.Log("見つかりませんでした");
            //GameObject CreateBGMObject = (GameObject)Resources.Load("TitleBGM");
            //Instantiate(CreateBGMObject, new Vector3(282.0f, 70.0f, 0.0f), Quaternion.identity);
        }

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
