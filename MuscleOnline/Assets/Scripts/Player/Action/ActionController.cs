using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ActionController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Collider panchCollider;
    [SerializeField] private Collider KickCollider;

    int HP;
    public int maxHP = 100;
    [SerializeField] private PlayerUIManager playerUIManager;
    [SerializeField] private Animator enemyAnimator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        panchCollider.enabled = false;
        KickCollider.enabled = false;
        HP = maxHP;
        playerUIManager.Init(this);
    }
    public void LowAttack()
    {

        animator.SetTrigger("Attack");
        animator.SetInteger("AttackType", 0);
        enemyAnimator.SetTrigger("HitCount");

    }

    public void HeavyAttack()
    {

        animator.SetTrigger("Attack");
        animator.SetInteger("AttackType", 1);

    }


    public void HideColliderPanch()
    {
        panchCollider.enabled = false;
    }

    public void ShowColliderPanch()
    {
        panchCollider.enabled = true;
    }
    public void HideColliderKick()
    {
        KickCollider.enabled = false;
    }

    public void ShowColliderKick()
    {
        KickCollider.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            Debug.Log(other.tag);
            Debug.Log("é©ï™ÇÕÉ_ÉÅÅ[ÉWÇéÛÇØÇÈ");
            animator.SetTrigger("Hit");
            Damage(damager.damage);
        }
    }




    void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
        }
        playerUIManager.UpdateHP(HP);
        Debug.Log(HP);
    }
}
