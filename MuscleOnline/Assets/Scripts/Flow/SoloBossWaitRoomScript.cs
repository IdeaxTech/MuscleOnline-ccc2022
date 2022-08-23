using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloBossWaitRoomScript : MonoBehaviour
{
    //- ログイン中ユーザの情報を取得
    //- ユーザ名、キャラクター、レベルを表示

    //- OKボタンをクリックしたらボス戦画面に遷移
    void ToBossButtle()
    {
        SceneManager.LoadScene("Scene");
    }
}
