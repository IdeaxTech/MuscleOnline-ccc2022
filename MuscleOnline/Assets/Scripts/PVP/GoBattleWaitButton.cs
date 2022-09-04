using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBattleWaitButton : MonoBehaviour
{
    public void OnClickGoBattleWaitButton()
    {
        SceneManager.LoadScene("BattleWait");
    }
}
 