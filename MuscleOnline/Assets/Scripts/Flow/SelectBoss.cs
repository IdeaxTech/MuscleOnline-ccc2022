using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBoss : MonoBehaviour
{
    //- クエストの情報、ボスの情報を取得
    //- 取得した情報の表示

    //- 1人でプレイを選択したらボス戦1人待機画面に遷移
    void ToSoloBossWaitRoom()
    {
        SceneManager.LoadScene("Scene");
    }
    //- みんなでプレイを選択したらボス戦協力待機画面に遷移
    void ToMultiBossWaitRoom()
    {
        SceneManager.LoadScene("Scene");
    }
}
