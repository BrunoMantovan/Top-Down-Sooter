using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Camera cam;

    public float moveSpeed = 5f;

    const string stateShooting = "Shooting";

    Animator anim;
    private PlayerController player;

    Vector2 movement;
    Vector2 mousePos;

    public Joystick joystick;

    public Joystick shootJoystick;

    public int playerHealth = 100;

    public GameObject dieEffect;

    Vector3 startPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool(stateShooting, false);

        startPosition = this.transform.position;

        RestartPosition();
    }

    void RestartPosition()
    {
        this.transform.position = startPosition;
        this.rb.velocity = Vector2.zero;
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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3);

        Destroy(gameObject);
    }

}
