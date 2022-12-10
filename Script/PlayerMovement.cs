using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public int bossHealth = 4;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask sandyGround;
    [SerializeField] public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    private float dirX;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float bounceForce = 14;
    [SerializeField] private float sinking = -.5f;


    public enum MovementState {idle, running, jumping, falling, attacking}//create animation data type

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource bounceSoundEffect;
    [SerializeField] private AudioSource slashSoundEffect;

    private void Start()
    {
         rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);//set left & right movement
        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//set jump movement
        }
        else if (Input.GetButtonDown("Jump") && IsSandy())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, 3.5f);
        }

        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        MovementState state;

        if (Input.GetButton("Attack") && sprite.flipX == false)
        {
            state = MovementState.attacking;
            slashSoundEffect.Play();
            AttackRight();
        }
        else if (Input.GetButton("Attack")&& sprite.flipX == true)
        {
            state = MovementState.attacking;
            slashSoundEffect.Play();
            AttackLeft();
        }
        else if (rb.velocity.y > .1f || rb.velocity.y < -0.1f)
        {
            state = MovementState.jumping;
        }
        else if (dirX > 0f)//set running animation using booleans
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        anim.SetInteger("state", (int)state);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyActions enemy = collision.gameObject.GetComponent<EnemyActions>();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Bounce();
            enemy.EnemyDie();
        }
        
        else if (collision.gameObject.CompareTag("SandTrap"))
        {
            rb.velocity = new Vector2(dirX * moveSpeed, sinking);
        }
    }

    private void Bounce()
    {
        bounceSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsSandy()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, sandyGround);
    }

    private void AttackRight()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyActions>().EnemyDie();
        }
    }

    private void AttackLeft()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyActions>().EnemyDie();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }


}
