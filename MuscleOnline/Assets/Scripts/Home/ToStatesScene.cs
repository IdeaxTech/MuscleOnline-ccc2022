using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStatesScene : MonoBehaviour
{
    //- キャラをクリックしたらステータス確認画面に遷移
    public void ToStates()
    {
        SceneManager.LoadScene("UserStates");
    }
}
