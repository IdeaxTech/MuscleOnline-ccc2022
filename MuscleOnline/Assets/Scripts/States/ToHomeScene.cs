using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHomeScene : MonoBehaviour
{
    public void ToHome()
    {
        SceneManager.LoadScene("Home");
    }
}
