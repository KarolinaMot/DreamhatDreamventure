using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool canAttack;

    public CharacterController2D controller;
    // Update is called once per frame
    public Animator animator;
    public Animator handAnimator;
    public Animator slashAnimator;

    public Transform attackPoint;
    public GameObject slash;
    public GameObject poof;
    public LayerMask enemies;

    public bool canTakeDamage = true;
    public float attackRange = 0.5f;

    public int attackDamage = 1;
    public int playerHealth = 3;
    public float playerDefense = 0;
    public int currentHealth = 3;
    public int timer = 0;


    private void Awake()
    {
        slash.SetActive(false);
        currentHealth = playerHealth;
    }
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }
        }
        
        TakeDamage();
        Tick();
        if(slash.activeSelf)
            CheckAnimationEnd();
    }
    void Attack()
    {
        animator.SetTrigger("isAttacking");
        handAnimator.SetTrigger("isAttacking");
        slash.SetActive(true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    void TakeDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.4f, enemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (canTakeDamage)
            {
                Debug.Log("damage taken");
                controller.knockbackCount = controller.knockbackLength;

                if (enemy.transform.position.x > transform.position.x)
                    controller.knockFromRight = true;
                else
                    controller.knockFromRight = false;

                currentHealth--;
                canTakeDamage = false;
            }
        }

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
    }

    void CheckAnimationEnd()
    {      if(slashAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
                slash.SetActive(false);        
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
