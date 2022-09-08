using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{

    int HP;
    [SerializeField] private int maxHP = 100;
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    void Damage(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            HP = 0;
        }
    }
}
