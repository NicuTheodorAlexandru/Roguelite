using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public CollisionBridge colliderLeft;
    public CollisionBridge colliderRight;
    public Animator animator;
    public EnemyController enemy;
    public bool attacking = false;
    private bool stopAttacking = false;
    public SpriteRenderer enemySprite;
    public int damageAnimationFrameBegin;
    public int damageAnimationFrameEnd;
    // Start is called before the first frame update
    void Start()
    {
        colliderLeft.Initalize(this);
        colliderRight.Initalize(this);
        colliderLeft.enabled = false;
        colliderRight.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopAttacking)
        {
            attacking = false;
            stopAttacking = false;
        }
        if (!colliderLeft.gameObject.activeSelf && enemySprite.flipX)
        {
            colliderRight.gameObject.SetActive(false);
            colliderLeft.gameObject.SetActive(true);
        }
        else if (!colliderRight.gameObject.activeSelf && !enemySprite.flipX)
        {
            colliderRight.gameObject.SetActive(true);
            colliderLeft.gameObject.SetActive(false);
        }
    }

    void Attack(GameObject player)
    {
        
        if (player.layer != LayerMask.NameToLayer("Player Hitbox"))
            return;
        
        if (!attacking)
            return;
        //Destroy(enemy);
        int frame = (int)(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length *
                    (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) *
                    animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate);
        if(frame < damageAnimationFrameBegin || frame > damageAnimationFrameBegin)
        {
            return;
        }
        stopAttacking = true;
        player.GetComponentInParent<PlayerController>().GotAttacked(10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Attack(collision.gameObject);
    }
}
