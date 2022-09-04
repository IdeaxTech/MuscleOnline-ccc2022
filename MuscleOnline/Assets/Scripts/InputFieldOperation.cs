using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldOperation : MonoBehaviour
{
    public void OnValueChanged()
    {
        string value = this.GetComponent<InputField>().text;
        if (value.IndexOf("\n") != -1)
        {
            this.GetComponent<InputField>().text = value;
        }
    }
}
