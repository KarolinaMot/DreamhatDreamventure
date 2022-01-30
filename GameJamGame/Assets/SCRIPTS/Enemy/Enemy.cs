using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int coinReward;
    public float expReward;
    int currentHealth;
    public GameObject deathPoof;
    public GameObject coin;
    public GameObject exp;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
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
}
