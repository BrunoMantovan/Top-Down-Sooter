using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform playerPos;

    public Rigidbody2D rb;

    public float timer;

    private Animator anim;
    const string stateAttacking = "isAttacking";
    

    public int health = 100;

    private bool attackmode;
    private bool cooling;
    private float intTimer;

    public GameController GameController;

    public int enemyDamage = 5;

    public GameObject dieEffect;
    

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool(stateAttacking, false);
        


        rb = this.GetComponent<Rigidbody2D>();

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = playerPos.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            speed = 0;

            anim.SetBool(stateAttacking, true);
        }

        if(collision.gameObject.tag == "Bullet")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;            
        }
        
        if(collision.gameObject.tag == "Enemy")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.None;
            speed = 5;

            anim.SetBool(stateAttacking, false);
        }

        if(collision.gameObject.tag == "Bullet")
        {
            rb.constraints = RigidbodyConstraints2D.None;            
        }

        if (collision.gameObject.tag == "Enemy")
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();

        if(player != null)
        {
            player.TakeDamage(enemyDamage);
        }
    }

    public void TakeDamage(int bulletDamage)
    {
        health -= bulletDamage;
        if (health <= 0)
        {
            
            Die();            
        }
    }

    public void Die()
    {
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.3f);

        GameController.EnemyHasDied();

        Destroy(gameObject);
    }


    public void EnemyStart()
    {
        gameObject.SetActive(true);
    }

}
