using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Shooting shootingScript;

    public Button meleeButton;

    public TimerButton timerButton;
    public GameObject timerCrcl;

    public MeleeButton meleebut;

    public GameManager gameMang;

    public melee meleeScript;

    public Canvas joystickCanvas;

    public Rigidbody2D rb;
    public Camera cam;

    public float moveSpeed;

    //const string stateShooting = "Shooting";
    const string stateMoving = "Moving";
    public const string stateMelee = "Melee";
    const string stateDisabled = "isDisabled";

    Animator anim;
    private PlayerController player;
    

    Vector2 movement;
    Vector2 mousePos;

    public GameObject meleeObj;    

    public GameObject dieEffect;

    Vector3 startPosition;

    bool isAlive = true;

    public GameObject spawnEffect;
    public GameObject spawnEffect2;
    public GameObject spawnEffect3;

    public bool bulletBool;
    public bool ableToShoot;
    public bool bullet2Bool;
    public bool bullet2Shoot;
    public bool rocketBool;
    public bool rocketAble;
    public bool shotgunBool;
    public bool shotgunAble;
    public bool meleeBool;
    public bool secondWeapon;

    public bool rotationOn;

    public int lifes = 4;
    public int NumberOfIcons;
    public int initialLifes = 4;
    public Image[] icons;
    public Sprite fullIcon;
    public Sprite emptyIcon;

    public float switchTime;
    public float meleeTime = 30f;
    public float disabledTime;

    public bool rocketShootBool = false;

    public GameObject rocketTimer;

    public Button rocketButInter;

    public GameObject pauseMenu;

    public SceneChanger sceneChanger;
    private int sceneNumber;

    private string secondaryWeapon;

    private int backpackInt = 0;
    public GameObject backpack;
    private void Awake()
    {       
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = PlayerPrefs.GetFloat("moveSpeed", moveSpeed);
        lifes = PlayerPrefs.GetInt("VidasJugador1", lifes);
        secondaryWeapon = PlayerPrefs.GetString("ArmaSecundaria", secondaryWeapon);
        EquipSecondaryWeapon();
        backpackInt = PlayerPrefs.GetInt("backpackInt", backpackInt);

        Invoke("backpackFunction", 1.334f);

        Spawn();  


        //anim.SetBool(stateShooting, false);
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
        if (Input.GetKeyDown(KeyCode.Q) && secondWeapon == true)
        {
            meleeBool = false;
            ableToShoot = false;
            Invoke("SwitchWeapon", switchTime);
        }

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


        movement.x = 0;
        movement.y = 0;

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

        if(movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        if(rotationOn == true) { 
        Vector3 posMouse = Input.mousePosition;
        posMouse.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        posMouse.x = posMouse.x - objectPos.x;
        posMouse.y = posMouse.y - objectPos.y;

        float angle = Mathf.Atan2(posMouse.y, posMouse.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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
        shootingScript.LMK1.SetActive(false);
        shootingScript.LMK2.SetActive(false);
        shootingScript.Cannon.SetActive(false);
        shootingScript.Shotgun.SetActive(false);
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<Renderer>().enabled = false;

        if (SceneManager.GetActiveScene().buildIndex == 3) 
        {
            GameObject spawnEff2 = Instantiate(spawnEffect2, transform.position, transform.rotation);
            Destroy(spawnEff2, 1.334f);
            Invoke("EndSpawn", 1.334f);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            GameObject spawnEff3 = Instantiate(spawnEffect3, transform.position, transform.rotation);
            Destroy(spawnEff3, 1.334f);
            Invoke("EndSpawn", 1.334f);
        }
        else
        {
            GameObject spawnEff = Instantiate(spawnEffect, transform.position, transform.rotation);
            Destroy(spawnEff, 1.250f);
            Invoke("EndSpawn", 1.250f);
        }
    }

    public void EndSpawn()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        rotationOn = true;
        shootingScript.LMK1.SetActive(true);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SwitchWeapon()
    {
        ableToShoot = true;
        meleeBool = true;

        if(bulletBool == true && bullet2Bool == true)
        {
            bullet2Shoot = true;
            bulletBool = false;
        }
        else if(bulletBool == true && rocketBool == true)
        {
            bulletBool = false;
            rocketAble = true;
        }
        else if(bulletBool == true && shotgunBool == true)
        {
            bulletBool = false;
            shotgunAble = true;
        }
        else if(bulletBool == false)
        {
            bulletBool = true;
            bullet2Shoot = false;
            rocketAble = false;
            shotgunAble = false;
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

    private void backpackFunction()
    {
        if(backpackInt == 1)
        {
            backpack.SetActive(true);
        }else if (backpackInt == 0)
        {
            backpack.SetActive(false);
        }
    }
    public void ExtraSpeed()
    {
        moveSpeed = moveSpeed * 1.15f;
        backpackInt = 1;
        PlayerPrefs.SetInt("backpackInt", backpackInt);
        backpackFunction();
        PlayerPrefs.SetFloat("moveSpeed", moveSpeed);
    }
    public void LifeIncrease()
    {
        if(lifes == 3)
        {
            return;
        }
        else
        {
            lifes++;
            PlayerPrefs.SetInt("VidasJugador1", lifes);
        }
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

            rb.bodyType = RigidbodyType2D.Static;

            Invoke("CallDeathMenu", 1.1f);
        }
    }

    public void CallDeathMenu()
    {
        FindObjectOfType<GameManager>().DeathTrigger();
    }


    public void EquipSecondaryWeapon()
    {
        if(secondaryWeapon == "LMK2")
        {
            bulletBool = false;
            bullet2Shoot = true;
            bullet2Bool = true;          
            rocketAble = false;
            rocketBool = false;
            shotgunBool = false;
            shotgunAble = false;
            secondWeapon = true;
            shootingScript.LMK2Active = true;            
        }
        else if (secondaryWeapon == "PlasmaCannon")
        {
            bulletBool = false;
            bullet2Bool = false;
            bullet2Shoot = false;
            rocketAble = true;
            rocketBool = true;
            shotgunBool = false;
            shotgunAble = false;
            secondWeapon = true;
            shootingScript.PlasmaCannonActive = true;
        }
        else if (secondaryWeapon == "Shotgun")
        {
            shotgunBool = true;
            shotgunAble = true;
            bulletBool = false;
            bullet2Bool = false;
            bullet2Shoot = false;
            secondWeapon = true;
            rocketAble = false;
            rocketBool = false;
            shootingScript.ShotgunActive = true;
        }
        else
        {
            bulletBool = true;
            bullet2Bool = false;
            bullet2Shoot = false;
            rocketAble = false;
            rocketBool = false;
            shotgunBool = false;
            shotgunAble = false;
            secondWeapon = false;
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LootObjects lootObj = collision.GetComponent<LootObjects>();
        RocketLoot rocketLoot = collision.GetComponent<RocketLoot>();
        ShotgunLoot shotgunLoot = collision.GetComponent<ShotgunLoot>();
        //Bullet2
        if ((lootObj !=null) && (bullet2Bool == false))
        {
            secondaryWeapon = "LMK2";
            PlayerPrefs.SetString("ArmaSecundaria", secondaryWeapon);
            EquipSecondaryWeapon();

            shootingScript.SetMaxLMK2();
            FindObjectOfType<AudioManager>().Play("PickUP");
        }
        //Rocket
        if ((rocketLoot !=null) && rocketBool == false)
        {
            secondaryWeapon = "PlasmaCannon";
            PlayerPrefs.SetString("ArmaSecundaria", secondaryWeapon);
            EquipSecondaryWeapon();

            shootingScript.SetMaxCannon();
            FindObjectOfType<AudioManager>().Play("PickUP");
        }
        //Shotgun
        if ((shotgunLoot !=null) && shotgunBool == false)
        {
            secondaryWeapon = "Shotgun";
            PlayerPrefs.SetString("ArmaSecundaria", secondaryWeapon);
            EquipSecondaryWeapon();

            shootingScript.SetMaxShotgun();
            FindObjectOfType<AudioManager>().Play("PickUP");
        }
    }

    public void False()
    {

        ableToShoot = false;
        bullet2Bool = false;
        bullet2Shoot = false;
        secondWeapon = false;
        bulletBool = true;
        Invoke("DelayedSwitch", switchTime);
        
       
    }
    public void False2()
    {
        secondWeapon = false;
        rocketAble = false;
        bulletBool = true;
        rocketBool = false;
    }

    public void False3()
    {
        ableToShoot = false;
        shotgunBool = false;
        shotgunAble = false;
        secondWeapon = false;
        bulletBool = true;
        Invoke("DelayedSwitch", switchTime);
    }

    public void DelayedSwitch()
    {
        ableToShoot = true;
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
