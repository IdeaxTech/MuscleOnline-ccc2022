using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanelChange : MonoBehaviour
{
    [SerializeField] GameObject PanelJoin;
    [SerializeField] GameObject PanelMake;

    void Start()
    {
        //BackToMenuメソッドを呼び出す
        BackToPanelJoin();
    }

    public void BackToPanelJoin()
    {
        PanelJoin.SetActive(true);
        PanelMake.SetActive(false);
    }

    public void BackToPanelMake()
    {
        PanelJoin.SetActive(false);
        PanelMake.SetActive(true);
    }


}
