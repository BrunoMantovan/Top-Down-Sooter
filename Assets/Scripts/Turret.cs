using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float fireForce = 20f;
    public Transform fireTip;

    public Rigidbody2D rb;

    public Transform enemyPos;
    public GameObject bullet;

    private float timeBtwShots;
    public float fireRate;

    public bool fireOn;

    public float activeTime;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = this.GetComponent<Rigidbody2D>();
        Invoke("DeactivateTurret", activeTime);
    }

    // Update is called once per frame
    void Update()
    {
        enemyPos = GameObject.FindGameObjectWithTag("Enemy").transform;

        if (timeBtwShots <= 0 && fireOn == true && active == true)
        {
            GameObject spit = Instantiate(bullet, fireTip.position, fireTip.rotation);
            Rigidbody2D rbSpit = spit.GetComponent<Rigidbody2D>();
            rbSpit.AddForce(fireTip.up * fireForce, ForceMode2D.Impulse);
            timeBtwShots = fireRate;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
       if(active == true)
        {
            Vector2 lookDir = enemyPos.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
        }
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            fireOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            fireOn = false;
        }
    }
    
    void DeactivateTurret()
    {
        this.gameObject.SetActive(false);
    }

}
