using UnityEngine;

public class ReadyBtn : MonoBehaviour
{
    [SerializeField] GameObject ReadyBtnComponent;
    void Start()
    {
        ReadyBtnComponent.SetActive(false);
    }

}
