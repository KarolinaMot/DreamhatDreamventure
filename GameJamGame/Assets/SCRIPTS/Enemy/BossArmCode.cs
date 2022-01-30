using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmCode : MonoBehaviour
{
    public float maxX = 24;
    public float minX = -18;
    public float verticalSpeed;
    private Rigidbody2D rb;
    public bool movingTime = true;
    public float speed;
    public bool collisionWithPlayer;
    float startHeight;
    bool goUp;
    bool mustFlip;
    public LayerMask wall;
    public Transform checker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MustFlip();
        if (mustFlip)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1;
            mustFlip = false;
        }

        if (movingTime)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }else
        {
           rb.velocity = new Vector2(0, verticalSpeed * Time.deltaTime);
        }

        if (goUp){
            transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
            goUp = false;
            movingTime = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BadPlayer" || collision.gameObject.tag == "Ground")
        {
            goUp = true;
        }
    }

    private void MustFlip()
    {
        mustFlip = Physics2D.OverlapCircle(checker.position, 0.1f, wall);
    }
}
