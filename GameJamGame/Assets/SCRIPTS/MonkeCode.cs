using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeCode : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject car;
    public GameObject car2;
    public Transform player;
    public Transform spawnPoint;
    public Sprite monkeHold;
    public Sprite monkeThrow;
    public float monkeRange;
    public LayerMask playerLayer;
    public int tick = 0;
    public int timeBetweenSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
       Gizmos.DrawWireSphere(transform.position, monkeRange);
    }

    // Update is called once per frame
    void Update()
    {
        Ticks();
        Collider2D[] locatePLayer = Physics2D.OverlapCircleAll(transform.position, monkeRange, playerLayer);

        if (locatePLayer.Length >= 0 && tick == timeBetweenSpawn)
        {
            sprite.sprite = monkeThrow;
            car2.SetActive(false);
            Instantiate(car, spawnPoint);
            tick = 0;
        }
        else if(tick == 500)
        {
            sprite.sprite = monkeHold;
            car2.SetActive(true);
        }
           
    }

    public void Ticks()
    {
        tick++;
    }
}
