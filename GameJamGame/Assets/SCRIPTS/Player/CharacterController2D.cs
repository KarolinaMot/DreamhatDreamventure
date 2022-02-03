using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Animator handAnim;
    [SerializeField] PlayerCombat combat;
    Vector3 cameraPos;
    Rigidbody2D rb;
    CapsuleCollider2D mainCollider;
    Transform t;

    [Header("Stats")]
    [SerializeField] float maxSpeed = 3.4f;
    [SerializeField] float jumpHeight = 6.5f;
    [SerializeField] float gravityScale = 1.5f;

    [Header("KnockBack")]
    [SerializeField] float knockBack;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    [Header("Camera")]
    private Camera mainCamera;
    [SerializeField] float camHeight;

    [Header("Movement Flags")]
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isMoving = false;


    [Header("Audio")]
    [SerializeField] AudioSource walkingSound;

    bool facingRight = true;
    float moveDirection = 0;

    void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        mainCamera = Camera.main;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    void Update()
    {
        UpdateAnimator();
        MovementControls();
        FlipPlayer();
        Jumping();

        if (mainCamera)
        {
                mainCamera.transform.position = new Vector3(t.position.x, t.position.y + camHeight, cameraPos.z);
        } 
    }

    void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    private void Move()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && knockbackCount <= 0)
            rb.velocity = new Vector2((moveDirection) * maxSpeed, rb.velocity.y);

        if (knockbackCount > 0)
            KnockBack();
    }

    void CheckGrounded()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);

        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
        if (!isGrounded)
        {
            walkingSound.Stop();
        }
    }

    void Jumping()
    {   
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            walkingSound.Stop();
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/3);
        }
    }

    void FlipPlayer()
    {
        // Change facing direction
        if (moveDirection != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            isMoving = true;
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }
        else
            isMoving = false;
    }

    void MovementControls()
    {
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            if(!walkingSound.isPlaying && isGrounded)
                walkingSound.Play();
        }
        else
        {

                moveDirection = 0;
                walkingSound.Stop();

        }
    }

    void UpdateAnimator()
    {
        anim.SetBool("isMoving", isMoving);
        handAnim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", !isGrounded);
        handAnim.SetBool("isJumping", !isGrounded);
    }

    void KnockBack()
    {
        if(knockFromRight)
            rb.velocity = new Vector2(-knockBack, knockBack);
        else
            rb.velocity = new Vector2(knockBack, knockBack);

        knockbackCount -= Time.deltaTime;
    }
}