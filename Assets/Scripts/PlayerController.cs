using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Camera cam;

    public float moveSpeed = 5f;

    const string stateShooting = "Shooting";
    const string stateMoving = "Moving";

    Animator anim;
    private PlayerController player;
    public RocketButton rktButton;

    Vector2 movement;
    Vector2 mousePos;

    public Joystick joystick;

    public Joystick shootJoystick;

    public int playerHealth = 100;

    public GameObject dieEffect;

    Vector3 startPosition;

    bool isAlive = true;

    public GameObject spawnEffect;

    public bool bullet2Bool;
    public bool rocketBool;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnEff = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(spawnEff, 0.73f);

        anim.SetBool(stateShooting, false);
        anim.SetBool(stateMoving, false);

        startPosition = this.transform.position;

        Invoke("Shake", 0.71f);
        Invoke("Spawn", 0.73f);       
    }   
    

    // Update is called once per frame
    void Update()
    {
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
        playerHealth -= enemyDamage;
        if (playerHealth <= 0)
        {
            Die();
        }
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

    void Shake()
    {
        
        CinemachineShake.Instance.ShakeCamera(3f, .15f);
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LootObjects lootObj = collision.GetComponent<LootObjects>();
        RocketLoot rocketLoot = collision.GetComponent<RocketLoot>();

        //Bullet2
        if ((lootObj !=null) && bullet2Bool == false)
        {
            rocketBool = false;
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
}
