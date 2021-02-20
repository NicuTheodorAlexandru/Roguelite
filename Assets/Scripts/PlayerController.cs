using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D playerCollider;
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    private int availableJumps = 1;
    [SerializeField]
    private int currentJump = 0;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float jumpSpeed = 4;
    [SerializeField]
    Rigidbody2D rigidbody;
    public float health = 100;
    public float maxHealth = 100;
    public HealthbarController healthbarController;
    private float xInput, yInput;
    private Transform spawnpoint;
    private BoxCollider2D hitbox;
    private bool grounded = false;
    public LayerMask mask;
    //private Collider[] childrenColliders;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        // adding all colliders to an array, but our collider will be added to !
        /*childrenColliders = GetComponentsInChildren<Collider>();


        foreach (Collider col in childrenColliders)
        {
            // checking if it is our collider, then skip it, 
            if (col != GetComponent<Collider>())
            {
                // if it is not our collider then ignore collision between our collider and childs collider
                Physics.IgnoreCollision(col, GetComponent<Collider>());
            }
        }*/
        GameObject.Find("Background Canvas").GetComponent<Canvas>().worldCamera = GetComponentInChildren<Camera>();
        spawnpoint = GameObject.Find("Spawnpoint").GetComponent<Transform>();
        healthbarController = GameObject.Find("Healthbar Image").GetComponent<HealthbarController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
        Animation();
        Attack();
    }

    private void SetHealth(float health)
    {
        this.health = health;
        if(health > maxHealth)
        {
            this.health = maxHealth;
        }
        else if(health < 0)
        {
            this.health = 0;
        }
        healthbarController.SetHealthbarHealth(health, maxHealth);
    }

    private void FixedUpdate()
    {
        Grounded();
        Movement();
    }

    void Attack()
    {
        if (animator.GetBool("Attacking"))
            return;
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attacking", true);
            //attack logic here
        }
    }

    void InputMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            xInput = -speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xInput = speed;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            yInput = 1;
        }
    }

    void Animation()
    {
        if (!IsGrounded())
        {
            if (rigidbody.velocity.y > 0)
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
        if (!MathUtils.Equal(rigidbody.velocity.x, 0))
        {
            animator.SetBool("Running", true);
            if (rigidbody.velocity.x > 0 && spriteRenderer.flipX)
            {
                Flip();
            }
            else if (rigidbody.velocity.x < 0 && !spriteRenderer.flipX)
            {
                Flip();
            }
        }
        else
        {
            animator.SetBool("Running", false);
        }
        
    }

    void Movement()
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
            if(currentJump < availableJumps)
            {
                // rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                move.y = jumpSpeed;
                currentJump++;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                //rigidbody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
        //
        rigidbody.velocity = new Vector2(move.x, rigidbody.velocity.y + move.y);
    }

    void Respawn()
    {
        //SetHealth(maxHealth);
        //transform.position = spawnpoint.position;
        //no respawn for you
        SceneManager.LoadScene("mainmenu");
    }

    private void Grounded()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, (hitbox.size.y / 2 - hitbox.offset.y), mask);
        //Debug.DrawRay(transform.position, -Vector2.up * (hitbox.size.y / 2 - hitbox.offset.y), Color.red);
        //Debug.Log(hit.collider);
        grounded = false;
        if (hit.collider != null)
        {
            //make player stop falling
            grounded = true;
        }
        
    }

    public void GotAttacked(int damage)
    {
        SetHealth(health - damage);
        if(health == 0)
        {
            Respawn();
        }
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private bool IsGrounded()
    {
        return grounded;
    }

    private void OnCollisionStay2D(Collision2D collision)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if(IsGrounded())
            {
                currentJump = 0;
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", false);
            }
        }
    }
}
