using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadScript : MonoBehaviour
{
    //- ローディングアニメーション(ビデオを流す)
    // TODO

    //- 余裕があれば何％ローディングしているか表示

    //- BGM流す
    // Unity側で実装

    //- ログイン情報がなければユーザー登録画面に遷移

    void ToUserRegister()
    {
        SceneManager.LoadScene("GameScene");
    }

    //- ログイン情報があればホーム画面に遷移
    void ToHome()
    {
        SceneManager.LoadScene("GameScene");
    }
}
