using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{

    //- BGMを流す
    // Unity側で実装


    //- 画面クリックでロード画面に遷移 
    void ToFirstLoad()
    {
        SceneManager.LoadScene("GameScene");
    }
}
