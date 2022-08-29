using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackFieldButton : MonoBehaviour
{
    public void OnClickBackFieldButton()
    {
        SceneManager.LoadScene("Home");
    }
}
