using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform playerPos;

    public Rigidbody2D rb;

    private Animator anim;
    const string stateAttacking = "isAttacking";

    public int health = 100;

    

    public GameController GameController;

    

    private void Awake()
    {
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            
            Die();            
        }
    }

    public void Die()
    {
        GameController.EnemyHasDied();

        Destroy(gameObject);
    }


    public void EnemyStart()
    {
        gameObject.SetActive(true);
    }

}
