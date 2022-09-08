using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{

    private Animator animator;
    [SerializeField] private Collider weaponCollider;
    int HP;
    public int maxHP = 120;
    [SerializeField] private EnemyUIManager enemyUIManager;
    private void Start()
    {
        animator = GetComponent<Animator>();
        weaponCollider.enabled = false;
        HP = maxHP;
        enemyUIManager.Init(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            Debug.Log(other.tag);
            Debug.Log("テキはダメージを受ける");
            animator.SetTrigger("Hit");
            Damage(damager.damage);
        }
        HeavyDamager heavyDamager = other.GetComponent<HeavyDamager>();
        if ( heavyDamager != null)
        {
            animator.SetTrigger("CreanHit");
            Damage(heavyDamager.damage);
        }


    }

    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }

    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }

    void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
        }
        enemyUIManager.UpdateHP(HP);
        Debug.Log(HP);
    }
}

