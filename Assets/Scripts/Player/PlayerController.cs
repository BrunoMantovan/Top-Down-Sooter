using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Camera cam;

    public float moveSpeed = 5f;

    const string stateShooting = "Shooting";
    const string stateMoving = "Moving";
    const string stateMelee = "Melee";
    const string stateDisabled = "isDisabled";

    Animator anim;
    private PlayerController player;
    public RocketButton rktButton;

    Vector2 movement;
    Vector2 mousePos;

    public Joystick joystick;

    public Joystick shootJoystick;

    public int initialHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public GameObject dieEffect;

    Vector3 startPosition;

    bool isAlive = true;

    public GameObject spawnEffect;

    public bool bullet2Bool;
    public bool rocketBool;


    public int lifes;
    public int NumberOfIcons;

    public Image[] icons;
    public Sprite fullIcon;
    public Sprite emptyIcon;

    public float disabledTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initialHealth;

        healthBar.SetMaxHealth(initialHealth);

        anim.SetBool(stateShooting, false);
        anim.SetBool(stateMoving, false);
        anim.SetBool(stateMelee, false);
        anim.SetBool(stateDisabled, false);

        startPosition = this.transform.position;     
    }   
    

    // Update is called once per frame
    void Update()
    {
        if (lifes > NumberOfIcons)
        {
            lifes = NumberOfIcons;
        }

        for (int i = 0; i < icons.Length; i++)
        {
            if (i < lifes)
            {
                icons[i].sprite = fullIcon;
            }
            else
            {
                icons[i].sprite = emptyIcon;
            }

            if (i < NumberOfIcons)
            {
                icons[i].enabled = true;
            }
            else
            {
                icons[i].enabled = false;
            }
        }


        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        

        anim.SetBool(stateShooting, Shooting());

        Vector3 moveVector = new Vector3(shootJoystick.Horizontal, shootJoystick.Vertical);
        if (shootJoystick.Horizontal != 0 || shootJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }

        if(joystick.Horizontal !=0 || joystick.Vertical != 0)
        {
            anim.SetBool(stateMoving, true);
        }
        else
        {
            anim.SetBool(stateMoving, false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    

    public void Spawn()
    {
        gameObject.GetComponent<Renderer>().enabled = false;

        GameObject spawnEff = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(spawnEff, 0.73f);

        Invoke("Shake", 0.71f);
        Invoke("EndSpawn", 0.73f);
    }

    void Shake()
    {
        CinemachineShake.Instance.ShakeCamera(3f, .15f);
    }

    public void EndSpawn()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    bool Shooting()
    {
        if(shootJoystick.Horizontal >= .2f || shootJoystick.Horizontal <= -.2f || shootJoystick.Vertical >= .2f || shootJoystick.Vertical <= -.2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int enemyDamage)
    {
        currentHealth -= enemyDamage;

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            if (lifes > 1)
            {
                currentHealth = initialHealth;
                healthBar.SetHealth(currentHealth);

                GetComponent<CircleCollider2D>().enabled = false;
                anim.SetBool(stateDisabled, true);

                StartCoroutine(ReactivateCollider(disabledTime)); 

                lifesDecrease();
            }
            else if(lifes <= 1)
            {
                lifesDecrease();

                Die();
            }
        }
         
    }

    IEnumerator ReactivateCollider(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<CircleCollider2D>().enabled = true;
        anim.SetBool(stateDisabled, false);
    }

    public void lifesDecrease()
    {
        lifes--;
    }

    public void Die()
    {
        if(isAlive == true)
        {
            isAlive = false; 

            GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);

            gameObject.GetComponent<Renderer>().enabled = false;

            Invoke("CallDeathMenu", 1.2f);
        }
    }

    public void CallDeathMenu()
    {
        FindObjectOfType<GameManager>().DeathTrigger();

        Destroy(gameObject);
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LootObjects lootObj = collision.GetComponent<LootObjects>();
        RocketLoot rocketLoot = collision.GetComponent<RocketLoot>();

        //Bullet2
        if ((lootObj !=null) && (bullet2Bool == false) && rocketBool == false)
        {
            
            bullet2Bool = true;

            Invoke("False", 10f);
        }

        else if((lootObj !=null) && bullet2Bool == true)
        {

            CancelInvoke("False");

            Invoke("False", 10f);
        }

        //Rocket
        if((rocketLoot !=null) && rocketBool == false)
        {
            rktButton.RocketButtonOn();
        }
    }

    public void RocketOn()
    {
        bullet2Bool = false;
        rocketBool = true;

        Invoke("False2", 10f);
    }

    void False()
    {
        bullet2Bool = false;
    }

    void False2()
    {
        rocketBool = false;
    }

    public void meleeOn()
    {
        anim.SetBool(stateMelee, true);

        Invoke("meleeOff", 0.31f);
    }

    public void meleeOff()
    {
        anim.SetBool(stateMelee, false);
    }
}
