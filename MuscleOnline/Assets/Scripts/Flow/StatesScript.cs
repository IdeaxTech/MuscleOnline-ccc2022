using UnityEngine;
using UnityEngine.SceneManagement;

public class StatesScript : MonoBehaviour
{
    //- ログイン中のユーザ情報を取得
    //- キャラクター、ステータス、ユーザー名、レベルを表示

    //- 余裕があれば戦歴も

    //- 戻るボタンをクリックしたらホーム画面に遷移
    void ToHome()
    {
        SceneManager.LoadScene("GameScene");
    }

    //- ログイン情報がなかったらタイトル画面に遷移
    void ToTitle()
    {
        SceneManager.LoadScene("GameScene");
    }
}
