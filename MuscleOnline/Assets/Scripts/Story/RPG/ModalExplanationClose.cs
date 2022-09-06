using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalExplanationClose : MonoBehaviour
{
    [SerializeField] GameObject ModalExplanation;

    // Update is called once per frame
    public void ModalClose()
    {
        ModalExplanation.SetActive(false);
    }
}
