using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemy : MonoBehaviour
{
    public float speed;
    public float stopDistance;
    public float reatreatDistance;

    public float spitForce = 20f;
    public Transform fireTip;

    public Rigidbody2D rb;

    public Transform playerPos;

    public GameObject projectile;
    public GameObject dieEffect;

    private float timeBtwShots;
    public float fireRate;

    Animator anim;
    const string stateMoving = "isMoving";

    public int health = 50;

    public GameObject[] loots;

    int randomLoot;
    public int scoreValue;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        anim.SetBool(stateMoving, false);

        rb = this.GetComponent<Rigidbody2D>();

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > stopDistance)
        {

            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            anim.SetBool(stateMoving, true);
        }

        else if (Vector2.Distance(transform.position, playerPos.position) < stopDistance && Vector2.Distance(transform.position, playerPos.position) > reatreatDistance)
        {
            transform.position = this.transform.position;

            anim.SetBool(stateMoving, false);

            if (timeBtwShots <= 0)
            {
                GameObject spit = Instantiate(projectile, fireTip.position, fireTip.rotation);
                Rigidbody2D rbSpit = spit.GetComponent<Rigidbody2D>();
                rbSpit.AddForce(fireTip.up * spitForce, ForceMode2D.Impulse);
                timeBtwShots = fireRate;
                FindObjectOfType<AudioManager>().Play("AlienShoot");
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        else if (Vector2.Distance(transform.position, playerPos.position) < reatreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, -speed * Time.deltaTime);
            anim.SetBool(stateMoving, true);

            if (timeBtwShots <= 0)
            {
                GameObject spit = Instantiate(projectile, fireTip.position, fireTip.rotation);
                Rigidbody2D rbSpit = spit.GetComponent<Rigidbody2D>();
                rbSpit.AddForce(fireTip.up * spitForce, ForceMode2D.Impulse);
                timeBtwShots = fireRate;
                FindObjectOfType<AudioManager>().Play("AlienShoot");
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }       
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = playerPos.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }


    public void TakeDamage(int bulletDamage)
    {
        health -= bulletDamage;

        Score.scoreAmount += 10;

        if (health <= 0)
        {
            SpawnLoot();

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
        FindObjectOfType<AudioManager>().Play("AlienDeath");
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.3f);

        Score.scoreAmount += scoreValue;

        FindObjectOfType<GameController>().EnemyHasDied();

        Destroy(gameObject);
    }
}
