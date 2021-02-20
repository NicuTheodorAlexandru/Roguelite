using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int health = 2;
    public Animator animator;
    public float speed = 17;
    public float aggroRange = 10;
    public CollisionBridge colliderLeft;
    public CollisionBridge colliderRight;
    public float attackRange;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Rigidbody2D playerRigidbody;
    private bool aggroed = false;
    private int xInput = 0, yInput = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        //playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        //Debug.Log(colliderLeft);
    }

    private void Update()
    {
        if(aggroed)
        {
            if(DistanceToPlayer() > aggroRange)
            {
                aggroed = false;
            }
        }
        else
        {
            if(DistanceToPlayer() < aggroRange)
            {
                aggroed = true;
            }
        }
        if(aggroed)
        {
            if (!animator.GetBool("Attacking"))
            {
                FaceTowardsPlayer();
                if (DistanceToPlayer() < attackRange)
                {
                    Attack();
                }
                else
                {
                    MoveTowardPlayer();
                }
            }
            else
            {
                //Debug.Log(frame);
            }
        }
    }

    private void MoveTowardPlayer()
    {
        if (playerRigidbody.position.x > rigidbody.position.x)
        {
            xInput = 1;
        }
        else
        {
            xInput = -1;
        }
    }

    private void Attack()
    {
        animator.SetBool("Attacking", true);
    }

    private void LateUpdate()
    {
        if(!IsGrounded())
        {
            xInput = 0;
        }
        Vector2 move = new Vector2(xInput * speed, 0);
        xInput = yInput = 0;
        if(move.x == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }
        Move(move);
    }

    private void Move(Vector2 dir)
    {
        dir.y = rigidbody.velocity.y;
        rigidbody.velocity = dir;
    }

    public void GotAttacked(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            animator.SetBool("Die", true);
            enabled = false;
            colliderRight.gameObject.SetActive(false);
            colliderLeft.gameObject.SetActive(false);
        }
        else
        {
            animator.SetBool("Hit", true);
        }
    }

    private void FaceTowardsPlayer()
    {
        if(rigidbody.position.x > playerRigidbody.position.x && !spriteRenderer.flipX)
        {
            Flip();
        }
        else if(rigidbody.position.x < playerRigidbody.position.x && spriteRenderer.flipX)
        {
            Flip();
        }
    }

    private float DistanceToPlayer()
    {
        if(playerRigidbody == null)
        {
            playerRigidbody = GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>();
            return Mathf.Infinity;
        }
        Vector2 pos1 = new Vector2(rigidbody.position.x, rigidbody.position.y);
        Vector2 pos2 = new Vector2(playerRigidbody.position.x, playerRigidbody.position.y);

        return Vector2.Distance(pos1, pos2);
    }

    private bool IsGrounded()
    {
        return MathUtils.Equal(rigidbody.velocity.y, 0);
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        colliderLeft.gameObject.SetActive(colliderLeft.gameObject.activeSelf);
        colliderRight.gameObject.SetActive(colliderRight.gameObject.activeSelf);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
