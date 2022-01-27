using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Update is called once per frame
    public Animator animator;
    public Animator handAnimator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemies;
    public bool canTakeDamage = true;

    public int attackDamage = 1;
    public int playerHealth = 3;
    public int currentHealth = 3;
    public int timer = 0;

    private void Awake()
    {
        currentHealth = playerHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        TakeDamage();
        Tick();
    }
    void Attack()
    {
        animator.SetTrigger("isAttacking");
        handAnimator.SetTrigger("isAttacking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    void TakeDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.2f, enemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (canTakeDamage)
            {
                Debug.Log("damage taken");
                currentHealth--;
                canTakeDamage = false;
            }
                
        }

        if(currentHealth <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
    }

    void Tick()
    {
        if (!canTakeDamage)
        {
            timer++;
            if (timer >= 400)
            {
                canTakeDamage = true;
                timer = 0;
            }
        }        
    }
}
