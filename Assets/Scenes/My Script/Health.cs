using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int startingHealth = 5;

    private int currentHealth;
    private Animator animator;

    void OnEnable()//OnEnableはObjectができる度に実行されるのでリスポーンとか可能
    {
        currentHealth = startingHealth;
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            Die();
        }

        animator.SetTrigger("Damage");
    }

    private void Die()
    {   
        //animator.SetTrigger("Death");
        gameObject.SetActive(false);
    }

}
