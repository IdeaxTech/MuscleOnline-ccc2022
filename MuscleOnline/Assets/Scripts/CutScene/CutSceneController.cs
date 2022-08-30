using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    [SerializeField] private string SceneName;

    public void On2ndSceneMove()
    {
        SceneManager.LoadScene(SceneName);
    }
}
