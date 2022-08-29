using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButtonScript : MonoBehaviour
{
    [SerializeField] AudioClip ClickSound;
    [SerializeField] AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        //デバッグ用
        UserInfo.UserName = "aa";
    }

    public void OnClickStartButton()
    {
        Source.PlayOneShot(ClickSound);
        if (UserInfo.UserName == null)
            SceneManager.LoadScene("InputScreen");
        else
            SceneManager.LoadScene("Home");
    }
}
