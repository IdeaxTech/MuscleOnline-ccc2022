using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBossBattleButton : MonoBehaviour
{
    public void OnClickGoBossBattleButton()
    {
        SceneManager.LoadScene("RPGScene");
    }
}
