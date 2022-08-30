using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryButton : MonoBehaviour
{
    public void OnClickBossButton()
    {
        SceneManager.LoadScene("RPGScene");
        GameObject TitleBGM = GameObject.Find("TitleBGM").gameObject;
        Destroy(TitleBGM);
    }
}
