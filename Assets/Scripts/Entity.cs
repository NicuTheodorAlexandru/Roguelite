using Assets.Scripts;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected BoxCollider2D entityCollider;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D entityRigidbody;
    [SerializeField]
    protected BoxCollider2D hitbox;
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float jumpSpeed;
    [SerializeField]
    protected LayerMask groundLayerMask;
    [SerializeField]
    protected int availableJumps = 1;
    protected int currentJump = 0;
    protected float xInput, yInput;
    protected bool grounded;

    public virtual float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            else if (health < 0)
            {
                health = 0;
                Death();
            }
        }
    }

    protected virtual void Animation()
    {
        if (!IsGrounded())
        {
            if (entityRigidbody.velocity.y > 0)
            {
                animator.SetBool("Jumping", true);
                animator.SetBool("Falling", false);
            }
            else
            {
                animator.SetBool("Falling", true);
                animator.SetBool("Jumping", false);
            }
        }
        if (!MathUtils.Equal(entityRigidbody.velocity.x, 0))
        {
            animator.SetBool("Running", true);
            if (entityRigidbody.velocity.x > 0 && spriteRenderer.flipX)
            {
                Flip();
            }
            else if (entityRigidbody.velocity.x < 0 && !spriteRenderer.flipX)
            {
                Flip();
            }
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    protected virtual void FixedUpdate()
    {
        Grounded();
        Movement();
    }

    protected virtual void Movement()
    {
        //rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        Vector2 move = new Vector2();
        move.x = xInput * speed;
        //Debug.Log(xInput);
        xInput = 0;
        move *= Time.fixedDeltaTime;
        if (yInput == 1)
        {
            yInput = 0;
            if (currentJump < availableJumps)
            {
                // rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                move.y = jumpSpeed;
                currentJump++;
                entityRigidbody.velocity = new Vector2(entityRigidbody.velocity.x, 0);
                //rigidbody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
        //
        entityRigidbody.velocity = new Vector2(move.x, entityRigidbody.velocity.y + move.y);
    }

    protected virtual void Start()
    {
        entityRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Death()
    {

    }

    public virtual void GotAttacked(int damage)
    {
        Health -= damage;
    }

    protected bool IsGrounded()
    {
        return grounded;
    }

    protected virtual void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    protected void Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, (hitbox.size.y / 2 - hitbox.offset.y), groundLayerMask);
        grounded = false;
        if (hit.collider != null)
        {
            //make player stop falling
            grounded = true;
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (!animator.GetBool("Falling"))
            return;
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (IsGrounded())
            {
                currentJump = 0;
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", false);
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (IsGrounded())
            {
                currentJump = 0;
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", false);
            }
        }
    }
}
