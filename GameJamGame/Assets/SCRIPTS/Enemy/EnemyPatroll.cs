using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    public bool mustPatrol = true;
    public float walkSpeed = 3;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public bool mustFlip;
    public bool mustFlip2;

    private void Update()
    {
        CheckGround();
        Flip();
        rb.velocity = new Vector2(walkSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void FixedUpdate()
    {

    }

    void Flip()
    {
        if (mustFlip || mustFlip2)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            walkSpeed *= -1;
            mustFlip = false;
            mustFlip2 = false;
        }
    }
    private void CheckGround(){

        mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        mustFlip2 = Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);

    }
}
