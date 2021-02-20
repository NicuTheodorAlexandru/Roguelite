using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int health = 2;
    public Animator animator;
    public float speed = 17;
    public int soulsDropped;
    public int minGoldDropped;
    public int maxGoldDropped;
    public float aggroRange = 10;
    public CollisionBridge colliderLeft;
    public CollisionBridge colliderRight;
    public float attackRange;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Rigidbody2D playerRigidbody;
    public BoxCollider2D hitbox;
    private bool aggroed = false;
    public Vector2 fov;
    private int xInput = 0, yInput = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        //playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        //Debug.Log(colliderLeft);
    }

    private float Forward()
    {
        if (spriteRenderer.flipX)
            return -1.0f;
        return 1.0f;
    }

    void Deb()
    {
        //float angle = GetAngleTowardPlayer();
        float x = fov.x;
        float y = fov.y;
        if(spriteRenderer.flipX)
        {
            x += 180;
            y -= 180;
        }
        //ray 1
        Quaternion q = Quaternion.AngleAxis(fov.x, Vector3.right);
        Vector3 ldir = q * rigidbody.transform.forward;
        Vector3 dir = transform.TransformDirection(ldir);
        Vector2 d = new Vector2(dir.x, dir.y);
        d *= aggroRange;
        d += rigidbody.position;
        var lDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * x), Mathf.Sin(Mathf.Deg2Rad * x)) * aggroRange;
        Debug.DrawLine(rigidbody.position, rigidbody.position + lDirection, Color.red, 0.1f, false);
        //ray 2
        q = Quaternion.AngleAxis(fov.x, Vector3.right);
        ldir = q * rigidbody.transform.forward;
        dir = transform.TransformDirection(ldir);
        d = new Vector2(dir.x, dir.y);
        d *= aggroRange;
        d += rigidbody.position;
        lDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * y), Mathf.Sin(Mathf.Deg2Rad * y)) * aggroRange;
        Debug.DrawLine(rigidbody.position, rigidbody.position + lDirection, Color.red, 0.1f, false);
        //
        
        float angle = GetAngleTowardPlayer();
        
        lDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * aggroRange;
        Debug.DrawLine(rigidbody.position, rigidbody.position + lDirection, Color.green, 0.1f, false);
    }

    private void Aggro()
    {
        //Deb();
        float angle = GetAngleTowardPlayer();
        float x = fov.x;
        float y = fov.y;
        if (spriteRenderer.flipX)
        {
            x += 180;
            y -= 180;
        }
        if (aggroed)
        {
            /*if (DistanceToPlayer() > aggroRange)
            {
                aggroed = false;
            }
            else if(x < angle || y > angle)
            {
                aggroed = false;
            }*/
        }
        else
        {
            float yy = playerRigidbody.position.y - rigidbody.position.y;
            if (DistanceToPlayer() < aggroRange && (x >= angle && y <= angle) && (yy <= 1 && yy >= -1) )
            {
                aggroed = true;
            }
        }
    }

    private void Update()
    {
        Aggro();
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
            hitbox.enabled = false;
            Inventory inv = GameObject.Find("Player(Clone)").GetComponent<Inventory>();
            inv.Souls = inv.Souls + soulsDropped;
            int gold = Random.Range(minGoldDropped, maxGoldDropped);
            inv.Gold = inv.Gold + gold;
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

    private float GetAngleTowardPlayer()
    {
        if (playerRigidbody == null)
        {
            playerRigidbody = GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>();
            return Mathf.Infinity;
        }
        return Vector2.SignedAngle(transform.right, playerRigidbody.position - rigidbody.position);
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
