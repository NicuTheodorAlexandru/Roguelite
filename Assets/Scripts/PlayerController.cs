using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    private HealthbarController healthbarController;
    private Transform spawnpoint;
    private Inventory inv;

    public override float Health
    {
        get { return base.Health; }
        set
        {
            base.Health = value;
            healthbarController.SetHealthbarHealth(health, maxHealth);
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        inv = GetComponent<Inventory>();
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

    protected override void Death()
    {
        //SetHealth(maxHealth);
        //transform.position = spawnpoint.position;
        //no respawn for you
        inv.Gold = 0;
        SceneManager.LoadScene("mainmenu");
    }
}
