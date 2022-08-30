using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    [SerializeField] private string SceneName;

    public void On2ndSceneMove()
    {
        SceneName = "Quest";
        SceneManager.LoadScene(SceneName);
    }
}
