using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(EnemyActionController enemyActionController)
    {
        hpSlider.maxValue = enemyActionController.maxHP;
        hpSlider.value = enemyActionController.maxHP;
    }
    public void UpdateHP(int HP)
    {
        hpSlider.value = HP;
    }

}
