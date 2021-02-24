using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEnemy : MonoBehaviour
{
    public float speed;
    public float attackSpeed;
    public float attackSpeedContinue;
    public float startWaitTime;
    float waitTime;

    public Transform moveSpot;
    public Transform playerPos;

    public float attackDistance;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public GameObject enemy3;

    public Rigidbody2D rb;

    public int health;

    public GameObject[] loots;
    int randomLoot;

    public int scoreValue;

    public GameObject dieEffect;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        waitTime = startWaitTime;

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > attackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = startWaitTime;
                }

                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }


        else if (Vector2.Distance(transform.position, playerPos.position) < attackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, attackSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > attackDistance)
        {
            Vector2 lookDir = moveSpot.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
        }
        
        else if (Vector2.Distance(transform.position, playerPos.position) < attackDistance)
        {
            Vector2 lookDir = playerPos.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            attackSpeed = 0;
            Die();
        }

        if (collision.gameObject.tag == "Bullet")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.None;
            attackSpeed = attackSpeedContinue;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    public void TakeDamage(int bulletDamage)
    {
        health -= bulletDamage;
        if (health <= 0)
        {
            //SpawnLoot();

            Die();
        }
    }

    public void SpawnLoot()
    {
        randomLoot = Random.Range(0, loots.Length);
        Instantiate(loots[randomLoot], transform.position, Quaternion.identity);
    }

    public void Die()
    {
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.583f);

        Score.scoreAmount += scoreValue;

        FindObjectOfType<GameController>().EnemyHasDied();

        Destroy(gameObject);
    }


}
