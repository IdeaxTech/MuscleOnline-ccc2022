using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(ActionController actionController)
    {
        hpSlider.maxValue = actionController.maxHP;
        hpSlider.value = actionController.maxHP;
    }
    public void UpdateHP(int HP)
    {
        hpSlider.value = HP;
    }


}
