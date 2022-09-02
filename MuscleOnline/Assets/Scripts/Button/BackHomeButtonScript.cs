using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackHomeButtonScript : MonoBehaviour
{
    public void OnClickBackHomeButton()
    {
        SceneManager.LoadScene("Home");
    }
}
