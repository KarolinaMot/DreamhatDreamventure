using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCode : MonoBehaviour
{
    public int maxHealth = 3;
    public int coinReward;
    public float expReward;
    int currentHealth;
    public GameObject deathPoof;
    public GameObject coin;
    public GameObject exp;
    public GameObject tiles;
    public bool hitTaken = false;
    public int tick =0;
    public int timeBetween;
    public GameObject arm;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        Ticks();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        tiles.SetActive(false);
        hitTaken = true;
        if (currentHealth <= 0)
        {
            Die();
        }
        if (currentHealth == 5)
        {
            Instantiate(arm, spawnPoint);
        }
    }

    void Die()
    {
        Instantiate(deathPoof, transform.position, Quaternion.identity);
        for (int i = 0; i < coinReward; i++)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        for (int i = 0; i < expReward; i++)
        {
            Instantiate(exp, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        Debug.Log("Enemy died");
    }

    void Ticks()
    {
        if (hitTaken)
        {
            tick++;

            if(tick == timeBetween)
            {
                tiles.SetActive(true);
                hitTaken = false;
                tick = 0;
            }
        }
    }
}
