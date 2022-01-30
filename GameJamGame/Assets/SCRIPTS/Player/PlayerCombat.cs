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
    public Animator smallSlashAnimator;
    public Animator bigSlashAnimator;
    public GameObject deathScreen;
    public AudioSource attack;

    public Transform attackPoint;
    public GameObject smallSlash;
    public GameObject bigSlash;
    public GameObject slash;
    public GameObject poof;
    public LayerMask enemies;

    public bool canTakeDamage = true;
    public float attackRange = 0.5f;

    public int totalLifes;
    public int currentDamage;
    public float currentLifes;
    public float currentDef;
    public int timer = 0;


    private void Start()
    {
        totalLifes = PlayerPrefs.GetInt("PlayerCurrentAtk");
        currentDamage = PlayerPrefs.GetInt("PlayerCurrentAtk");
        currentLifes = PlayerPrefs.GetFloat("PlayerCurrentLifes");
        currentDef = PlayerPrefs.GetFloat("PlayerCurrentLifes");

        slash.SetActive(false);
        slashAnimator = smallSlashAnimator;
    }
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                attack.Play();
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
            if(enemy.gameObject.name== "BOSS")
            {
                enemy.GetComponent<bossCode>().TakeDamage(currentDamage);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(currentDamage);
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name== "bosscrushingarm")
        {
            if (canTakeDamage)
            {
                Debug.Log("damage taken");
                controller.knockbackCount = controller.knockbackLength;

                if (collision.transform.position.x > transform.position.x)
                    controller.knockFromRight = true;
                else
                    controller.knockFromRight = false;

                currentLifes--;
                canTakeDamage = false;
            }

            if (currentLifes <= 0)
            {
                Death();
            }
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
                controller.knockbackCount = controller.knockbackLength;

                if (enemy.transform.position.x > transform.position.x)
                    controller.knockFromRight = true;
                else
                    controller.knockFromRight = false;

                currentLifes--;
                canTakeDamage = false;
            }
        }

        if(currentLifes <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Time.timeScale = 0;
        deathScreen.SetActive(true);
       
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
