using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public CollisionBridge colliderLeft;
    public CollisionBridge colliderRight;
    public Animator animator;
    public PlayerController player;
    public static bool attacking = false;
    private bool stopAttacking = false;
    public SpriteRenderer playerSprite;
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
        if(stopAttacking)
        {
            attacking = false;
            stopAttacking = false;
        }
        if(!colliderLeft.gameObject.activeSelf && playerSprite.flipX)
        {
            colliderRight.gameObject.SetActive(false);
            colliderLeft.gameObject.SetActive(true);
        }
        else if(!colliderRight.gameObject.activeSelf && !playerSprite.flipX)
        {
            colliderRight.gameObject.SetActive(true);
            colliderLeft.gameObject.SetActive(false);
        }
    }

    void Attack(GameObject enemy)
    {
        if (enemy.layer != LayerMask.NameToLayer("Enemy Hitbox"))
            return;
        if (!attacking)
            return;
        //Destroy(enemy);
        
        stopAttacking = true;
        enemy.GetComponentInParent<EnemyController>().GotAttacked(1);
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
