using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    public Animator anim;
    public Animator handAnim;
    public PlayerCombat combat;
    public GameObject deathScreen;
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;
    int doubleJump =0;


    bool facingRight = true;
    float moveDirection = 0;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isMoving = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    [SerializeField]  float knockBack;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;


    private float maxXp = 1;
    public LayerMask coins;
    public LayerMask xp;
    public float currentXp;
    public int currentCoins;
    public int currentAtk;

    public float camHeight;
    public int tick;
    public bool berserker = false;
    int sk = 0;

    // Use this for initialization
    void Awake()
    {
        currentXp = PlayerPrefs.GetFloat("PlayerCurrentXP");
        currentCoins = PlayerPrefs.GetInt("PlayerCurrentMoney");

        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
        TakeMoney();
        TakeXp();
        MovementControls();
        FlipPlayer();
        Jumping();
        Berserk();

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y+camHeight, cameraPos.z);
        }

        if(currentXp >= maxXp && Input.GetKeyDown(KeyCode.Q))
        {
            berserker = true;
        }
        
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    doubleJump = 0;
                    break;
                }
            }
        }

        // Apply movement velocity
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && knockbackCount <= 0)
            r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        if (knockbackCount > 0)
            KnockBack();

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    void Berserk()
    {
        if (berserker)
        {
            if (sk == 0)
            {
                currentAtk+=2;
                combat.slash = combat.bigSlash;
                combat.slashAnimator = combat.bigSlashAnimator;
                combat.attackRange += 2;
                sk++;
            }

            currentXp -= 0.0008f;
        }
        if (currentXp <= 0 && berserker)
        {
            berserker = false;
            currentAtk -= 2; ;
            combat.slash = combat.smallSlash;
            combat.slashAnimator = combat.smallSlashAnimator;
            combat.attackRange -= 2;
            sk = 0;
        }
    }
    void Jumping()
    {
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && doubleJump<1)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            doubleJump++;
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
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
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
        }
    }

    void TakeXp()
    {
        Collider2D[] allXp = Physics2D.OverlapCircleAll(transform.position, 0.2f, xp);

        foreach (Collider2D particle in allXp)
        {
            Destroy(particle.gameObject);
            currentXp+=0.1f;
            if (currentXp > maxXp)
                currentXp = maxXp;
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
            r2d.velocity = new Vector2(-knockBack, knockBack);
        else
            r2d.velocity = new Vector2(knockBack, knockBack);

        knockbackCount -= Time.deltaTime;
    }
}