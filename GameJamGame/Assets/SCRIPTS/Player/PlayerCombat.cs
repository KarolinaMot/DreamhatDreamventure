using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public bool canAttack;

    public CharacterController2D controller;
    // Update is called once per frame

    [Header("Animators")]
    [SerializeField] Animator animator;
    [SerializeField] Animator handAnimator;
    [SerializeField] Animator smallSlashAnimator;
    [SerializeField] Animator bigSlashAnimator;

    [Header("Stats")]
    [SerializeField] float attackRange = 0.5f;
    public int totalLifes;
    public float currentLifes;
    public float currentDef;
    public float currentXp;
    public int currentCoins;
    public int currentAtk;

    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject smallSlash;
    [SerializeField] GameObject bigSlash;

    [Header("UI")]
    [SerializeField] GameObject deathScreen;

    [Header("Taking Damage")]
    [SerializeField] bool canTakeDamage = true;
    [SerializeField] GameObject poof;

    [Header("Layer masks")]
    [SerializeField] LayerMask coins;
    [SerializeField] LayerMask xp;
    [SerializeField] LayerMask enemies;

    [Header("Audio")]
    [SerializeField] AudioSource attack;
    [SerializeField] AudioSource laugh;
    [SerializeField] AudioSource money;

    bool berserker = false;
    private int timer = 0;
    private float maxXp = 1;
    int sk = 0;
    Animator slashAnimator;
    GameObject slash;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            currentXp = PlayerPrefs.GetFloat("PlayerCurrentXP");
        }
        currentCoins = PlayerPrefs.GetInt("PlayerCurrentMoney");

        Time.timeScale = 1;
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            totalLifes = PlayerPrefs.GetInt("PlayerCurrentAtk");
            currentAtk = PlayerPrefs.GetInt("PlayerCurrentAtk");
            currentLifes = PlayerPrefs.GetFloat("PlayerCurrentLifes");
            currentDef = PlayerPrefs.GetFloat("PlayerCurrentLifes");
        }

        slash = smallSlash;
        slashAnimator = smallSlashAnimator;
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

        TakeMoney();
        TakeXp();
        TakeDamage();
        Tick();
        if(slash.activeSelf)
            CheckAnimationEnd();


        if (currentXp >= maxXp && Input.GetKeyDown(KeyCode.Q))
        {
            berserker = true;
            laugh.Play();
        }

        Berserk();
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
                enemy.GetComponent<bossCode>().TakeDamage(currentAtk);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(currentAtk);
            }
            
        }
    }

    void TakeMoney()
    {
        Collider2D[] allCoins = Physics2D.OverlapCircleAll(transform.position, 0.2f, coins);

        foreach (Collider2D coin in allCoins)
        {
            Destroy(coin.gameObject);
            currentCoins++;
            money.Play();
        }
    }

    void TakeXp()
    {
        Collider2D[] allXp = Physics2D.OverlapCircleAll(transform.position, 0.2f, xp);

        foreach (Collider2D particle in allXp)
        {
            Destroy(particle.gameObject);
            currentXp += 0.1f;
            if (currentXp > maxXp)
                currentXp = maxXp;
        }
    }

    void Berserk()
    {
        if (berserker)
        {
            if (sk == 0)
            {
                currentAtk += 2;
                slash = bigSlash;
                slashAnimator = bigSlashAnimator;
                attackRange += 2;
                sk++;
            }

            currentXp -= 0.0008f;
        }
        if (currentXp <= 0 && berserker)
        {
            berserker = false;
            currentAtk -= 2; ;
            slash = smallSlash;
            slashAnimator = smallSlashAnimator;
            attackRange -= 2;
            sk = 0;
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
