using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Button meleeButton;
    public Button rocketButton;

    public TimerButton timerButton;
    public GameObject timerCrcl;

    public MeleeButton meleebut;

    public GameManager gameMang;

    public melee meleeScript;

    public Canvas joystickCanvas;

    public Rigidbody2D rb;
    public Camera cam;

    public float moveSpeed = 5f;

    const string stateShooting = "Shooting";
    const string stateMoving = "Moving";
    public const string stateMelee = "Melee";
    const string stateDisabled = "isDisabled";

    Animator anim;
    private PlayerController player;
    public RocketButton rktButton;

    Vector2 movement;
    Vector2 mousePos;

    public GameObject meleeObj;

    public Joystick joystick;

    public Joystick shootJoystick;

    public int initialHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public GameObject dieEffect;

    Vector3 startPosition;

    bool isAlive = true;

    public GameObject spawnEffect;

    public bool ableToShoot;
    public bool bullet2Bool;
    public bool rocketBool;
    public bool meleeBool;

    public int lifes = 3;
    public int NumberOfIcons;

    public Image[] icons;
    public Sprite fullIcon;
    public Sprite emptyIcon;

    public float meleeTime = 30f;

    public float disabledTime;

    public bool rocketShootBool = false;

    public GameObject rocketTimer;

    public Button rocketButInter;

    public GameObject pauseMenu;

    public int initialLifes = 3;
   
    private void Awake()
    {
        
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lifes = PlayerPrefs.GetInt("VidasJugador1", lifes);

        currentHealth = initialHealth;

        healthBar.SetMaxHealth(initialHealth);

        anim.SetBool(stateShooting, false);
        anim.SetBool(stateMoving, false);
        anim.SetBool(stateMelee, false);
        anim.SetBool(stateDisabled, false);

        startPosition = this.transform.position;

        ableToShoot = true;
        meleeBool = true;
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

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
        }

        Vector3 posMouse = Input.mousePosition;
        posMouse.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        posMouse.x = posMouse.x - objectPos.x;
        posMouse.y = posMouse.y - objectPos.y;

        float angle = Mathf.Atan2(posMouse.y, posMouse.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        

        anim.SetBool(stateShooting, Shooting());


        Vector3 moveVector = new Vector3(shootJoystick.Horizontal, shootJoystick.Vertical);

        if (shootJoystick.Horizontal != 0 || shootJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }

        if(joystick.Horizontal !=0 || joystick.Vertical != 0 || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool(stateMoving, true);
        }
        else
        {
            anim.SetBool(stateMoving, false);
        }

        if (meleeBool == true && Input.GetKeyDown(KeyCode.V))
        {
            meleeObj.SetActive(true);
            timerCrcl.SetActive(true);
            timerButton.InvokeCircleOff();
            meleeScript.GetComponent<melee>().Start();
            meleeButton.interactable = false;
            meleebut.GetComponent<MeleeButton>().Timer();
            meleeOn();
            meleeBool = false;
            Invoke("MeleeActiveAgain", meleeTime);
        }

        if (rocketShootBool == true && Input.GetKeyDown(KeyCode.R))
        {
            rocketTimer.SetActive(true);
            RocketOn();
            rocketButInter.interactable = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameMang.currentGameState == GameState.inGame)
        {
            pauseMenu.SetActive(true);
            gameMang.PauseGame();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void MeleeActiveAgain()
    {
        meleeBool = true;
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
        if(shootJoystick.Horizontal >= .2f || Input.GetKey(KeyCode.Mouse0) || shootJoystick.Horizontal <= -.2f || shootJoystick.Vertical >= .2f || shootJoystick.Vertical <= -.2f)
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

    public void explosionHit()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        anim.SetBool(stateDisabled, true);

        StartCoroutine(ReactivateCollider(disabledTime));

        lifesDecrease();
    }

    IEnumerator ReactivateCollider(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<CircleCollider2D>().enabled = true;
        anim.SetBool(stateDisabled, false);
    }

    public void extraLife()
    {
        FindObjectOfType<GameManager>().StartGame();

        

        currentHealth = initialHealth;
        healthBar.SetHealth(currentHealth);

        lifes++;

        gameObject.GetComponent<Renderer>().enabled = true;

        GetComponent<CircleCollider2D>().enabled = false;
        anim.SetBool(stateDisabled, true);

        isAlive = true;

        joystickCanvas.enabled = true;

        StartCoroutine(ReactivateCollider(disabledTime));
        PlayerPrefs.SetInt("VidasJugador1", lifes);
    }

    public void lifesDecrease()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        anim.SetBool(stateDisabled, true);

        StartCoroutine(ReactivateCollider(disabledTime));
        lifes--;

        PlayerPrefs.SetInt("VidasJugador1", lifes);

        if (lifes == 0)
        {
            Die();
        }

        if (lifes < 0)
        {
            lifes = 0;
        }
    }

    public void Die()
    {
        if(isAlive == true)
        {
            
            isAlive = false; 

            GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.11f);

            gameObject.GetComponent<Renderer>().enabled = false;

            
            Invoke("CallDeathMenu", 1.1f);
        }
    }

    public void CallDeathMenu()
    {
        FindObjectOfType<GameManager>().DeathTrigger();
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

            rocketShootBool = true;
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

        ableToShoot = false;

        Invoke("meleeOff", 0.31f);
    }

    public void meleeOff()
    {
        anim.SetBool(stateMelee, false);

        ableToShoot = true;
    }

    public void ResetLifes()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("VidasJugador1", initialLifes);
    }
}
